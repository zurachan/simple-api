using Microsoft.AspNetCore.Mvc;
using simple.API.Domains;

namespace simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private AppDbContext _context;
        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Role> GetList() => [.. _context.Roles];

        [HttpGet("{id}")]
        public Role? GetById(int id) => _context.Roles.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public bool Create(Role model)
        {
            try
            {
                _context.Roles.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("{id}")]
        public bool Update(int id, Role model)
        {
            try
            {
                if (id != model.Id) return false;
                var domain = _context.Roles.FirstOrDefault(x => x.Id == model.Id);
                if (domain == null) return false;
                domain.Name = model.Name;
                _context.Roles.Update(domain);
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
                var domain = _context.Roles.FirstOrDefault(x => x.Id == id);
                if (domain == null) return false;
                _context.Roles.Remove(domain);
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
