using Flashy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Flashy.API
{
    public class SetAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/sets", async (FlashyDbContext db, Set flashcardSet) =>
            {
                await db.Sets.AddAsync(flashcardSet);
                await db.SaveChangesAsync();
                return Results.Created($"/sets/{flashcardSet.Id}", flashcardSet);
            });

            app.MapGet("/sets", async (FlashyDbContext db) =>
            {
                var sets = await db.Sets.ToListAsync();
                return Results.Ok(sets);
            });

            app.MapDelete("/sets/{id}", async (FlashyDbContext db, int id) =>
            {
                var flashcardSet = await db.Sets.FindAsync(id);
                if (flashcardSet == null)
                {
                    return Results.NotFound();
                }

                db.Sets.Remove(flashcardSet);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
