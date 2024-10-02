
using Microsoft.EntityFrameworkCore;
using WalletService.Infra;

namespace WalletService.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<WalletContext>();

                _db.Database.Migrate();

            }
        }
    }
}
