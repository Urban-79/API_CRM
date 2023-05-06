using API_CRM.Class;
using API_ERP.Class;
using Newtonsoft.Json;

namespace API_CRM.Context
{
    public class CRMContextMock : ICRMApiService
    {
        public List<Customer> customers { get; }
        public List<Product> products { get; }
        public CRMContextMock()
        {
            string fileJsonProducts = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, ".\\Data\\products.json"));
            string fileJsonCustomers = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, ".\\Data\\customers.json"));
            products = JsonConvert.DeserializeObject<List<Product>>(fileJsonProducts);
            customers = JsonConvert.DeserializeObject<List<Customer>>(fileJsonCustomers);
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        { 
            return customers.ToList();
        }
    }
}
