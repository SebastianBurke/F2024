using Microsoft.AspNetCore.Identity;

namespace scbH60Services.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Province { get; set; }
        public string CreditCard { get; set; }
    }
}
