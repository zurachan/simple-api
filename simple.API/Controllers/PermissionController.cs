using Microsoft.AspNetCore.Mvc;
using simple.API.Domains;

namespace simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private AppDbContext _context;
        public PermissionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Permission> GetList() => [.. _context.Permissions];

        [HttpGet("{id}")]
        public Permission? GetById(int id) => _context.Permissions.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public bool Create(Permission model)
        {
            try
            {
                _context.Permissions.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("{id}")]
        public bool Update(int id, Permission model)
        {
            try
            {
                if (id != model.Id) return false;
                var domain = _context.Permissions.FirstOrDefault(x => x.Id == model.Id);
                if (domain == null) return false;
                domain.UserId = model.UserId;
                domain.RoleId = model.RoleId;
                domain.DepartmentId = model.DepartmentId;
                _context.Permissions.Update(domain);
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
                var domain = _context.Permissions.FirstOrDefault(x => x.Id == id);
                if (domain == null) return false;
                _context.Permissions.Remove(domain);
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
