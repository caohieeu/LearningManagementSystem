using LearningManagementSystem.Authorization;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Mapper;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services;
using LearningManagementSystem.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        });

//Connection DB
builder.Services.AddDbContext<LMSContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DBConnection"], opt =>
    {
        opt.CommandTimeout(600);
    });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LMSContext>().AddDefaultTokenProviders();

//builder.Services.AddControllers()
// .ConfigureApiBehaviorOptions(o =>
// {
//     o.InvalidModelStateResponseFactory = context =>
//     {
//         var problemsDetailsFactory = context.HttpContext.RequestServices
//             .GetRequiredService<ProblemDetailsFactory>();
//         var problemDetails = problemsDetailsFactory.CreateValidationProblemDetails(
//             context.HttpContext,
//             context.ModelState);
//         problemDetails.Detail = "Custom Details";
//         problemDetails.Instance = context.HttpContext.Request.Path;
//         problemDetails.Type = "https://tools.etf............";
//         problemDetails.Status = StatusCodes.Status400BadRequest;
//         problemDetails.Title = "One or more errors on input occured";
//         return IActionResult<ResponseEntity>() {
//             new ResponseEntity()
//             {
//                 code = 400,
//                 message = "Lsdasdasd"
//             };
//         }
//     };
// });

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
{
    opt.TokenLifespan = TimeSpan.FromMinutes(3);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new
    Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecretKey"]))
    };
});

var policies = new Dictionary<string, string>
{
    { "ViewUserPermission", "ViewUser" },
    { "EditUserPermission", "EditUser" },
    { "ViewAuthorizationPermission", "ViewAuthorization" },
    { "AddAuthorizationPermission", "AddAuthorization" },
    { "EditAuthorizationPermission", "EditAuthorization" },
    { "ViewSubjectPermission", "ViewSubject" },
    { "EditSubjectPermission", "EditSubject" },
    { "ViewExaminationPermission", "ViewExamination" },
    { "AddExaminationPermission", "AddExamination" },
    { "EditExaminationPermission", "EditExamination" },
    { "DeleteExaminationPermission", "DeleteExamination" },
    { "DownloadExaminationPermission", "DownloadExamination" },
    { "ApproveExaminationPermission", "ApproveExamination" },
    { "DeleteFilePermission", "DeleteFile" },
    { "DownloadFilePermission", "DownloadFile" },
    { "AddLessionAndResourcePermission", "AddLessionAndResource" },
    { "ViewLessionAndResourcePermission", "ViewLessionAndResource" },
    { "EditLessionAndResourcePermission", "EditLessionAndResource" },
    { "DeleteLessionAndResourcePermission", "DeleteLessionAndResource" },
    { "AddSubjectLessionAndResourcePermission", "AddSubjectLessionAndResource" },
    { "DownloadLessionAndResourcePermission", "DownloadLessionAndResource" },
};

foreach (var policy in policies)
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(policy.Key, policyOptions =>
        {
            policyOptions.Requirements.Add(new PermissionRequirement(policy.Value));
        });
    });
}

//Services
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<ILessionRepository, LessionRepository>();
builder.Services.AddScoped<ITitleRepository, TitleRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IExaminationRepository, ExaminationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<ILessionService, LessionService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IExaminationService, ExaminationService>();
builder.Services.AddScoped<IExcelExportService, ExcelExportService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

//send mail
builder.Services.AddOptions();
var mailSetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSetting);
builder.Services.AddScoped<IEmailSender, SendMailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//middleware
app.UseMiddleware<ResponseMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LMSContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var seedData = new SeedData(userManager, roleManager);
    await seedData.SeedingData(context);
}

app.Run();
