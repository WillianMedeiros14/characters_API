
using System.ComponentModel.DataAnnotations;

namespace characters_API.Models
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "O campo de nome de usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome de usuário não pode ter mais que 50 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo de email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo de senha é obrigatório.")]
        [StringLength(100, ErrorMessage = "A senha não pode ter mais que 100 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo de confirmação de senha é obrigatório.")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
