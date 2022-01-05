using ScanningApp.Core.Entity;
using System;

namespace ScanningApp.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void InitData(ScanningAppContext ctx)
        {
            ctx.Database.EnsureDeleted(); //Crucial that it is only in development
            ctx.Database.EnsureCreated();

            //Mock data for testing
            var concert = ctx.Concerts.Add(new Concert()
            {
                title = "Concert1",               
                start_date = new DateTime(2022, 05, 09, 09, 15, 00)
            }).Entity;
            ctx.SaveChanges();

            var scan = ctx.Scans.Add(new Scan()
            {
                SecurityCode = "1234",
                ConcertId = concert.id,
                UserId = 111

            }).Entity;
            ctx.SaveChanges();
        }
    }
}
