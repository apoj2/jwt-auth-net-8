using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities.Auth
{
    public class User : IdentityUser
    {
        public string FisrtName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public IList<string> Roles { get; set; }
    }
}
