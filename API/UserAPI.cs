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
                    return Results.Ok();
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
        }
    }
}
