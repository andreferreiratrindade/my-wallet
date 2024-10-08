
using Microsoft.EntityFrameworkCore;
using ForexService.Infra;

namespace ForexService.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ForexContext>();

                _db.Database.Migrate();
                _db.LoadForexList();
            }
        }
    }
}
