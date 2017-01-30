﻿using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Templates.Core.Extensions;

namespace Microsoft.Templates.Core.Diagnostics
{
    public class TelemetryService : IHealthWriter, IDisposable
    {
        public bool IsEnabled { get; private set; }

        private Configuration _currentConfig;
        private TelemetryClient _client { get; set; }

        public static TelemetryService _current;
        public static TelemetryService Current
        {
            get
            {
                if(_current == null)
                {
                    _current = new TelemetryService(Configuration.Current);
                }
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        private TelemetryService(Configuration config)
        {
            _currentConfig = config ?? throw new ArgumentNullException("config");
            IntializeTelemetryClient();
        }

        public static void SetConfiguration(Configuration config)
        {
            _current = new TelemetryService(config);
        }

        public async Task TrackEventAsync(string eventName, Dictionary<string, string> properties = null, Dictionary<string, double> metrics = null)
        {
            await SafeExecuteAsync(() => _client.TrackEvent(eventName, properties, metrics)).ConfigureAwait(false);
        }

        public async Task TrackExceptionAsync(Exception ex, Dictionary<string, string> properties = null, Dictionary<string, double> metrics = null)
        {
            await SafeExecuteAsync(() => _client.TrackException(ex, properties, metrics)).ConfigureAwait(false);
        }

        public async Task WriteTraceAsync(TraceEventType eventType, string message, Exception ex = null)
        {
            //Trace events will not be forwarded to the remote service
            await Task.Run(() => { });
        }

        public async Task WriteExceptionAsync(Exception ex, string message = null)
        {
            await TelemetryService.Current.TrackExceptionAsync(ex);
        }

        public bool AllowMultipleInstances()
        {
            return false;
        }

        private void IntializeTelemetryClient()
        {
            try
            {
                _client = new TelemetryClient()
                {
                    InstrumentationKey = _currentConfig.RemoteTelemetryKey
                };

                if (RemoteKeyAvailable())
                {
                    string userToTrack = Obfuscate(Environment.UserName); //TODO: Obfuscate this info??
                    string machineToTrack = Obfuscate(Environment.MachineName);
                    // Set session data
                    _client.Context.User.Id = userToTrack;
                    _client.Context.Device.Id = machineToTrack;
                    _client.Context.Cloud.RoleInstance = machineToTrack;
                    _client.Context.Cloud.RoleName = "VSIX Instance";
                    _client.Context.Session.Id = Guid.NewGuid().ToString();
                    //_client.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
                    _client.Context.Component.Version = GetVersion();
                    _client.Context.Properties.Add("Application FileVersion", GetFileVersion());
                    

                    _client.TrackEvent(TelemetryEvents.SessionStarted);

                    IsEnabled = true;
#if DEBUG
                    TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
#endif
                }
                else
                {
                    IsEnabled = false;
                    TelemetryConfiguration.Active.DisableTelemetry = true;
                }
            }
            catch (Exception ex)
            {
                IsEnabled = false;
                TelemetryConfiguration.Active.DisableTelemetry = true;
                Trace.TraceError($"Exception instantiating TelemetryClient:\n\r{ex.ToString()}");
            }
        }

        private async Task SafeExecuteAsync(Action action)
        {
            try
            {
                var task = Task.Run(() => {
                    action();
                });
                await task;
            }
            catch (AggregateException aggEx)
            {
                foreach (Exception ex in aggEx.InnerExceptions)
                {
                    Trace.TraceError("Error tracking telemetry: {0}", ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error tracking telemetry: {0}", ex.ToString());
            }
        }

        public async Task FlushAsync()
        {
            await SafeExecuteAsync( async () => {
                if (_client != null)
                {
                    _client.TrackEvent(TelemetryEvents.SessionEnded);
                    _client.Flush();
                    _client = null;
                }
                await Task.Delay(1000);
            });
        }

        public void Flush()
        {
            try
            {
                if (_client != null)
                {
                    _client.TrackEvent(TelemetryEvents.SessionEnded);
                    _client.Flush();
                    System.Threading.Thread.Sleep(1000);

                    _client = null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error tracking telemetry: {0}", ex.ToString());
            }
        }

        private string Obfuscate(string data)
        {
            string result = String.Empty;
            try
            {
                byte[] b64data = Encoding.UTF8.GetBytes(data);

                using (MD5 md5Hash = MD5.Create())
                {
                    result = GetMd5Hash(md5Hash, b64data);
                }
            }
            catch (Exception ex)
            {
                AppHealth.Current.Error.TrackAsync("Error obfuscating data", ex).FireAndForget();
            }
            return result.ToUpper();
        }

        static string GetMd5Hash(MD5 md5Hash, byte[] inputData)
        {
            byte[] data = md5Hash.ComputeHash(inputData);

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }


        private bool RemoteKeyAvailable()
        {
            return Guid.TryParse(_currentConfig.RemoteTelemetryKey, out var aux);
        }

        private static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString(); 
        }

        private static string GetFileVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }


        ~TelemetryService()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources 
                Flush();
            }
            //free native resources if any.
        }
    }
}
