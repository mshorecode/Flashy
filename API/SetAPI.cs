using Flashy.DTOs;
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
                    return Results.NotFound("Set not found");
                }

                db.Sets.Remove(flashcardSet);
                await db.SaveChangesAsync();
                return Results.Ok($"Set ID {id} deleted");
            });

            // get set information by id
            app.MapGet("/sets/{id}", async (FlashyDbContext db, int id) =>
            {
                var set = await db.Sets
                                  .Include(s => s.Flashcards)
                                  .SingleOrDefaultAsync(s =>  s.Id == id);

                if (set == null)
                {
                    return Results.NotFound("Set not found");
                }

                var response = new
                {
                    id = set.Id,
                    userId = set.UserId,
                    title = set.Title,
                    description = set.Description,
                    favorite = set.Favorite,
                    dateCreated = set.DateCreated,
                    flashcards = set.Flashcards?.Select( flashcard => new
                    {
                        id = flashcard.Id,
                        setId = set.Id,
                        userId = flashcard.UserId,
                        question = flashcard.Question,
                        answer = flashcard.Answer,
                        dateCreated = flashcard.DateCreated,
                        tags = flashcard.Tags,
                    })

                };

                return Results.Ok(response);
            });

            // add card to set
            app.MapPost("/sets/addcard", async (FlashyDbContext db, AddFlashcardDto addCardDto) => 
            { 
                var set = await db.Sets.Include(s => s.Flashcards).SingleOrDefaultAsync(s => s.Id == addCardDto.SetId);

                if (set == null)
                {
                    return Results.NotFound("Set not found");
                }

                var cardToAdd = await db.Flashcards.SingleOrDefaultAsync(f => f.Id == addCardDto.FlashcardId);

                if (cardToAdd == null)
                {
                    return Results.NotFound("Flashcard not found");
                }

                if (cardToAdd.SetId != null)
                {
                    return Results.BadRequest("Flashcard already belongs to a set");
                }

                cardToAdd.SetId = set.Id;

                set.Flashcards.Add(cardToAdd);
                await db.SaveChangesAsync();
                return Results.Ok(cardToAdd);
            });

            // remove card from set
            app.MapPost("/sets/removecard", async (FlashyDbContext db, RemoveFlashcardDto removeCardDto) =>
            {
                var set = await db.Sets.Include(s => s.Flashcards).SingleOrDefaultAsync(s => s.Id == removeCardDto.SetId);

                if (set == null)
                {
                    return Results.NotFound("Set not found");
                }

                var cardToRemove = await db.Flashcards.SingleOrDefaultAsync(f => f.Id == removeCardDto.FlashcardId);

                cardToRemove.SetId = null;

                await db.SaveChangesAsync();
                return Results.Ok(cardToRemove);
            });
        }
    }
}
