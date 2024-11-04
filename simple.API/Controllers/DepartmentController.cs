using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple.API.Domains;

namespace simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Department> GetList() => [.. _context.Departments];

        [HttpGet("{id}")]
        public Department? GetById(int id) => _context.Departments.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public bool Create(Department model)
        {
            try
            {
                _context.Departments.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("{id}")]
        public bool Update(int id, Department model)
        {
            try
            {
                if (id != model.Id) return false;
                var domain = _context.Departments.FirstOrDefault(x => x.Id == model.Id);
                if (domain == null) return false;
                domain.Name = model.Name;
                _context.Departments.Update(domain);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                var domain = _context.Departments.FirstOrDefault(x => x.Id == id);
                if (domain == null) return false;
                _context.Departments.Remove(domain);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
