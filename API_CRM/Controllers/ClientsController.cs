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
    }
}