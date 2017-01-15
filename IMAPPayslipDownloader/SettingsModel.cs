namespace IMAPPayslipDownloader
{
    public class SettingsModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mailbox { get; set; }
        public string LocalPayslipsPath { get; set; }
    }
}
