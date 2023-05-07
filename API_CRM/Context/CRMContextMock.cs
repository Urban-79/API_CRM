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
        public async Task<Customer> AddCustomerAsync(Customer addedCustomer)
        {
            if (customers != null)
            {
                Customer existingCustomer = customers.FirstOrDefault(c => c.Id == addedCustomer.Id);
                if (existingCustomer != null)
                {
                    return null;
                }
                List<Customer> listCustomers = await GetCustomersAsync();
                Customer lastCustomer = listCustomers.OrderByDescending(c => c.Id).FirstOrDefault();
                int newCustomerId = (lastCustomer != null) ? lastCustomer.Id + 1 : 1;

                addedCustomer.Id = newCustomerId;
                customers.Add(addedCustomer);

                // save changes to file
                File.WriteAllText(".\\Data\\customers.json", JsonConvert.SerializeObject(customers));

                return addedCustomer;
            }
            else
            {
                 return null;
            }
        }
        public async Task<Customer> UpdateCustomerAsync(Customer updatedCustomer)
        {
            if (updatedCustomer != null)
            {
                Customer customerToUpdate = customers.FirstOrDefault(c => c.Id == updatedCustomer.Id); 
                if (customerToUpdate != null)
                {
                    customers.Remove(customerToUpdate);
                    customers.Add(updatedCustomer);
                    File.WriteAllText(".\\Data\\customers.json", JsonConvert.SerializeObject(customers));
                    return updatedCustomer;
                }
            }
            return null;
        }
        public async Task<Customer> DeleteCustomerAsync(int id)
        {
            if (id != null)
            {
                Customer customerToDelete = customers.FirstOrDefault(c => c.Id == id);
                if (customerToDelete != null)
                {
                    customers.Remove(customerToDelete);
                    File.WriteAllText(".\\Data\\customers.json", JsonConvert.SerializeObject(customers));
                    return customerToDelete;
                }
            }
            return null;
        }
    }
}
