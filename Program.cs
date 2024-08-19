using ChrisHaniHospital.Areas.Identity.Data;
using ChrisHaniHospital.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ChrisHaniContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder
    .Services.AddDefaultIdentity<ChrisHaniUser>(options =>
        options.SignIn.RequireConfirmedAccount = true
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ChrisHaniContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

//creating Default Roles
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ChrisHaniContext>();

    // Use dbContext.Database to access the DatabaseFacade
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        // If there are any pending migrations, then migrate
        dbContext.Database.Migrate();
    }
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ChrisHaniContext>();
        context.Database.Migrate(); // This line applies pending migrations or creates the database if it doesn't exist
    }
    catch (Exception ex)
    {
        // Log errors or handle them as needed
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new string[]
    {
        "Administrator",
        "Pharmacist",
        "Nurse",
        "Surgeon",
        "Anaesthesiologist",
        "Patient"
    };

    foreach (var role in roles)
    {
        // Use async/await instead of .Result or .Wait()
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ChrisHaniUser>>();

    // Helper function to create a user and assign a role
    async Task CreateUser(
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string password,
        string role
    )
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ChrisHaniUser
            {
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                EmailConfirmed = true
            };

            var createUserResult = await userManager.CreateAsync(user, password);
            if (createUserResult.Succeeded)
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, role);
                if (!addToRoleResult.Succeeded)
                {
                    // Handle failure to add user to role
                    Console.WriteLine(
                        $"Failed to add user {email} to role {role}: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}"
                    );
                }
            }
            else
            {
                // Handle failure to create user
                Console.WriteLine(
                    $"Failed to create user {email}: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}"
                );
            }
        }
    }

    // Create users
    await CreateUser(
        "Nurse@gmail.com",
        "Okuhle",
        "Ntshongwa",
        "081 123 4123",
        "Nurse@123",
        "Nurse"
    );
    await CreateUser(
        "Administrator@gmail.com",
        "Admin",
        "Admin",
        "0855 354 1874",
        "passworD@123",
        "Administrator"
    );
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
