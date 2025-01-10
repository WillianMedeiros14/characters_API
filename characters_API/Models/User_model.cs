using Microsoft.AspNetCore.Identity;

namespace characters_API.Models
{
    public class UserModel : IdentityUser
    {
        public virtual ICollection<CharacterModel> Characters { get; set; }
    }
}
