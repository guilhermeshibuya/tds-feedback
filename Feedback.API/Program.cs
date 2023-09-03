using Feedback.API.Data;
using Feedback.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Feedbacks", async (AppDbContext context) =>
    await context.Feedbacks.ToListAsync()
);

app.MapGet("/Feedbacks/{id}", async (AppDbContext context, int id) =>
{
    var result = await context.Feedbacks.FindAsync(id);

    if (result ==  null) return Results.NotFound();

   return Results.Ok(result);
});

app.MapPost("/Feedbacks", async (AppDbContext context, FeedbackModel feedback) => 
{
    context.Feedbacks.Add(feedback);
    await context.SaveChangesAsync();

    return Results.Created($"/Feedbacks/{feedback.IdFeedback}", feedback);
});


app.MapPut("/Feedbacks/{id}", async (AppDbContext context, FeedbackModel feedback, int id) =>
{
    var feedbackToUpdate = await context.Feedbacks.FindAsync(id);

    if (feedbackToUpdate == null) return Results.NotFound();

    feedbackToUpdate.NomeCliente = feedback.NomeCliente;
    feedbackToUpdate.EmailCliente = feedback.EmailCliente;
    feedbackToUpdate.DataFeedback = feedback.DataFeedback;
    feedbackToUpdate.Comentario = feedback.Comentario;
    feedbackToUpdate.Avaliacao = feedback.Avaliacao;

    await context.SaveChangesAsync();

    return Results.Ok("Successfully updated");
});

app.MapDelete("/Feedbacks/{id}", async (AppDbContext context, int id) =>
{
    var feedbackToRemove = await context.Feedbacks.FindAsync(id);

    if (feedbackToRemove == null) return Results.NotFound();

    context.Feedbacks.Remove(feedbackToRemove);
    await context.SaveChangesAsync();

    return Results.Ok("Successfully removed");
});

app.Run();
