﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Templates.Core.Locations
{
    public class LocalTemplatesSourceV2 : TemplatesSourceV2
    {
        public static readonly TemplatesPackageInfo VersionZero = new TemplatesPackageInfo()
        {
            Name = "LocalTemplates",
            LocalPath = $@"..\..\..\..\..\{TemplatesFolderName}",
            Bytes = 1024,
            Date = DateTime.Now,
            Uri = new Uri("file://"),
            Version = new Version(0, 0, 0, 0)
        };

        protected virtual string Origin => $@"..\..\..\..\..\{TemplatesFolderName}";

        private string _id;

        private List<TemplatesPackageInfo> availablePackages = new List<TemplatesPackageInfo>();

        protected override bool VerifyPackageSignatures => false;

        public override string Id { get => _id; }

        public LocalTemplatesSourceV2()
            : this("0.0.0.0", true)
        {
            _id = Configuration.Current.Environment + GetAgentName();
        }

        public LocalTemplatesSourceV2(string id)
            : this("0.0.0.0", true)
        {
            _id = id + GetAgentName();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Used to override the default value for this property in the local (test) source")]
        public LocalTemplatesSourceV2(string templatesVersion, bool forcedAdquisition = true)
        {
            ForcedAcquisition = forcedAdquisition;

            if (string.IsNullOrEmpty(_id))
            {
                _id = Configuration.Current.Environment + GetAgentName();
            }

            availablePackages.Add(VersionZero);
            Version.TryParse(templatesVersion, out Version v);
            if (!v.IsZero())
            {
                var package = new TemplatesPackageInfo()
                {
                    Name = $"LocalTemplates_v{v.ToString()}",
                    LocalPath = $@"..\..\..\..\..\{TemplatesFolderName}",
                    Bytes = 1024,
                    Date = DateTime.Now,
                    Uri = new Uri("file://"),
                    Version = v
                };

                availablePackages.Add(package);
            }
        }

        public override void LoadConfig()
        {
            Config = new TemplatesSourceConfig()
            {
                PackagesInfo = availablePackages,
                Latest = VersionZero,
                VersionCount = 1
            };
        }

        public override TemplatesContentInfo GetContent(TemplatesPackageInfo packageInfo, string workingFolder)
        {
            string targetFolder = Path.Combine(workingFolder, packageInfo.Version.ToString());

            if (Directory.Exists(targetFolder))
            {
                Fs.SafeDeleteDirectory(targetFolder);
            }

            NativeMethods.CreateJunction(Origin, targetFolder, true);

            return new TemplatesContentInfo()
            {
                Version = packageInfo.Version,
                Path = targetFolder,
                Date = packageInfo.Date
            };
        }

        public override void Adquire(ref TemplatesPackageInfo packageInfo)
        {
            packageInfo.LocalPath = Origin;
        }

        protected static string GetAgentName()
        {
            // If running tests in VSTS concurrently in different agents avoids the collison in templates folders
            string agentName = Environment.GetEnvironmentVariable("AGENT_NAME");
            if (string.IsNullOrEmpty(agentName))
            {
                return string.Empty;
            }
            else
            {
                return $"-{agentName}";
            }
        }
    }
}
