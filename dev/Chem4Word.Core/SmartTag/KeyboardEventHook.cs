// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using log4net;

namespace Chem4Word.Core.SmartTag
{
    /// <summary>
    /// Class to hook up keyboard events. Word APIs do not expose keypress events. 
    /// This class helps us capture the right click keyboard shortcuts.
    /// </summary>
    public class KeyboardEventHook : IDisposable
    {
        #region Delegates

        /// <summary>
        /// delegate referencing the callback procedure written for handling key press events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void KeyboardRightClickEventHandler(object sender, EventArgs e);

        #endregion

        /// <summary>
        /// Used to denote Keyboard event hooks type.
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Message ID for key down events
        /// </summary>
        private const int WM_KEYDOWN = 0x0100;

        private static readonly ILog Log = LogManager.GetLogger(typeof (KeyboardEventHook));

        /// <summary>
        /// initialization of delegate used to invoke.
        /// </summary>
        private readonly LowLevelKeyboardProc proc;

        /// <summary>
        /// Variable to indicate that dispose is called.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// hook ID object.
        /// </summary>
        private IntPtr hookID = IntPtr.Zero;

        /// <summary>
        /// flag to indicate the shift key press for right click shortcut context.
        /// </summary>
        private bool isShiftKeyPressed;

        /// <summary>
        /// Creates an instance of KeyboardEventHook.
        /// </summary>
        public KeyboardEventHook()
        {
            // call back procedure assigned to the delegate object.
            this.proc = this.HookCallback;
        }

        /// <summary>
        /// Event to be fired in context menu helper class for handling right click context menu event.
        /// </summary>
        public event KeyboardRightClickEventHandler KeyboardRightClickEvent;

        /// <summary>
        /// Hooks the Keyboard event to the current process.
        /// </summary>
        internal void SetKeyBoardHook()
        {
            // un hook the existing hooks before adding the new one.
            this.ClearKeyBoardHook();

            // used to add the keyboard hook to the current running process.
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    this.hookID = SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                                                   GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        /// <summary>
        /// Callback event for the Keypress event.
        /// </summary>
        /// <param name="nCode">code for hook procedure to process</param>
        /// <param name="wParam">Specifies the virtual-key code of the key that generated the keystroke message</param>
        /// <param name="lParam">Specifies the repeat count</param>
        /// <returns></returns>
        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr) WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys currentKey = (Keys) vkCode;

                //Log.Debug(currentKey);

                // Check if the "Context Menu" key or "Shift+F10" was pressed.
                if (currentKey == Keys.Apps || (this.isShiftKeyPressed && currentKey == Keys.F10))
                {
                    this.KeyboardRightClickEvent(this, new EventArgs());
                }
                else if (currentKey == Keys.RShiftKey || currentKey == Keys.LShiftKey)
                {
                    this.isShiftKeyPressed = true;
                }
                else
                {
                    this.isShiftKeyPressed = false;
                }
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Releases the keyboard hook.
        /// </summary>
        internal void ClearKeyBoardHook()
        {
            if (hookID != IntPtr.Zero)
            {
                bool ret = UnhookWindowsHookEx(hookID);
                if (ret == false)
                {
                    return;
                }
                hookID = IntPtr.Zero;
            }
        }

        #region Native Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
                                                      LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                                                    IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// The Dispose method helps to release the resources
        /// </summary>
        /// <param name="disposing">variable indicating if its an explicit call or implicit call</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.ClearKeyBoardHook();
                Log.Debug("Released Keyboard hook.");
                this.disposed = true;

                // Suppress finalization of this disposed instance.
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        /// <summary>
        /// Disposable types with unmanaged resources implement a finalizer.
        /// the following code to satisfy rule:
        /// DisposableTypesShouldDeclareFinalizer
        /// </summary>
        ~KeyboardEventHook()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// delegate for the callback in keyboard events.
        /// </summary>
        /// <param name="nCode">code for hook procedure to process</param>
        /// <param name="wParam">Specifies the virtual-key code of the key that generated the keystroke message</param>
        /// <param name="lParam">Specifies the repeat count</param>
        /// <returns></returns>
        internal delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);
    }
}