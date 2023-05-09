using API_CRM.Class;
using API_ERP.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        public static User user = new User();
        private readonly ICRMApiService _CRMApiService;
        private readonly IConfiguration _configuration;
        public ClientsController(ICRMApiService CRMApiService, IConfiguration configuration)
        {
            _CRMApiService = CRMApiService;
            _configuration = configuration;
        }
        /// <summary>
        /// Get Client
        /// </summary>
        /// <param name="id">Id du client</param>
        /// <returns>test</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer customer = await _CRMApiService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        /// <summary>
        /// Get All Clients
        /// </summary>
        /// <returns>test</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _CRMApiService.GetCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        /// <summary>
        /// Add Clients
        /// </summary>
        /// <param name="addedCustomer">object client</param>
        /// <returns>test</returns>

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer addedCustomer)
        {
            Customer customers = await _CRMApiService.AddCustomerAsync(addedCustomer);
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="id">id client</param>
        /// <returns>test</returns>

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Customer customers = await _CRMApiService.DeleteCustomerAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="updatedCustomer">object client</param>
        /// <returns>test</returns>

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer updatedCustomer)
        {
            Customer customers = await _CRMApiService.UpdateCustomerAsync(updatedCustomer);
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            List<Customer> listCustomers = await _CRMApiService.GetCustomersAsync();
            int lastCustomerId = listCustomers.Last().Id +1;
            await _CRMApiService.AddCustomerAsync(new Customer
            {
                CreatedAt = DateTime.UtcNow,
                Name = "string",
                Username = "string",
                FirstName = "string",
                LastName = "string",
                Address = new Address { PostalCode = "string", City = "string" },
                Profile = new Profile { FirstName = "string", LastName = "string" },
                Company = new Company { CompanyName = "string" },
                Id = lastCustomerId,
                Orders = new List<Order> {}
            });

            user.Id = lastCustomerId;
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
                new Claim("email", user.AdresseMail),
                new Claim("id", user.Id.ToString()),
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
    }
}