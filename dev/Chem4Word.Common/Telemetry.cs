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
        private bool systemInfoSent = false;

        private SystemHelper _helper;
        private AzureStorage _storage;

        public Telemetry()
        {
            _helper = new SystemHelper();
            Debug.WriteLine(_helper.MachineId);
            Debug.WriteLine(_helper.SystemOs);
            Debug.WriteLine(_helper.WordVersion);
            _storage = new AzureStorage();
        }

        private void WriteSystemInfo()
        {
            MessageEntity me = new MessageEntity();
            me.MachineId = _helper.MachineId;
            me.Operation = "StartUp";
            me.Level = "Information";
            me.Message = _helper.SystemOs + Environment.NewLine + _helper.WordVersion;
            systemInfoSent = _storage.WriteMessage(me);
        }

        public void Write(string operation, string level, string message)
        {
            if (!systemInfoSent)
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
    }
}
