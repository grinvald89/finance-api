using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/users
        [HttpGet]
        public IActionResult Get()
        {
            /*
            UserFullName fullName = new UserFullName("last", "first", "middle");
            PersonalAccount personalAccount = new PersonalAccount("personalAccount");
            FamilyAccount familyAccount = new FamilyAccount("familyAccount");
            UserAuthorization authorization = new UserAuthorization();

            User user = new Models.User()
            {
                Id = Guid.NewGuid(),
                Authorization = authorization,
                BusinessAccounts = new List<BusinessAccount>(),
                CreationDate = DateTime.Now,
                FamilyAccount = familyAccount,
                FullName = fullName,
                PersonalAccount = personalAccount,
                Roles = new List<UserRole>()
            };

            
            _dbContext.Add(user);

            _dbContext.SaveChanges();
            */

            return StatusCode(403, "asd");

            // return _dbContext.Users.ToArray();
        }
    }
}
