using GolfCourseManagement.DTOs;
using GolfCourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly GolfCourseManagementDbContext _context;

        public CustomerController(GolfCourseManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomers()
        {
            var customers = await _context.Customers.Select(c => new CustomerDTO()
            {
                ID = c.ID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber
            }).ToListAsync();
            return customers;
        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int customerID)
        {
            var customer = await _context.Customers.Where(c => c.ID == customerID).FirstOrDefaultAsync();
            if (customer != null)
            {
                return new CustomerDTO()
                {
                    ID = customer.ID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber
                };
            }
            else
            {
                return BadRequest($"Cannot find customer with id {customerID}");
            }
        }

        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(CustomerDTO updatedCustomer)
        {
            var customer = await _context.Customers.Where(c => c.ID == updatedCustomer.ID).FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                customer.PhoneNumber = updatedCustomer.PhoneNumber;
                customer.Address = updatedCustomer.Address;
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return new CustomerDTO()
                {
                    ID = customer.ID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber
                };
            }
            else
            {
                return BadRequest($"Cannot find customer with id {updatedCustomer.ID}");
            }
        }

        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerDTO model)
        {
            var customer = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerDTO()
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber
            };
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int customerID)
        {
            var customer = await _context.Customers
                .Where(c => c.ID == customerID)
                .FirstOrDefaultAsync();
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest($"Cannot find customer with id {customerID}");
            }

            return new CustomerDTO()
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
