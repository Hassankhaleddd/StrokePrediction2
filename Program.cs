using Microsoft.AspNetCore.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//to setup database
var connectionstring = builder.Configuration.GetConnectionString("StrokePrediction");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionstring));

//prepare identity
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

//for validation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//for identity
app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.UseAuthorization();

app.MapControllers();

app.Run();
