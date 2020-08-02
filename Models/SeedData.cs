using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TechnicalStock.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StockDBContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<StockDBContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.SpareParts.Any())
            {
                context.SpareParts.AddRange(
                    new SparePart
                    {
                        Name = "ECL110 amplifier",
                        Description = "For control loading system",
                        Quantity = 2,
                        Simulator = "A321"
                    },
                    new SparePart
                    {
                        Name = "Motion actuator",
                        Description = "For motion system",
                        Quantity = 1,
                        Simulator = "B737"
                    },
                    new SparePart
                    {
                        Name = "Multipurpose control panel (MCP)",
                        Description = "Autopilot module",
                        Quantity = 1,
                        Simulator = "B737"
                    },
                    new SparePart
                    {
                        Name = "Circuit braker 80A",
                        Description = "For motion PDU of E170",
                        Quantity = 5,
                        Simulator = "E170"
                    },
                    new SparePart
                    {
                        Name = "Power supply module 12V, 3A",
                        Description = "For Reality 7 simulators",
                        Quantity = 4,
                        Simulator = "A322, B737"
                    },
                    new SparePart
                    {
                        Name = "Host server",
                        Description = "Suitable for all L3 sims",
                        Quantity = 1,
                        Simulator = "A321, A322, B737"
                    },
                    new SparePart
                    {
                        Name = "IOS display",
                        Description = "For Reality 7 simulators",
                        Quantity = 2,
                        Simulator = "A322, B737"
                    },
                    new SparePart
                    {
                        Name = "EGPWS module",
                        Description = "Suitable for all L3 sims",
                        Quantity = 1,
                        Simulator = "A321, A322, B737"
                    }
                    );
                context.SaveChanges();
            }

        }
    }
}
