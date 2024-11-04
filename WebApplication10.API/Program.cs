using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// ���ɉ����Đݒ��؂�ւ���
if (!builder.Environment.IsDevelopment())
{
    // �{�Ԋ��ł�Azure Key Vault���g�p
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
