using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// ŠÂ‹«‚É‰‚¶‚Äİ’è‚ğØ‚è‘Ö‚¦‚é
if (!builder.Environment.IsDevelopment())
{
    // –{”ÔŠÂ‹«‚Å‚ÍAzure Key Vault‚ğg—p
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
