using Flashy.DTOs;
using Flashy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Flashy.API
{
    public class UserAPI
    { 
        public static void Map(WebApplication app)
        {
            app.MapPost("/checkuser", async (FlashyDbContext db, UserAuthDto userAuthDto) =>
            {
                var userUid = await db.Users.SingleOrDefaultAsync(u => u.Uid == userAuthDto.Uid);

                if (userUid == null)
                {
                    return Results.NotFound();
                }
                else
                {
<<<<<<< Updated upstream
                    return Results.Ok();
=======
                    return Results.Ok(userUid);
>>>>>>> Stashed changes
                }
            });

            app.MapPost("/register", async (FlashyDbContext db, User user) =>
            {
                var userInDb = await db.Users.SingleOrDefaultAsync(u => u.Uid == user.Uid);

                if (userInDb != null)
                {
                    return Results.BadRequest("User already exists");
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Results.Created($"/user/{user.Id}", user);
                }
            });

            app.MapGet("/users/{id}", async (FlashyDbContext db, int id) =>
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.Id == id);
                return user;
            });

            app.MapGet("/users", async (FlashyDbContext db) =>
            {
                var users = await db.Users.ToListAsync();
                return Results.Ok(users);
            });
        }
    }
}
