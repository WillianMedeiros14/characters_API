using System.ComponentModel.DataAnnotations;

namespace characters_API.Data.Dtos;

public class ReadCharacterDto
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string Race { get; set; }

    public string UrlImage { get; set; }

    public string Description { get; set; }

    public int Strength { get; set; }
}

