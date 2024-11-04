using Microsoft.AspNetCore.Mvc;
using simple.API.Domains;

namespace simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<User> GetList() => [.. _context.Users];

        [HttpGet("{id}")]
        public User? GetById(int id) => _context.Users.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public bool Create(User model)
        {
            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("{id}")]
        public bool Update(int id, User model)
        {
            try
            {
                if (id != model.Id) return false;
                var user = _context.Users.FirstOrDefault(x => x.Id == model.Id);
                if (user == null) return false;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Age = model.Age;
                _context.Users.Update(user);
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
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null) return false;
                _context.Users.Remove(user);
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
