﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Packaging;

namespace WtsTool
{
    internal class PackageWorker
    {
        internal static void Create(string inputPath, string outfile, string certThumbprint, TextWriter output, TextWriter error)
        {
            try
            {
                if (Directory.Exists(inputPath))
                {
                    output.WriteCommandHeader($"Creating template package from folder {inputPath}.");
                    if (!string.IsNullOrWhiteSpace(certThumbprint))
                    {
                        output.WriteCommandText($"The template package will be signed using the cert matching {certThumbprint} as thumbprint.");
                        TemplatePackage.PackAndSign(inputPath, outfile, certThumbprint, "text/plain");
                        output.WriteCommandText($"Templates package file '{outfile}' successfully created.");
                    }
                    else
                    {
                        output.WriteCommandText($"No cert thumbprint provided, the template package will not be signed.");
                        TemplatePackage.Pack(inputPath, outfile, "text/plain");
                    }

                    output.WriteCommandText($"Templates package file '{outfile}' successfully created.");
                }
                else
                {
                    error.WriteCommandText($"{inputPath} is not a valid folder to create a Templates Package.");
                }
            }
            catch (Exception ex)
            {
                error.WriteException(ex, "Unexpected exception creating templates package.");
            }
        }

        internal static void Prepare(string prepareDir, IEnumerable<string> exclusions, string version, bool verbose, TextWriter output, TextWriter error)
        {
            output.WriteCommandHeader($"Preparing directory {prepareDir} for version {version}");

            Version.TryParse(version, out Version v);
            if (v != null)
            {
                DirectoryInfo prepareDirInfo = new DirectoryInfo(prepareDir);

                if (prepareDirInfo.Exists)
                {
                    List<Regex> exclusionFilters = GetExclusionFilters(exclusions, output);

                    var resultDir = Path.GetFullPath(prepareDir).Replace(Path.GetFileName(prepareDir), $"{Path.GetFileName(prepareDir)}_v{v.ToString()}");

                    List<string> excludedDirs = new List<string>();
                    List<DirectoryInfo> includedDirs = new List<DirectoryInfo>();
                    List<DirectoryInfo> allDirs = new List<DirectoryInfo>(prepareDirInfo.EnumerateDirectories("*", SearchOption.AllDirectories));

                    ApplyFilters(exclusionFilters, excludedDirs, includedDirs, allDirs);

                    if (excludedDirs.Count == 0 && exclusionFilters.Count > 0)
                    {
                        output.WriteCommandText("WARN: Preparation did not work. The exclusions do not filter any directory, please review the regular expresions.");
                        return;
                    }

                    if (exclusionFilters.Count > 0 && (excludedDirs.Count + includedDirs.Count != allDirs.Count))
                    {
                        output.WriteCommandText($"WARN: Preparation did not work. The excluded dirs ({excludedDirs.Count}) plus the included dirs ({includedDirs.Count}) do not match the original dir count {allDirs.Count}");
                        return;
                    }

                    ShowDirectoriesInfo(verbose, output, exclusionFilters, excludedDirs, includedDirs);

                    PrepareResultDir(output, resultDir);

                    List<DirectoryInfo> toCopy = exclusionFilters.Count > 0 ? includedDirs : allDirs;

                    MakeCopy(prepareDir, version, output, resultDir, toCopy);
                }
                else
                {
                    error.WriteCommandText($"WARN: The directory '{prepareDir}' does not exists");
                }
            }
            else
            {
                error.WriteCommandText($"WAR: {version} is not a valid version number");
            }
        }

        internal static void Extract(string inputFile, string destinationDir, TextWriter output, TextWriter error)
        {
            try
            {
                if (File.Exists(inputFile))
                {
                    if (destinationDir == ".")
                    {
                        destinationDir = System.Environment.CurrentDirectory;
                    }

                    Fs.EnsureFolder(destinationDir);

                    output.WriteCommandHeader($"Extracting {inputFile} to {destinationDir}...");
                    TemplatePackage.Extract(inputFile, destinationDir, true);
                }
                else
                {
                    error.WriteCommandText($"{inputFile} is not a valid folder to create a Templates Package.");
                }
            }
            catch (Exception ex)
            {
                error.WriteException(ex, "Unexpected exception extracting templates package.");
            }
        }

        internal static void GetInfo(string inputPath, TextWriter output, TextWriter error)
        {
            try
            {
                output.WriteCommandHeader($"Templates package {inputPath} information:");
                if (File.Exists(inputPath))
                {
                    output.WriteCommandText($"Is signed: {TemplatePackage.IsSigned(inputPath).ToString()}");

                    WriteSignatureValidationsInfo(inputPath, output);

                    WriteCertificatesInfo(inputPath, output);

                    WriteCurrentPinConfiguration(output);
                }
            }
            catch (Exception ex)
            {
                error.WriteException(ex, $"Unexpected error getting template package information.");
            }
        }

        private static void WriteCertificatesInfo(string inputPath, TextWriter output)
        {
            var certsInfo = TemplatePackage.GetCertsInfo(inputPath);
            output.WriteLine();
            output.WriteCommandText($"Found {certsInfo.Count} certificates in the package.");
            foreach (var info in certsInfo)
            {
                output.WriteLine();
                output.WriteCommandText($" Cert Subject: {info.cert.Subject}");
                output.WriteLine();
                output.WriteCommandText($" Cert Issuer: {info.cert.IssuerName.Name}");
                output.WriteLine();
                output.WriteCommandText($" Cert Serial Number: {info.cert.SerialNumber}");
                output.WriteLine();
                output.WriteCommandText($" Cert Chain Status: {info.status.ToString()}");
                output.WriteLine();
                output.WriteCommandText($" Cert PubKey:");
                output.WriteCommandText($" {info.cert.GetPublicKeyString()}");
                output.WriteLine();
                output.WriteCommandText($" Cert Pin:");
                output.WriteCommandText($" {info.pin}");
                output.WriteLine();
                output.WriteCommandText("--");
            }
        }

        private static void WriteSignatureValidationsInfo(string inputPath, TextWriter output)
        {
            output.WriteLine();
            output.WriteCommandText($"WTS is valid signature: {TemplatePackage.ValidateSignatures(inputPath)}");
        }

        private static void WriteCurrentPinConfiguration(TextWriter output)
        {
            var allowedPins = Microsoft.Templates.Core.Configuration.Current.AllowedPublicKeysPins;

            output.WriteLine();
            output.WriteLine("TOOL CONFIG:");

            var validationType = "Cert Chain";
            if (allowedPins.Count > 0)
            {
                output.WriteCommandText($"Pins configured ({allowedPins.Count}):");
                validationType = "Cert Pins";
                for (int i = 0; i < allowedPins.Count; i++)
                {
                    output.WriteCommandText($"{(char)('A' + i)}) {allowedPins[i]}");
                }
            }
            else
            {
                output.WriteCommandText($"No cert pins configured pins in the tool.");
            }

            output.WriteLine();
            output.WriteCommandText($"Signature validation type: {validationType}");
        }

         private static void MakeCopy(string prepareDir, string version, TextWriter output, string resultDir, List<DirectoryInfo> toCopy)
        {
            output.WriteLine();
            output.WriteCommandText("Copying directories...");
            output.WriteLine();

            int countDirs = 0;
            int countFiles = 0;

            bool consoleAvailable = CheckConsole();
            if (consoleAvailable)
            {
                Console.CursorVisible = false;
            }

            toCopy.ForEach(d =>
            {
                foreach (var file in d.GetFiles("*", SearchOption.TopDirectoryOnly))
                {
                    Fs.SafeCopyFile(file.FullName, d.FullName.Replace(prepareDir, resultDir), true);
                    countFiles++;
                }
                if (consoleAvailable)
                {
                    output.WriteLine($"   {++countDirs}/{toCopy.Count}...");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
            });
            output.WriteLine();

            if (consoleAvailable)
            {
                Console.CursorVisible = true;
            }

            File.WriteAllText(Path.Combine(resultDir, "version.txt"), version, System.Text.Encoding.UTF8);

            output.WriteLine();
            output.WriteCommandText($"Preparation for directory '{prepareDir}' done in '{resultDir}' ({countDirs} dirs and {countFiles} files).");
        }

        private static bool CheckConsole()
        {
            try
            {
                Console.CursorVisible = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void PrepareResultDir(TextWriter output, string resultDir)
        {
            if (Directory.Exists(resultDir))
            {
                output.WriteCommandText("The target directory already exists, cleaning...");
                Fs.SafeDeleteDirectory(resultDir);
            }
            else
            {
                Fs.EnsureFolder(resultDir);
            }
        }

        private static void ShowDirectoriesInfo(bool verbose, TextWriter output, List<Regex> exclusionFilters, List<string> excludedDirs, List<DirectoryInfo> includedDirs)
        {
            if (exclusionFilters.Count > 0)
            {
                output.WriteLine();
                output.WriteCommandText("EXCLUDED DIRS:");
                output.WriteLine();
                excludedDirs.ForEach(d => output.WriteCommandText(d));
                output.WriteLine($"Total excluded dirs: {excludedDirs.Count}");
                output.WriteLine();
            }

            if (verbose)
            {
                output.WriteLine();
                output.WriteCommandText("INCLUDED DIRS:");
                output.WriteLine();
                includedDirs.ForEach(d => output.WriteCommandText(d.FullName));
                output.WriteLine($"Total included dirs: {includedDirs.Count}");
                output.WriteLine();
            }
        }

        private static void ApplyFilters(List<Regex> exclusionFilters, List<string> excludedDirs, List<DirectoryInfo> includedDirs, List<DirectoryInfo> allDirs)
        {
            if (exclusionFilters.Count > 0)
            {
                foreach (var subDir in allDirs)
                {
                    bool include = false;
                    foreach (var regex in exclusionFilters)
                    {
                        include = !regex.IsMatch(subDir.Name);
                        if (!include)
                        {
                            break;
                        }
                    }

                    if (include)
                    {
                        includedDirs.Add(subDir);
                    }
                    else
                    {
                        excludedDirs.Add(subDir.FullName);
                    }
                }
            }
        }

        private static List<Regex> GetExclusionFilters(IEnumerable<string> exclusions, TextWriter output)
        {
            List<Regex> exclusionFilters = new List<Regex>();
            foreach (var exclusion in exclusions)
            {
                if (!string.IsNullOrWhiteSpace(exclusion))
                {
                    exclusionFilters.Add(new Regex(exclusion, RegexOptions.Compiled & RegexOptions.IgnoreCase & RegexOptions.CultureInvariant));
                    output.WriteCommandText($"The regex {exclusion} will be applied to the target directory.");
                }
            }

            return exclusionFilters;
        }
    }
}
