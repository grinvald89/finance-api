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
            return _dbContext.UserRoles
                .ToList()
                .FindAll(u => !u.Deleted);
        }

        [HttpGet("{id}")]
        public UserRole? Get(Guid id)
        {
            // Todo: проверка на id и существование пользователя

            List<UserRole> roles =
                _dbContext.UserRoles
                    .ToList()
                    .FindAll(u => !u.Deleted);

            return roles.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(UserRoleRequest request)
        {
            // Todo: Добавить проверку на существавание роли по Name

            UserRole role = new UserRole()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.UserRoles.Add(role);
            _dbContext.SaveChanges();

            return Ok(role);
        }

        [HttpPost]
        public IActionResult Post(UserRoleRequest request)
        {
            // Todo: проверка на id и существование роли

            UserRole role =
                _dbContext.UserRoles
                    .ToList()
                    .FindAll(u => !u.Deleted)
                    .Find(u => Guid.Equals(u.Id, request.Id));

            role.Name = request.Name;

            _dbContext.SaveChanges();

            return Ok(role);
        }

        [HttpDelete]
        public IActionResult Delete(UserRoleIdRequest request)
        {
            // Todo: проверка на id и существование роли

            UserRole role =
                _dbContext.UserRoles
                    .ToList()
                    .Find(u => u.Id == request.Id);

            role.Deleted = true;

            _dbContext.SaveChanges();

            return Ok(role);
        }

        public class UserRoleRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class UserRoleIdRequest
        {
            public Guid Id { get; set; }
        }
    }
}
