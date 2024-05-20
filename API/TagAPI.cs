using Flashy.DTOs;
using Flashy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Flashy.API
{
    public class TagAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/tags", async (FlashyDbContext db, Tag tag) =>
            {
                await db.Tags.AddAsync(tag);
                await db.SaveChangesAsync();
                return Results.Created($"/tags/{tag.Id}", tag);
            });

            app.MapGet("/tags", async (FlashyDbContext db) =>
            {
                var tags = await db.Tags.ToListAsync();
                return Results.Ok(tags);
            });

            app.MapPut("/tags/{id}", async (FlashyDbContext db, int id, UpdateTagDto updatedTag) =>
            {
                var tag = await db.Tags.FindAsync(id);
                if (tag == null)
                {
                    return Results.NotFound();
                }

                tag.Label = updatedTag.Label;

                await db.SaveChangesAsync();
                return Results.Ok(tag);
            });

            app.MapDelete("/tags/{id}", async (FlashyDbContext db, int id) =>
            {
                var tag = await db.Tags.FindAsync(id);
                if (tag == null)
                {
                    return Results.NotFound();
                }

                db.Tags.Remove(tag);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
