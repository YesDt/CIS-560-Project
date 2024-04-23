using Microsoft.EntityFrameworkCore;

using CISProject.EFCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<CISProjectDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("CISProjectDataContext"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<CISProjectDataContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Can't connect to DB");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
