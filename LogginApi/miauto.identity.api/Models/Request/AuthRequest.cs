using System.ComponentModel.DataAnnotations;

namespace miauto.identity.api.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string email { get; set;  }
        [Required]
        public string password { get; set; }
    }
}
