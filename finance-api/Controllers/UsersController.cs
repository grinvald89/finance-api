using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public UsersController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<User> users =
                _dbContext.Users
                    .Include(u => u.Authorization)
                    .Include(u => u.FullName)
                    .Include(u => u.Roles)
                    .ToList();

            return Ok(users);
        }


        [HttpGet("{id}")]
        public User? Get(Guid id)
        {
            List<User> users =
                _dbContext.Users
                    .Include(u => u.Authorization)
                    .Include(u => u.FullName)
                    .Include(u => u.Roles)
                    .ToList().FindAll(c => !c.Deleted);

            return users.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(UserCreateRequest request)
        {
            User user = new User()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Authorization = CreateAuthorization(request.UserName, request.Password),
                CreationDate = DateTime.Now,
                FullName = CreateFullName(request.FirstName, request.LastName, request.MiddleName),
                Roles = GetRolesByIds(request.RoleIds)
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(user);
        }

        /*
        [HttpPost]
        public IActionResult Post(UserCreateRequest request)
        {
            Guid id = Guid.Parse("45e6d487-39b9-48b3-8c78-02ed928cb8f9");
            UserFullName? fullname = _dbContext.UserFullNames.ToList()
                .Find(u => Guid.Equals(u.Id, id));

            if (fullname != null)
            {
                fullname.FirstName = "Admin";
                fullname.LastName = "Admin";
                fullname.MiddleName = "Admin";
                _dbContext.UserFullNames.Update(fullname);
            }

            return Ok(user);
        }
        */

        private UserAuthorization CreateAuthorization(string userName, string password)
        {
            UserAuthorization authorization = new UserAuthorization()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                AccessToken = Guid.NewGuid(),
                IsBlocked = false,
                Password = password,
                TokenUpdateDate = DateTime.Now,
                UserName = userName
            };

            return authorization;
        }

        private UserFullName CreateFullName(string firstName, string lastName, string middleName)
        {
            UserFullName fullName = new UserFullName()
            {
                Deleted = false,
                FirstName = firstName,
                Id = Guid.NewGuid(),
                LastName = lastName,
                MiddleName = middleName
            };

            return fullName;
        }

        private List<UserRole> GetRolesByIds(List<string> roleIds)
        {
            List<UserRole> allRoles = _dbContext.UserRoles.ToList().FindAll(r => !r.Deleted);
            List<UserRole> userRoles = new List<UserRole>();

            roleIds.ForEach(id =>
            {
                Guid roleId = Guid.Parse(id);
                UserRole? userRole = allRoles.Find(role => Guid.Equals(role.Id, roleId));

                if (userRole != null)
                {
                    userRoles.Add(userRole);
                }
            });

            return userRoles;
        }

        public class UserCreateRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Password { get; set; }
            public List<string> RoleIds { get; set; }
            public string UserName { get; set; }
        }
    }
}
