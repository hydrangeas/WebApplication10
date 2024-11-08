using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// 環境に応じて設定を切り替える
if (!builder.Environment.IsDevelopment())
{
    // 本番環境ではAzure Key Vaultを使用
    var managedIdentityClientId = Environment.GetEnvironmentVariable("ManagedIdentityClientId");
    var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri")!);
    var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
    {
        ManagedIdentityClientId = managedIdentityClientId
    });

    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, credential);
}

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
