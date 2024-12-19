using System.ComponentModel.DataAnnotations;

namespace characters_API.Data.Dtos
{
    public class UpdateCharacterDto
    {
        [Required(ErrorMessage = "O Nome do personagem é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo race é obrigatório.")]
        public string Race { get; set; }

        [Required(ErrorMessage = "O campo urlImage é obrigatório.")]
        public string UrlImage { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo strength é obrigatório.")]
        [Range(1, 5, ErrorMessage = "O valor de strength deve ser entre 1 e 5.")]
        public int Strength { get; set; }
    }
}
