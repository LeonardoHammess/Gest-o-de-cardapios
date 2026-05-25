using Microsoft.EntityFrameworkCore;
using PROJETO.MEU.Components;
using PROJETO.MEU.Data;
using PROJETO.MEU.Repositories;
using PROJETO.MEU.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=restaurante.db"));

// Add repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRefeicaoRepository, RefeicaoRepository>();
builder.Services.AddScoped<ICardapioRepository, CardapioRepository>();
builder.Services.AddScoped<IEstudanteRepository, EstudanteRepository>();
builder.Services.AddScoped<IRegistroConsumoRepository, RegistroConsumoRepository>();

// Add services
builder.Services.AddScoped<IRefeicaoService, RefeicaoService>();
builder.Services.AddScoped<ICardapioService, CardapioService>();
builder.Services.AddScoped<IEstudanteService, EstudanteService>();
builder.Services.AddScoped<IRegistroConsumoService, RegistroConsumoService>();
builder.Services.AddScoped<IIngredienteService, IngredienteService>();
builder.Services.AddScoped<IRestricaoAlimentarService, RestricaoAlimentarService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
