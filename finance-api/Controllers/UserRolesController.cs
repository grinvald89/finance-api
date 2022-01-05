using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/user-roles")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public UserRolesController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<UserRole> Get()
        {
            return _dbContext.UserRoles.ToList().FindAll(u => !u.Deleted);
        }

        [HttpGet("{id}")]
        public UserRole? Get(Guid id)
        {
            List<UserRole> roles = _dbContext.UserRoles.ToList().FindAll(u => !u.Deleted);

            return roles.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(UserRoleCreateRequest request)
        {
            UserRole role = new UserRole()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.UserRoles.Add(role);
            _dbContext.SaveChanges();

            return Ok(role);
        }

        public class UserRoleCreateRequest
        {
            public string Name { get; set; }
        }
    }
}
