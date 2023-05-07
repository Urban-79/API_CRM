using API_ERP.Class;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace API_CRM.Class
{   
    public class CRMApiService : ICRMApiService
    {
        private readonly HttpClient _httpClient;
        public CRMApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://615f5fb4f7254d0017068109.mockapi.io/api/v1/");
        }

        public async Task<Customer> GetCustomerAsync(int  id)
        {
            var response = await _httpClient.GetAsync($"customers/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Customer>(json);
            }
            return null;
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync($"customers/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Customer>>(json);
            }
            return null;
        }
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
    public interface ICRMApiService
    {
        Task<Customer> GetCustomerAsync(int id);
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Customer> DeleteCustomerAsync(int id);
    }

}
