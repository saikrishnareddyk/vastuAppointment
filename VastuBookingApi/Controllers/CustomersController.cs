using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VastuBookingApi.Data;
using VastuBookingApi.Models;

namespace VastuBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly VastuDbContext _context;

        public CustomersController(VastuDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }

        //example-https://localhost:7075/api/Customers/by-place/Hyderabad
        [HttpGet("by-place/{place}")]
        public async Task<ActionResult<List<Customer>>> GetCustomersByPlace(string place)
        {
            if (string.IsNullOrWhiteSpace(place))
            {
                return BadRequest("Place is required");
            }

            var customers = await _context.Customers
                .Where(c => c.Place == place)
                .ToListAsync();

            if (customers.Count == 0)
            {
                return NotFound("No customers found for this place");
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("Customer ID mismatch");
            }

            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }

            existingCustomer.FullName = customer.FullName;
            existingCustomer.MobileNumber = customer.MobileNumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;

            await _context.SaveChangesAsync();

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("Customer deleted successfully");
        }
    }
}