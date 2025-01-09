

using characters_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace characters_API.Data;

public class CharacterContext : IdentityDbContext<UserModel>
{
    public CharacterContext(DbContextOptions<CharacterContext> opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

    public DbSet<CharacterModel> Characters { get; set; }
}