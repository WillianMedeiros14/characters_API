

using characters_API.Models;
using Microsoft.EntityFrameworkCore;

namespace characters_API.Data;

public class CharacterContext : DbContext
{
    public CharacterContext(DbContextOptions<CharacterContext> opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


    }

    public DbSet<CharacterModel> Characters { get; set; }
}