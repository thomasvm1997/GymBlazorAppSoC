using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pri.EindOpdracht.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Data.Seeding
{
    public class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder) 
        
      {
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        // Seed Roles
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);

        // Seed Users
        var users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = "1",
                UserName = "admin",
                Email = "admin@example.com",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "Admin123!"),
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                NormalizedUserName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "User"
            },
            new ApplicationUser
            {
                Id = "2",
                UserName = "customer1",
                Email = "customer1@example.com",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "Customer123!"),
                NormalizedEmail = "CUSTOMER1@EXAMPLE.COM",
                NormalizedUserName = "CUSTOMER1",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = "Customer",
                LastName = "One"
            },
            new ApplicationUser
            {
                Id = "3",
                UserName = "customer2",
                Email = "customer2@example.com",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "Customer123!"),
                NormalizedEmail = "CUSTOMER2@EXAMPLE.COM",
                NormalizedUserName = "CUSTOMER2",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = "Customer",
                LastName = "Two"
            }
        };

        modelBuilder.Entity<ApplicationUser>().HasData(users);

        // Seed User Roles
        var userRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Admin
            new IdentityUserRole<string> { UserId = "2", RoleId = "2" }, // Customer
            new IdentityUserRole<string> { UserId = "3", RoleId = "2" }  // Customer
        };

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        // Seed WorkoutTypes
        var workoutTypes = new List<WorkoutType>
        {
            new WorkoutType { Id = 1, Name = "Cardio", Description = "Boost your heart rate and endurance", Difficulty = "Intermediate" },
            new WorkoutType { Id = 2, Name = "Strength", Description = "Build muscle and strength", Difficulty = "Advanced" },
            new WorkoutType { Id = 3, Name = "Flexibility", Description = "Improve range of motion and posture", Difficulty = "Beginner" }
        };

        modelBuilder.Entity<WorkoutType>().HasData(workoutTypes);

        // Seed Workouts
        var workouts = new List<Workout>
        {
            // Workouts for Customer 1
            new Workout { Id = 1, Date = DateTime.Now.AddDays(-1), WorkoutTypeId = 1, Duration = 30, CaloriesBurned = 250, UserId = "2" },
            new Workout { Id = 2, Date = DateTime.Now.AddDays(-2), WorkoutTypeId = 2, Duration = 45, CaloriesBurned = 350, UserId = "2" },
            new Workout { Id = 3, Date = DateTime.Now.AddDays(-3), WorkoutTypeId = 3, Duration = 20, CaloriesBurned = 100, UserId = "2" },

            // Workouts for Customer 2
            new Workout { Id = 4, Date = DateTime.Now.AddDays(-1), WorkoutTypeId = 1, Duration = 25, CaloriesBurned = 200, UserId = "3" },
            new Workout { Id = 5, Date = DateTime.Now.AddDays(-2), WorkoutTypeId = 2, Duration = 40, CaloriesBurned = 300, UserId = "3" },
            new Workout { Id = 6, Date = DateTime.Now.AddDays(-3), WorkoutTypeId = 3, Duration = 15, CaloriesBurned = 80, UserId = "3" },

            // General workouts
            new Workout { Id = 7, Date = DateTime.Now, WorkoutTypeId = 1, Duration = 35, CaloriesBurned = 270 },
            new Workout { Id = 8, Date = DateTime.Now, WorkoutTypeId = 2, Duration = 50, CaloriesBurned = 400 },
            new Workout { Id = 9, Date = DateTime.Now, WorkoutTypeId = 3, Duration = 25, CaloriesBurned = 120 },
            new Workout { Id = 10, Date = DateTime.Now, WorkoutTypeId = 1, Duration = 60, CaloriesBurned = 500 }
        };

        modelBuilder.Entity<Workout>().HasData(workouts);

        // Seed Goals
        var goals = new List<Goal>
        {
            // Goals for Customer 1
            new Goal { Id = 1, UserId = "2", Description = "Run 5 km in 30 minutes", TargetDate = DateTime.Now.AddMonths(1), Achieved = false },
            new Goal { Id = 2, UserId = "2", Description = "Lose 5 kg in 2 months", TargetDate = DateTime.Now.AddMonths(2), Achieved = false },
            new Goal { Id = 3, UserId = "2", Description = "Do 50 push-ups in one go", TargetDate = DateTime.Now.AddMonths(3), Achieved = false },

            // Goals for Customer 2
            new Goal { Id = 4, UserId = "3", Description = "Run 10 km in 60 minutes", TargetDate = DateTime.Now.AddMonths(1), Achieved = false },
            new Goal { Id = 5, UserId = "3", Description = "Gain 3 kg of muscle in 3 months", TargetDate = DateTime.Now.AddMonths(3), Achieved = false },
            new Goal { Id = 6, UserId = "3", Description = "Hold a plank for 5 minutes", TargetDate = DateTime.Now.AddMonths(2), Achieved = false }
        };

        modelBuilder.Entity<Goal>().HasData(goals);
    }

    }
}
