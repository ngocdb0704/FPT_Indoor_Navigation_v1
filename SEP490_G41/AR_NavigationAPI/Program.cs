using BusinessObject.MappingProfile;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Text.Json.Serialization;




static IEdmModel GetEdmModel()
{
    ODataModelBuilder builder = new ODataConventionModelBuilder();

    builder.EntitySet<Building>("building");
    builder.EntitySet<Facility>("facilities");
    builder.EntitySet<Mappoint>("mappoint");
    builder.EntitySet<Floor>("floor");
    builder.EntitySet<Map>("map");


    return builder.GetEdmModel();
}
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7391);
});
// Add services to the container.

builder.Services.AddControllers().AddOData(option => option.Select()
      .Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<finsContext>();

builder.Services.AddDbContext<finsContext>((serviceProvider, options) =>
{
    var serverVersion = new MySqlServerVersion(new Version(10, 6, 10)); 
    options.UseMySql(builder.Configuration.GetConnectionString("Project"), serverVersion,
        mysqlOptions => mysqlOptions.UseNetTopologySuite()); 

});
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



builder.Services.AddScoped<BuildingDAO>();
builder.Services.AddScoped<FacilityDAO>();
builder.Services.AddScoped<MapDAO>();
builder.Services.AddScoped<MappointDAO>();
builder.Services.AddScoped<FloorDAO>();
builder.Services.AddScoped<MemberDAO>();
builder.Services.AddScoped<MapManageDAO>();
builder.Services.AddScoped<EdgeDAO>();
builder.Services.AddScoped<ProfileDAO>();
builder.Services.AddScoped<PathShortest>();


builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IMapPointRepository, MapPointRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IEdgeRepository, EdgeRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddDistributedMemoryCache();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();


var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();
