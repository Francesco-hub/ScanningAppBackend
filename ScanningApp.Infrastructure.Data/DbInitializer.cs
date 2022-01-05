using ScanningApp.Core.Entity;
using System;

namespace ScanningApp.Infrastructure.Data
{
    public class DbInitializer
    {

        public static void InitMockData(ScanningAppContext ctx)
        {
            ctx.Database.EnsureDeleted();  //Crucial that it is only in development
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

        public static void InitializeUsers(ScanningAppContext ctx)
        {
            var user1_pia = ctx.Users.Add(new User()
            {
                Code = 1111,
                FirstName = "Pia",
                LastName = "Jørs"
            }).Entity;

            var user2_gabriella = ctx.Users.Add(new User()
            {
                Code = 2222,
                FirstName = "Gabriella",
                LastName = "Bergman"
            }).Entity;

            ctx.SaveChanges();
        }
    }
}
