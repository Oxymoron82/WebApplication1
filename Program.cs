using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервиса In-Memory базы данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));

// Добавляем контроллеры
builder.Services.AddControllers();

// Чтение конфигурации из appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Конфигурация аутентификации через Google OAuth (JWT Bearer)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Конфигурация параметров токенов Google OAuth
        options.Authority = "https://accounts.google.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://accounts.google.com", // Проверка издателя
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Google:ClientId"], // Используем ClientId из конфигурации
            ValidateLifetime = true
        };

        // Точное соответствие алгоритму подписи (если нужно)
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("your-secret-key") // замените на свой секретный ключ
        );
    });

// Добавляем авторизацию
builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseAuthentication(); // Включаем аутентификацию
app.UseAuthorization(); // Включаем авторизацию

// Подключение контроллеров
app.MapControllers();

app.Run();
