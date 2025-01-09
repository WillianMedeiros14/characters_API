using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using characters_API.Data;
using characters_API.Data.Dtos;
using characters_API.Models;
using Microsoft.AspNetCore.Authorization;


namespace characters_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private CharacterContext _context;
    private IMapper _mapper;

    public CharacterController(CharacterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um personagem ao banco de dados
    /// </summary>
    /// <param name="characterDto">Objeto com os campos necessários para criação de um personagem</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddCharacter([FromBody] CreateCharacterDto characterDto)
    {


        CharacterModel character = _mapper.Map<CharacterModel>(characterDto);
        _context.Characters.Add(character);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
    }

    /// <summary>
    /// Buscar todos os personagens do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre os personagens</response>

    [HttpGet]
    [Authorize]
    public IEnumerable<ReadCharacterDto> GetAllCharacters([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Characters
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .ProjectTo<ReadCharacterDto>(_mapper.ConfigurationProvider)
            .ToList();
    }


    /// <summary>
    /// Buscar um personagem por id do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre os personagem</response>
    /// <response code="404">Caso o personagem não seja encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadCharacterDto))]
    public IActionResult GetCharacterById(int id)
    {
        var product = _context.Characters.FirstOrDefault(product => product.Id == id);
        if (product == null) return NotFound();
        var productDto = _mapper.Map<ReadCharacterDto>(product);
        return Ok(productDto);
    }

    /// <summary>
    /// Atualizar um personagem por id do banco de dados
    /// </summary>
    /// <param name="id">O ID do personagem a ser atualizado</param>
    /// <param name="updateCharacterDto">Objeto com os campos necessários para atualização de um personagem</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    /// <response code="404">Caso o personagem não seja encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateProduct(int id, [FromBody] UpdateCharacterDto updateCharacterDto)
    {
        var product = _context.Characters.FirstOrDefault(product => product.Id == id);
        if (product == null) return NotFound();
        _mapper.Map(updateCharacterDto, product);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletar um personagem por id do banco de dados
    /// </summary>
    /// <param name="id">O ID do personagem a ser deletado</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso deleção seja feita com sucesso</response>
    /// <response code="404">Caso o personagem não seja encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Characters.FirstOrDefault(product => product.Id == id);
        if (product == null) return NotFound();
        _context.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }

}
