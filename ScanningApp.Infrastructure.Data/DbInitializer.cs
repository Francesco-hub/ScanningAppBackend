using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanningApp.Infrastructure.Data
{
   public class DbInitializer
    {
        public static void InitData(ScanningAppContext ctx)
        {
            ctx.Database.EnsureDeleted(); //Crucial that it is only in development
            ctx.Database.EnsureCreated();
            var con1 = ctx.Concerts.Add(new Concert()
            {
                title = "Concert1",
                date = System.DateTime.Now.ToString(),
                start_date = System.DateTime.Now

            }
                ).Entity;
            ctx.SaveChanges();
            var scn1 = ctx.Scans.Add(new Scan()
            {
                SecurityCode = "1234",
                ConcertId = con1.id

            }).Entity;
            ctx.SaveChanges();
        }
    }
}
