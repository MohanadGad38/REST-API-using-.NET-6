using BuberBreakFast.Services.Breakfast;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    //builder.Services.AddSingleton<IBreakfastServices,breakfastServices>();
    builder.Services.AddScoped<IBreakfastServices,breakfastServices>();
    //builder.Services.AddTransient<IBreakfastServices,breakfastServices>();
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();
{   
        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
}


