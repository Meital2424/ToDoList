
// using Microsoft.EntityFrameworkCore;
// using TodoApi.Data;
// using TodoApi.Models;
// using Microsoft.OpenApi.Models;
// using Microsoft.AspNetCore.Builder;
// using System.Reflection;
// using System.IO;
// using System;
// using TodoApi;
// using Microsoft.AspNetCore.Mvc;

// var builder = WebApplication.CreateBuilder(args);

// // מאזינים מפורשים ל HTTP ו HTTPS
// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenLocalhost(5101); // HTTP
//     options.ListenLocalhost(7097, listenOptions =>
//     {
//         listenOptions.UseHttps();  // HTTPS
//     });
// });

// // CORS - פתיחת הרשאות כללית
// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(policy =>
//     {
//         policy.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//     });
// });

// // הגדרת DbContext עם חיבור ל-MySQL
// // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// var connectionString = builder.Configuration.GetConnectionString("ToDoDB");

// builder.Services.AddDbContext<ToDoDbContext>(options =>
//     options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

// // Swagger - הגדרת SwaggerGen עם מידע מפורט והטמעת קובץ XML לתיעוד
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new OpenApiInfo
//     {
//         Version = "v1",
//         Title = "Todo API",
//         Description = "An ASP.NET Core Web API for managing ToDo items",
//         TermsOfService = new Uri("https://example.com/terms"),
//         Contact = new OpenApiContact
//         {
//             Name = "Example Contact",
//             Url = new Uri("https://example.com/contact")
//         },
//         License = new OpenApiLicense
//         {
//             Name = "Example License",
//             Url = new Uri("https://example.com/license")
//         }
//     });

//     var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
//     if (File.Exists(xmlPath))
//     {
//         options.IncludeXmlComments(xmlPath);
//     }
// });

// var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
//     context.Database.CanConnect();
// }


// app.UseCors();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
//     });
// }

// app.UseHttpsRedirection();

// // Endpoints
// app.MapGet("/", () => " ברוכה הבאה");


// app.MapGet("/tasks", async ([FromServices] ToDoDbContext db) =>
//     await db.Items.ToListAsync());


// app.MapPost("/tasks", async ([FromServices] ToDoDbContext db, [FromBody] Item? item) =>
// {
//     if (item == null || string.IsNullOrWhiteSpace(item.Name))
//         return Results.BadRequest("Task name cannot be empty.");

//     db.Items.Add(item);
//     await db.SaveChangesAsync();
//     return Results.Created($"/tasks/{item.Id}", item);
// });



// app.MapPut("/tasks/{id}", async ([FromServices] ToDoDbContext db, int id, [FromBody] Item? updatedItem) =>
// {
//     if (updatedItem == null || string.IsNullOrWhiteSpace(updatedItem.Name))
//         return Results.BadRequest("Task name cannot be empty.");

//     var item = await db.Items.FindAsync(id);
//     if (item == null) return Results.NotFound();

//     item.Name = updatedItem.Name;
//     item.IsComplete = updatedItem.IsComplete;

//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });


// app.MapDelete("/tasks/{id}", async ([FromServices] ToDoDbContext db, int id) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item == null) return Results.NotFound();

//     db.Items.Remove(item);
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });


// app.Run();
