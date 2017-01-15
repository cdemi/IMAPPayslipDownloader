using Newtonsoft.Json;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAPPayslipDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText("settings.json"));
            using (ImapClient Client = new ImapClient(settings.Host, settings.Port, settings.Username, settings.Password, ssl: settings.SSL))
            {
                Console.WriteLine("We are connected!");
            }
        }
    }
}
