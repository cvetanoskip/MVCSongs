using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCSongs.Areas.Identity.Data;
using MVCSongs.Data;

namespace MVCSongs.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<MVCSongsUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            //Add User Role
            var roleUserCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleUserCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("User")); }
            MVCSongsUser user = await UserManager.FindByEmailAsync("admin@song.com");
            if (user == null)
            {
                var User = new MVCSongsUser();
                User.Email = "admin@song.com";
                User.UserName = "admin@song.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCSongsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCSongsContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                
                // Look for any book.
                if (context.Artist.Any() || context.Song.Any() || context.SongsGenre.Any() || context.Genre.Any() || context.Review.Any() || context.UserSongs.Any())
                {
                    return;   // DB has been seeded
                }


                context.Artist.AddRange(
                    new Artist { /*Id = 1, */Name = "Jesper Kyd Jakobson", BirthDate = DateTime.Parse("1972-2-3"), Nationality = "Danish ", Gender = "Male" },
                    new Artist { /*Id = 2, */Name = "Tose Proeski", BirthDate = DateTime.Parse("1981-1-25"), Nationality = "Macedonian", Gender = "Male" },
                    new Artist { /*Id = 3, */Name = "Željko Bebek", BirthDate = DateTime.Parse("1945-12-16"), Nationality = "Bosnian-Croatian", Gender = "Male" },
                    new Artist { /*Id = 4, */Name = "Bijelo Dugme", BirthDate = DateTime.Parse("1974-1-25"), Nationality = "Macedonian", Gender = "Male" },
                    new Artist { /*Id = 5, */Name = "Plavi Orkestar", BirthDate = DateTime.Parse("1983-1-15"),},
                    new Artist { /*Id = 6, */Name = "Orthodox chants", BirthDate = DateTime.Parse("200-1-25"), },
                    new Artist { /*Id = 7, */Name = "Miligram", BirthDate = DateTime.Parse("2004-1-25"), Nationality = "Serbian", Gender = "Male" },
                    new Artist { /*Id = 8, */Name = "Tavitjan Brothers & Garo", BirthDate = DateTime.Parse("1961-1-25"), Nationality = "Macedonian", Gender = "Male" }
                    
                );
                context.SaveChanges();

                context.Song.AddRange(
                    new Song
                    {
                        //Id = 1,
                        Title = "Ezio's Family",
                        Year = 2009,
                        
                        Description = "Soundtrack of Assassin's Creed 2",
                        Publisher = "Jesper Kyd",
                        SongUrl = "https://www.youtube.com/embed/FSVHx23ByhM",
                        AlbumCover = "WarAndPeace.jpg",
                        
                    },
                    new Song
                    {
                        //Id = 2,
                        Title = "Po tebe",
                        Year = 2005,
                        
                        Description = "Po tebe (Macedonian: По тебе) is Macedonian edidion of fifth studio album by the Macedonian singer Toše Proeski. The album was released in Macedonia and subsequently in Serbia and Montenegro, Bosnia and Herzegovina, Croatia and Slovenia under the Serbo-Croatian title Pratim te (Serbo-Croatian Cyrillic: Пратим те).",
                        Publisher = "Tose Proeski",
                        SongUrl = "https://www.youtube.com/embed/-U4h1l_zjfw",
                        AlbumCover = "WarAndPeace.jpg",
                        
                    }
                );

                context.SaveChanges();

                context.Genre.AddRange(
                    new Genre
                    {
                        // Id = 1
                        GenreName = "Chants"
                    },
                    new Genre
                    {
                        // Id = 2
                        GenreName = "Pop"
                    },
                    new Genre
                    {
                        // Id = 3
                        GenreName = "Rock"
                    },
                    new Genre
                    {
                        // Id = 4
                        GenreName = "Classical"
                    }
                );

                context.SaveChanges();

                context.SongsGenre.AddRange(
                    new SongsGenre
                    {
                        SongId = 1,
                        GenreId = 4
                    },
                    new SongsGenre
                    {
                        SongId = 2,
                        GenreId = 2
                    }
                );

                context.SaveChanges();

                context.Review.AddRange(
                    new Review
                    {
                        SongId = 1,
                        AppUser = "Alice",
                        Rating = 10,
                        Comment = "Ezio's Family is a hauntingly beautiful composition that captures the essence of the Assassin's Creed II narrative. Jesper Kyd masterfully blends orchestral strings with a serene melody that evokes a sense of nostalgia and longing. This piece not only complements the game's historical setting but also stands on its own as an emotionally stirring track. It's perfect for moments of introspection or simply unwinding after a long day."

                    },
                    new Review
                    {
                        SongId = 1,
                        AppUser = "John",
                        Rating = 10,
                        Comment = "Jesper Kyd's Ezio's Family is a timeless piece that transcends its origins in the Assassin's Creed II soundtrack. The song's gentle, flowing melody and soothing harmonies create an atmosphere of peace and tranquility. Whether you're a fan of the game or just a lover of beautiful music, this track is sure to resonate with you. Its calming effect makes it an ideal choice for relaxation, meditation, or background music while working."

                    },
                    new Review
                    {
                        SongId = 1,
                        AppUser = "Sarah",
                        Rating = 9,
                        Comment = "The track Ezio's Family by Jesper Kyd beautifully captures the spirit of Renaissance Italy while conveying deep emotional undertones. The use of strings and choir gives the piece a majestic, almost ethereal quality. It's a standout track that enhances the gaming experience, but it's also powerful enough to be appreciated independently. This song is an auditory journey that evokes a sense of both historical grandeur and personal introspection, making it perfect for a reflective mood."

                    },
                    new Review
                    {
                        SongId = 2,
                        AppUser = "Alice",
                        Rating = 10,
                        Comment = "Po tebe by Toše Proeski is a deeply emotional ballad that touches the soul. The powerful vocals and poignant lyrics speak of love and longing, making it a perfect song for moments of reflection. Toše's voice is both soothing and powerful, bringing a unique depth to the song. This track is ideal for anyone who appreciates heartfelt music and emotional storytelling."

                    },
                    new Review
                    {
                        SongId = 2,
                        AppUser = "John",
                        Rating = 9,
                        Comment = "Po tebe is a timeless piece that showcases Toše Proeski's incredible talent. The song's melody is hauntingly beautiful, and the lyrics are filled with passion and sincerity. It's a moving tribute to lost love that resonates with listeners on a deep emotional level. Whether you're a long-time fan of Toše or discovering his music for the first time, Po tebe is sure to leave a lasting impression."

                    },
                    new Review
                    {
                        SongId = 2,
                        AppUser = "Sarah",
                        Rating = 9,
                        Comment = "Toše Proeski's Po tebe is an emotionally resonant song that captures the essence of love and loss. The combination of Toše's expressive vocals and the song's touching lyrics creates a powerful listening experience. This song is perfect for quiet moments of introspection or when you need to connect with your emotions. It's a testament to Toše's artistry and the enduring power of his music."

                    }
                );

                context.SaveChanges();
            }
        }
    }
}
