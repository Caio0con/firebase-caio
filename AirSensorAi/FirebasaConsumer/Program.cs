using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapGet("/teste", () => "ok");
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();
app.Run();

string apiUrl = "https://firebase-caio--caioocon.replit.app/api/DHT";

string firebaseUrl =
    "https://airsensorai-default-rtdb.firebaseio.com/sensores.json";

using var http = new HttpClient();

// GET na API
var apiResponse = await http.GetAsync(apiUrl);
apiResponse.EnsureSuccessStatusCode();

var jsonApi = await apiResponse.Content.ReadAsStringAsync();

// POST no Firebase
var content = new StringContent(
    jsonApi,
    Encoding.UTF8,
    "application/json"
);

await http.PostAsync(firebaseUrl, content);

Console.WriteLine("✅ Dados enviados para o Firebase");
Console.WriteLine(jsonApi);
