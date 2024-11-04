using Microsoft.EntityFrameworkCore;
using simple.API.Domains;

namespace simple.API;

public class SeedingWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SeedingWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SeedDataAsync();
    }

    private async Task SeedDataAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
        var departments = new List<Department>
        {
            new Department { Name = "Ban giám đốc" },
            new Department { Name = "Nhân sự" },
            new Department { Name = "Kế toán" },
            new Department { Name = "Công nghệ thông tin" }
        };

        var users = new List<User> {
            new User { FirstName = "Duong", LastName = "Hoang", Age = 28 },
            new User { FirstName = "Thao", LastName = "Tran", Age = 26 },
            new User { FirstName = "Trinh", LastName = "Pham", Age = 27 },
            new User { FirstName = "Tung", LastName = "Do", Age = 27 },
            new User { FirstName = "Tuan", LastName = "Le", Age = 26 },
            new User { FirstName = "Mai", LastName = "Hoang", Age = 24 },
        };

        var roles = new List<Role> {
            new Role { Name = "Trưởng phòng" },
            new Role { Name = "Trưởng nhóm" },
            new Role { Name = "Nhân viên" },
        };


        await context.Departments.AddRangeAsync(departments);
        await context.Users.AddRangeAsync(users);
        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
        Console.WriteLine("Seeding complete!");
    }
}
