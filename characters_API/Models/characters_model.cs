using System.ComponentModel.DataAnnotations;

namespace characters_API.Models;

public class CharacterModel
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "O campo race é obrigatório.")]
    public string Race { get; set; }

    [Required(ErrorMessage = "O campo urlImage é obrigatório.")]
    public string UrlImage { get; set; }

    [Required(ErrorMessage = "O campo description é obrigatório.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo strength é obrigatório.")]
    public int Strength { get; set; }
}

