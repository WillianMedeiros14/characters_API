using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using characters_API.Data;
using characters_API.Data.Dtos;
using characters_API.Models;
using Microsoft.AspNetCore.Authorization;
using characters_API.Services;
namespace characters_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private CharacterContext _context;
    private IMapper _mapper;

    private UserService _userService;

    public CharacterController(CharacterContext context, IMapper mapper, UserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
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
    public async Task<IResult> AddCharacterAsync([FromBody] CreateCharacterDto characterDto)
    {
        var userName = User.Identity.Name;

        var userId = await _userService.GetUserId(userName);

        CharacterModel character = _mapper.Map<CharacterModel>(characterDto);

        character.UserId = userId;

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        var characterResponseDto = _mapper.Map<ReadCharacterDto>(character);

        var location = Url.Action(nameof(AddCharacterAsync), new { id = character.Id });
        return Results.Created(location, characterResponseDto);
    }

    /// <summary>
    /// Buscar todos os personagens do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre os personagens</response>

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<ReadCharacterDto>> GetAllCharactersAsync([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var userName = User.Identity.Name;

        var userId = await _userService.GetUserId(userName);

        return _context.Characters
            .Where(p => p.UserId == userId)
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadCharacterDto))]
    public async Task<IActionResult> GetCharacterByIdAsync(int id)
    {
        var userName = User.Identity.Name;

        var userId = await _userService.GetUserId(userName);

        var character = _context.Characters.FirstOrDefault(c => c.Id == id && c.UserId == userId);

        if (character == null) return NotFound();
        var characterDto = _mapper.Map<ReadCharacterDto>(character);
        return Ok(characterDto);
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCharacterAsync(int id, [FromBody] UpdateCharacterDto updateCharacterDto)
    {
        var userName = User.Identity.Name;

        var userId = await _userService.GetUserId(userName);

        var character = _context.Characters.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (character == null) return NotFound();
        _mapper.Map(updateCharacterDto, character);
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCharacterAsync(int id)
    {
        var userName = User.Identity.Name;

        var userId = await _userService.GetUserId(userName);

        var character = _context.Characters.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (character == null) return NotFound();
        _context.Remove(character);
        _context.SaveChanges();
        return NoContent();
    }
}
