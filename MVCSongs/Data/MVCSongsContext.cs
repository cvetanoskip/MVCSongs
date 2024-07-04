using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSongs.Areas.Identity.Data;

namespace MVCSongs.Data
{
    public class MVCSongsContext : IdentityDbContext<MVCSongsUser>
    {
        public MVCSongsContext (DbContextOptions<MVCSongsContext> options)
            : base(options)
        {
        }

        public DbSet<MVCSongs.Models.Artist> Artist { get; set; } = default!;
        public DbSet<MVCSongs.Models.Genre> Genre { get; set; } = default!;
        public DbSet<MVCSongs.Models.Review> Review { get; set; } = default!;
        public DbSet<MVCSongs.Models.SongArtist> SongArtist { get; set; } = default!;
        public DbSet<MVCSongs.Models.Song> Song { get; set; } = default!;
        public DbSet<MVCSongs.Models.SongsGenre> SongsGenre { get; set; } = default!;
        public DbSet<MVCSongs.Models.UserSongs> UserSongs { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
