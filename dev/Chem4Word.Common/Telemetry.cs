﻿// Created by Mike Williams - 22/09/2015
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.
//   This software is released under the Apache License, Version 2.0.
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Chem4Word.Common
{
    public class Telemetry
    {
        private bool _systemInfoSent = false;

        private SystemHelper _helper;

        public int WordVersion {
            get
            {
                return _helper.WordVersion;
            }
        }

        public Telemetry()
        {
            _helper = new SystemHelper();
            Debug.WriteLine(_helper.MachineId);
            Debug.WriteLine(_helper.IpAddress);
            Debug.WriteLine(_helper.SystemOs);
            Debug.WriteLine(_helper.WordProduct);
        }

        private void WriteSystemInfo()
        {
            string module = "Telemetry.WriteSystemInfo()";

            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;

            AzureStorage storage = new AzureStorage();

            try
            {
                // Write OS
                MessageEntity me1 = new MessageEntity();
                me1.MachineId = _helper.MachineId;
                me1.Operation = "StartUp";
                me1.Level = "Information";
                me1.Message = _helper.SystemOs;
                result1 = storage.WriteMessage(me1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
                Write(module, "Exception", "OS: " + ex.Message);
            }

            try
            {
                // Write Word version
                MessageEntity me2 = new MessageEntity();
                me2.MachineId = _helper.MachineId;
                me2.Operation = "StartUp";
                me2.Level = "Information";
                me2.Message = _helper.WordProduct;
                result2 = storage.WriteMessage(me2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
                Write(module, "Exception", "WV: " + ex.Message);
            }

            try
            {
                // Write Word command line
                MessageEntity me5 = new MessageEntity();
                me5.MachineId = _helper.MachineId;
                me5.Operation = "StartUp";
                me5.Level = "Information";
                me5.Message = Environment.GetCommandLineArgs()[0];
                result5 = storage.WriteMessage(me5);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
                Write(module, "Exception", "WV: " + ex.Message);
            }

            try
            {
                // Write AddIn Version
                MessageEntity me3 = new MessageEntity();
                me3.MachineId = _helper.MachineId;
                me3.Operation = "StartUp";
                me3.Level = "Information";
#if DEBUG
                me3.Message = _helper.AddInVersion + " (debug)";
#else
                me3.Message = _helper.AddInVersion + " (R9 25-Oct-2017)";
#endif
                result3 = storage.WriteMessage(me3);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
                Write(module, "Exception", "AV: " + ex.Message);
            }

            try
            {
                // Write Ip Address
                MessageEntity me4 = new MessageEntity();
                me4.MachineId = _helper.MachineId;
                me4.Operation = "StartUp";
                me4.Level = "Information";
                me4.Message = _helper.IpAddress;
                result4 = storage.WriteMessage(me4);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
                Write(module, "Exception", "IP: " + ex.Message);
            }

            _systemInfoSent = result1 && result2 && result3 && result4 && result5;
        }

        public void Write(string operation, string level, string message)
        {
            if (!string.IsNullOrEmpty(_helper.IpAddress) && !_helper.IpAddress.Contains("0.0.0.0"))
            {
                if (!_systemInfoSent)
                {
                    WriteSystemInfo();
                }

                AzureStorage storage = new AzureStorage();
                MessageEntity me = new MessageEntity();
                me.MachineId = _helper.MachineId;
                me.Operation = operation;
                me.Level = level;
                me.Message = message;
                storage.WriteMessage(me);
            }
        }
    }
}
