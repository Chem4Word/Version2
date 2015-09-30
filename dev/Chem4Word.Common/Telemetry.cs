// Created by Mike Williams - 22/09/2015
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Chem4Word.Common
{
    public class Telemetry
    {
        private bool _systemInfoSent = false;

        private SystemHelper _helper;
        private AzureStorage _storage;

        public Telemetry()
        {
            _helper = new SystemHelper();
            Debug.WriteLine(_helper.MachineId);
            Debug.WriteLine(_helper.SystemOs);
            Debug.WriteLine(_helper.WordProduct);
            _storage = new AzureStorage();
        }

        private void WriteSystemInfo()
        {
            bool result1 = false;
            bool result2 = false;

            try
            {
                // Write OS
                MessageEntity me = new MessageEntity();
                me.MachineId = _helper.MachineId;
                me.Operation = "StartUp";
                me.Level = "Information";
                me.Message = _helper.SystemOs;
                result1 = _storage.WriteMessage(me);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message);
            }

            try
            {
                // Write Word version
                MessageEntity me = new MessageEntity();
                me.MachineId = _helper.MachineId;
                me.Operation = "StartUp";
                me.Level = "Information";
                me.Message = _helper.WordProduct;
                result2 = _storage.WriteMessage(me);
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
            MessageEntity me = new MessageEntity();
            me.MachineId = _helper.MachineId;
            me.Operation = operation;
            me.Level = level;
            me.Message = message;
            _storage.WriteMessage(me);
        }

        public int WordVersion()
        {
            return _helper.WordVersion;
        }
    }
}
