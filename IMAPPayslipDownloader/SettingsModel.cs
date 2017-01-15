using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace IMAPPayslipDownloader
{
    public class SettingsModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
