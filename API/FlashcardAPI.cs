using Flashy.DTOs;
using Flashy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Flashy.API
{
    public class FlashcardAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/flashcards", async (FlashyDbContext db, Flashcard flashcard) =>
            {
                await db.Flashcards.AddAsync(flashcard);
                await db.SaveChangesAsync();
                return Results.Created($"/flashcard/{flashcard.Id}", flashcard);
            });

            app.MapGet("/flashcards", async (FlashyDbContext db) =>
            {
                var flashcards = await db.Flashcards.ToListAsync();
                return Results.Ok(flashcards);
            });

            app.MapPut("/flashcards/{id}", async (FlashyDbContext db, int id, UpdateFlashcardDto updatedFlashcard) =>
            {
                var flashcard = await db.Flashcards.FindAsync(id);

                if (flashcard == null)
                {
                    return Results.NotFound("Flashcard not found");
                }

                // only updates information that is changed
                if(!string.IsNullOrEmpty(updatedFlashcard.Question)) flashcard.Question = updatedFlashcard.Question;
                if(!string.IsNullOrEmpty(updatedFlashcard.Answer)) flashcard.Answer = updatedFlashcard.Answer;

                await db.SaveChangesAsync();
                return Results.Ok(flashcard);
            });

            app.MapDelete("/flashcards/{id}", async (FlashyDbContext db, int id) =>
            {
                var flashcard = await db.Flashcards.FindAsync(id);
                if (flashcard == null)
                {
                    return Results.NotFound();
                }

                db.Flashcards.Remove(flashcard);
                await db.SaveChangesAsync();
                return Results.Ok($"Flashcard ID {id} deleted");
            });

            app.MapGet("/flashcards/user/{userId}", (FlashyDbContext db, int userId) =>
            {
                var userFlashcards = db.Flashcards.Where(f => f.UserId == userId);

                return Results.Ok(userFlashcards);
            });

            app.MapGet("/flashcards/{id}", async (FlashyDbContext db, int id) =>
            {
                var flashcard = await db.Flashcards
                                        .Include(f => f.Tags)
                                        .SingleOrDefaultAsync(s => s.Id == id);

                if (flashcard == null)
                {
                    return Results.NotFound("Flashcard not found");
                }

                var response = new
                {
                    id = flashcard.Id,
                    userId = flashcard.UserId,
                    setId = flashcard.SetId,
                    question = flashcard.Question,
                    answer = flashcard.Answer,
                    dateCreated = flashcard.DateCreated,
                    tags = flashcard.Tags?.Select( tag => new
                    {
                        id = tag.Id,
                        label = tag.Label,
                    })

                };

                return Results.Ok(response);
            });
        }
    }
}
