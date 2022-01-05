using System.ComponentModel.DataAnnotations;

namespace finance_api.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public UserFullName FullName { get; set; }
        public DateTime CreationDate { get; set; }
        public List<BusinessAccount> BusinessAccounts { get; set; }
        public PersonalAccount PersonalAccount { get; set; }
        public FamilyAccount FamilyAccount { get; set; }
        public List<UserRole> Roles { get; set; }
        public UserAuthorization Authorization { get; set; }
        public bool Deleted { get; set; }
    }

    public class UserFullName
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        public bool Deleted { get; set; }
    }

    public class UserRole
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }

    public class UserAuthorization
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime TokenUpdateDate { get; set; }
        public Guid AccessToken { get; set; }
        public bool IsBlocked { get; set; }
        public bool Deleted { get; set; }
    }
}