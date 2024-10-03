
using Microsoft.EntityFrameworkCore;
using StockService.Infra;

namespace StockService.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<StockContext>();

                _db.Database.Migrate();
                _db.LoadStockList();
            }
        }
    }
}
