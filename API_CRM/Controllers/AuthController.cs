using API_CRM.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using MimeKit;

namespace API_CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly UtilsService _utils;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.AdresseMail = request.AdresseMail;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDto request)
        {
            if (user.AdresseMail != request.AdresseMail)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("erreur password.");
            }

            string token = CreateToken(user);
            GenerateQRCode(token, "qrcode.png");
            //SendEmailWithAttachment("pydima@gmail.com", "pydima@gmail.com", "QRCODE JWT", "testtt", "", "");

            return Ok(new { token = token });
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.AdresseMail),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        /// <summary>
        /// Générer un QRCode
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>

        public static void GenerateQRCode(string data, string fileName)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            qrCodeImage.Save(fileName, ImageFormat.Png);
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

            SmtpClient smtp = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
            smtp.UseDefaultCredentials = false;
            //smtp.Credentials =new System.Net.NetworkCredential(fromAddress,password); // Remplacez "votre_mot_de_passe" par votre propre mot de passe
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}
