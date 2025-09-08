using BACKEND.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BACKEND.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // globalni converter za DateTime? formata yyyy-MM-dd
        options.JsonSerializerOptions.Converters.Add(new JsonNullableDateConverter());

    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<EdunovaContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<SlikaService>();




builder.Services.AddCors(o=> 
{
    o.AddPolicy("CorsPolicy", p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(Program));




var app = builder.Build();

Console.WriteLine("? Connection string:");
Console.WriteLine(builder.Configuration.GetConnectionString("EdunovaContext"));


try
{
    using var con = new SqlConnection(builder.Configuration.GetConnectionString("EdunovaContext"));
    con.Open();
    using var cmd = new SqlCommand("SELECT DB_NAME()", con);
    var dbName = (string)cmd.ExecuteScalar();
    Console.WriteLine($"? Connected to DB: {dbName}");
}
catch (Exception ex)
{
    Console.WriteLine($"? Database connection failed: {ex.Message}");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/cec1dc005b96b6a3d3962ba063ded2e5b8f9636b/src/Swashbuckle.AspNetCore.SwaggerUI/SwaggerUIOptionsExtensions.cs#L143
    //options.ConfigObject.TryItOutEnabled = true;
    options.EnableTryItOutByDefault();
});


app.MapControllers();

//app.MapGet("/test-operateri", async (EdunovaContext db) =>
//{
//    try
//    {
//        var count = await db.Operateri.CountAsync();
//        return Results.Ok($"Operateri count: {count}");
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem($"Gre�ka pri dohva�anju tablice: {ex.Message}");
//    }
//});

app.UseDefaultFiles();
app.MapFallbackToFile("index.html");



app.Run();