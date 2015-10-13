// Created by Mike Williams - 22/09/2015
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

        public Telemetry()
        {
            _helper = new SystemHelper();
            Debug.WriteLine(_helper.MachineId);
            Debug.WriteLine(_helper.SystemOs);
            Debug.WriteLine(_helper.WordProduct);
        }

        private void WriteSystemInfo()
        {
            bool result1 = false;
            bool result2 = false;

            try
            {
                // Write OS
                AzureStorage storage = new AzureStorage();
                MessageEntity me = new MessageEntity();
                me.MachineId = _helper.MachineId;
                me.Operation = "StartUp";
                me.Level = "Information";
                me.Message = _helper.SystemOs;
                result1 = storage.WriteMessage(me);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
            }

            try
            {
                // Write Word version
                AzureStorage storage = new AzureStorage();
                MessageEntity me = new MessageEntity();
                me.MachineId = _helper.MachineId;
                me.Operation = "StartUp";
                me.Level = "Information";
                me.Message = _helper.WordProduct;
                result2 = storage.WriteMessage(me);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
            }

            _systemInfoSent = result1 && result2;
        }

        public void Write(string operation, string level, string message)
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

        public int WordVersion()
        {
            return _helper.WordVersion;
        }
    }
}
