using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using QRCoder;

namespace API_CRM.Class
{
    public static class Utils
    {
        /// <summary>
        /// Générer un QRCode
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public static String GenerateQRCode(string data, string fileName)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            qrCodeImage.Save(fileName, ImageFormat.Png);
            return fileName;
        }

        /// <summary>
        /// Envoi d'un email
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="fileName"></param>
        public static void SendEmailWithAttachment(string fromAddress, string toAddress, string subject, string body, string fileName, string password)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromAddress);
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;

            // Ajouter la pièce jointe
            //Attachment attachment = new Attachment(fileName);
            //message.Attachments.Add(attachment);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.UseDefaultCredentials = false;
            //smtp.Credentials =new System.Net.NetworkCredential(fromAddress,password); // Remplacez "votre_mot_de_passe" par votre propre mot de passe
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
    public interface UtilsService
    {
        String GenerateQRCode(string data, string fileName);
        void SendEmailWithAttachment(string fromAddress, string toAddress, string subject, string body, string fileName, string password);

    }
}