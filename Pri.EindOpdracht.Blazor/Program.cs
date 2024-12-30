using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Pri.EindOpdracht.ApiConsumer.Goals;
using Pri.EindOpdracht.ApiConsumer.StateProvider;
using Pri.EindOpdracht.ApiConsumer.Users;
using Pri.EindOpdracht.ApiConsumer.Workouts;
using Pri.EindOpdracht.ApiConsumer.WorkoutTypes;
using Pri.EindOpdracht.Blazor.Data;
using Pri.EindOpdracht.Blazor.StateProvider;

namespace Pri.EindOpdracht.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient("WorkoutApiClient", client =>
            {
                
            });
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<IGoalApiService, GoalApiService>();
            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddScoped<IWorkoutApiService, WorkoutApiService>();
            builder.Services.AddScoped<IWorkoutTypeApiService, WorkoutTypeApiService>();
            builder.Services.AddScoped<IJwtAuthenticationStateProvider, JwtAuthenticationStateProvider>();



            var app = builder.Build();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}