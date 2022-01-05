using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        private readonly LoginRequest adminLogin = new LoginRequest()
        {
            Password = "f0l2t%8W",
            UserName = "admin"
        };

        private readonly string adminRoleName = "Admin";

        public AuthenticationController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Post(LoginRequest request)
        {
            bool isUserAdmin = request.UserName == adminLogin.UserName && request.Password == adminLogin.Password;
            if (isUserAdmin)
            {
                return Ok(GetOrCreateAdminUser().Authorization.AccessToken);
            }

            User? user = GetUserByName(request.UserName);
            if (user == null)
            {
                return StatusCode(504, "Такого пользователя не существует!");
            }

            if (user.Authorization.Password != request.Password)
            {
                return StatusCode(503, "Неверный пароль!");
            }

            return Ok(user.Authorization.AccessToken);
        }

        private User GetOrCreateAdminUser()
        {
            User? user = _dbContext.Users
                .Include(u => u.Authorization)
                .ToList()
                .Find(user => user.Authorization.UserName == adminLogin.UserName);

            if (user == null)
            {
                user = CteateAdminUser();
            }

            return user;
        }

        private User CteateAdminUser()
        {
            UserAuthorization authorization = new UserAuthorization()
            {
                AccessToken = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsBlocked = false,
                Password = adminLogin.Password,
                TokenUpdateDate = DateTime.Now,
                UserName = adminLogin.UserName
            };

            FamilyAccount familyAccount = new FamilyAccount()
            {
                Id = Guid.NewGuid(),
                Name = "",
            };

            PersonalAccount personalAccount = new PersonalAccount()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            UserFullName fullName = new UserFullName()
            {
                FirstName = "",
                Id = Guid.NewGuid(),
                LastName = "",
                MiddleName = ""
            };

            User userForDB = new User()
            {
                Authorization = authorization,
                BusinessAccounts = new List<BusinessAccount>(),
                CreationDate = DateTime.Now,
                FamilyAccount = familyAccount,
                FullName = fullName,
                Id = Guid.NewGuid(),
                PersonalAccount = personalAccount,
                Roles = new List<UserRole>() { GetOrCreateAdminRole() }
            };


            _dbContext.Users.Add(userForDB);

            _dbContext.SaveChanges();

            return userForDB;
        }

        private UserRole GetOrCreateAdminRole()
        {
            UserRole? adminRole = _dbContext.UserRoles.ToList().Find(role => role.Name == adminRoleName);

            if (adminRole == null)
            {
                adminRole = new UserRole()
                {
                    Id = Guid.NewGuid(),
                    Name = adminRoleName
                };

                _dbContext.UserRoles.Add(adminRole);

                _dbContext.SaveChanges();
            }

            return adminRole;
        }

        private User? GetUserByName(string userName)
        {
            List<User> users = _dbContext.Users.Include(u => u.Authorization).ToList();

            return users.Find(u => u.Authorization.UserName == userName);
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
