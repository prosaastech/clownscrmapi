using System.Net.Mail;
using System.Net;
using OtpNet;

namespace ClownsCRMAPI.CustomModels
{
    public static class Extensions
    {
        public enum enumContractStatus
        {
            BookedApprovedConfirmation = 1,
            BookedApproved = 2,
            Booked = 3,
            Quoted = 4,
            Cancelled = 5
        }

        public static void SendEmail(string SendTo,int? CustomerId, int? ContractId)
        {
            try
            {
                var fromAddress = new MailAddress("motanfaisal67@gmail.com", "Clowns");
                var toAddress = new MailAddress(SendTo, "To Faisal");
                const string fromPassword = "sagh lvmx ybmo tqvq";
                const string subject = "Clowns and Contract Verification";
                 string imageUrl = "https://i.ibb.co/TKbFbTT/Clowns-Email2.png"; //"https://i.ibb.co/qC1b6Vx/Clowns-Email.png"; // Use the public URL of your image
                string linkUrl = $"http://localhost:3000/ClientVerification/{CustomerId}/contract/{ContractId}";// $"http://localhost:3000/Customer/{CustomerId}/contract/{ContractId}"; // URL for the "Verify Your Contract" button

                // HTML body with image and clickable link
                string htmlBody = $@"
            <html>
            <body>
                <h2>Verify Your Contract</h2>
                <p>Click the image below to verify your contract:</p>
                <a href='{linkUrl}'>
                    <img src='{imageUrl}' alt='Clowns and Verify Button' style='width:100%;height:auto;' />
                </a>
            </body>
            </html>
        ";

                //http://localhost:3000/Customer
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                  
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private const string SecretKey = "Ht43mkcsrmvSSd";

        public static string GenerateToken()
        {
            var totp = new Totp(Base32Encoding.ToBytes(SecretKey));
            return totp.ComputeTotp();
        }
        public static void SendTokenEmail(string SendTo, int? CustomerId, int? ContractId,string Token)
        {
            try
            {
                if (CustomerId == null 
                    || CustomerId == 0
                    || ContractId == null
                    || ContractId == 0) {
                    return;
                } 

                var fromAddress = new MailAddress("motanfaisal67@gmail.com", "Clowns");
                var toAddress = new MailAddress(SendTo, "To Faisal");
                const string fromPassword = "sagh lvmx ybmo tqvq";
                const string subject = "Clowns Verification Code";
               // string Token = GenerateToken();
              
                // HTML body with image and clickable link
                string htmlBody = $@"
            <html>
            <body>
                <h2>Verification Token:{Token}</h2>
                 
             
            </body>
            </html>
        ";

                //http://localhost:3000/Customer
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {

            }
        }


        // For nullable bool
        public static bool GetValueOrDefault(this bool? value, bool defaultValue = false)
        {
            return value ?? defaultValue;
        }

        // For nullable int
        public static int GetValueOrDefault(this int? value, int defaultValue = 0)
        {
            return value ?? defaultValue;
        }
        public static decimal GetValueOrDefault(this decimal? value, int defaultValue = 0)
        {
            return value ?? defaultValue;
        }

        // For nullable string
        public static string GetValueOrDefault(this string value, string defaultValue = "")
        {
            return value ?? defaultValue;
        }

        // For nullable DateTime
        public static DateTime GetValueOrDefault(this DateTime? value, DateTime defaultValue)
        {
            return value ?? defaultValue;
        }

        // Add more extension methods as needed
    }

}
