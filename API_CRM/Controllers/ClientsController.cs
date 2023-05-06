using API_CRM.Class;
using API_ERP.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ICRMApiService _CRMApiService;
        public ClientsController(ICRMApiService CRMApiService)
        {
            _CRMApiService = CRMApiService;
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
        public async Task<IActionResult> GetCustomers()
        {
            List<Customer> customers = await _CRMApiService.GetCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
    }
}