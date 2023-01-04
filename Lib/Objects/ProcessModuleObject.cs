﻿// Copyright (c) Microsoft Corporation. All rights reserved. Licensed under the MIT License.
using System.Collections.Generic;
using System.Diagnostics;
using MessagePack;

namespace Microsoft.CST.AttackSurfaceAnalyzer.Objects
{
    [MessagePackObject]
    public class ProcessModuleObject
    {
        public ProcessModuleObject(string? FileName, string? ModuleName, SerializableFileVersionInfo? FileVersionInfo)
        {
            this.FileName = FileName;
            this.ModuleName = ModuleName;
            this.FileVersionInfo = FileVersionInfo;
        }

        public ProcessModuleObject() { }
        [Key(0)]
        public string? FileName { get; set; }
        [Key(2)]
        public SerializableFileVersionInfo? FileVersionInfo { get; set; }
        [Key(1)]
        public string? ModuleName { get; set; }

        internal static ProcessModuleObject FromProcessModule(ProcessModule mainModule)
        {
            return new ProcessModuleObject(mainModule.FileName, mainModule.ModuleName, SerializableFileVersionInfo.FromFileVersionInfo(mainModule.FileVersionInfo));
        }

        internal static List<ProcessModuleObject> FromProcessModuleCollection(ProcessModuleCollection modules)
        {
            var output = new List<ProcessModuleObject>();
            foreach (var processModule in modules)
            {
                if (processModule is ProcessModule pm)
                    output.Add(FromProcessModule(pm));
            }
            return output;
        }
    }
}