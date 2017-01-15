using Newtonsoft.Json;
using S22.Imap;
using System;
using System.IO;
using System.Linq;

namespace IMAPPayslipDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText("settings.json"));
            Console.WriteLine("Connecting to the IMAP Server...");
            using (ImapClient client = new ImapClient(settings.Host, settings.Port, settings.Username, settings.Password, ssl: settings.SSL))
            {
                Console.WriteLine("Connected!");
                Console.WriteLine("Looking for Payslips...");
                var payslipEmailUIDs = client.Search(SearchCondition.All(), settings.Mailbox);
                Console.WriteLine($"Downloading {payslipEmailUIDs.Count()} Emails...");
                foreach (var email in client.GetMessages(payslipEmailUIDs, seen: false, mailbox: settings.Mailbox))
                {
                    var payslipPDF = email.Attachments.SingleOrDefault(a => a.Name.ToLower().Contains(".pdf"));
                    if (payslipPDF != null)
                    {
                        Console.Write($"Found Payslip in \"{email.Subject}\" sent on {email.Date().Value.ToShortDateString()}. ");
                        var payslipPath = settings.LocalPayslipsPath + "\\" + email.Date().Value.ToString("yyyy-MM") + ".pdf";
                        if (File.Exists(payslipPath))
                        {
                            Console.WriteLine("Not Downloading: Payslip already on Local.");
                        }
                        else
                        {
                            Console.Write("Saving PDF. ");
                            using (var localPayslipPDFStream = File.Create(payslipPath))
                            {
                                payslipPDF.ContentStream.Seek(0, SeekOrigin.Begin);
                                payslipPDF.ContentStream.CopyTo(localPayslipPDFStream);
                            }
                            Console.WriteLine("Saved.");
                        }
                    }
                    else
                    {
                        Console.Write($"No payslip found in \"{email.Subject}\" sent on {email.Date().Value.ToShortDateString()}. ");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Done! Press any key to exit");
            Console.ReadKey();
        }
    }
}
