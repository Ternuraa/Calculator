using Calculator.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); //зарегистрировать контролеры, сервисы (регистрирует что есть обработчики)
//Включиет необходимые сервисы для работы контроллеров

// Add connection to database
builder.Services.AddDbContext<NumbersDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
//настраивает подключение к базе данных SQLite с помощью строки подключения, хранящейся в конфигурации приложения.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //: включает обозреватель API, который представляет собой службу, которая предоставляет метаданные о HTTP-API.
builder.Services.AddSwaggerGen(); // добавляет услуги генерации Swagger к проекту.

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NumbersDbContext>();
    dbContext.Database.EnsureCreated(); // Создает базу данных и таблицы, если их нет
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); 

//app.UseAuthorization();

app.MapControllers(); // настраивает связь между контролерам и тем маршрутом который для него задан
// добавляет маршрутизацию к ранее зарегистрированным контроллерам. Марш связанным с URL
app.Run();
