using LivingComplexLib.Models;
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Class1
    {
        public async Task<List<ApartamentProfit>> GetApartamentForProfit(ProfitBaseAPI profitBase)
        {
            await profitBase.GetCastomStatus();
            List<ApartamentProfit> apartamentProfits = new();

            var projects = await profitBase.GetProject();
            foreach (var project in projects)
            {
                var houses = await profitBase.GetHouses((ProjectProfit)project);

                foreach (var house in houses)
                {
                    var sections = await profitBase.GetSection((HouseProfit)house);

                    foreach (SectionProfit section in sections.Cast<SectionProfit>())
                    {
                        for (int i = 1; i <= section.CountFloor + 1; i++)
                        {
                            Floor floor = new(i, section);
                            var apartament = await profitBase.GetApartaments(floor);

                            if (apartament == null) continue;

                            apartamentProfits.AddRange(apartament.Cast<ApartamentProfit>());
                            Console.WriteLine($"{project.Title}" +
                                $" {house.Title} " +
                                $" {section.Title} " +
                                $"{floor.Number}");
                        }

                    }
                }
            }

            return apartamentProfits;
        }

        public async Task Update() 
        {
            List<ApartamentProfit> apartamentProfits = new List<ApartamentProfit>();

            Auth auth = new("app-65291fbf36ee5", "pb14686");
            await auth.RefreshAccessToken();
            apartamentProfits.AddRange(await GetApartamentForProfit(new ProfitBaseAPI(auth)));

            auth = new("app-6528fef718c38", "pb16992");
            await auth.RefreshAccessToken();
            apartamentProfits.AddRange(await GetApartamentForProfit(new ProfitBaseAPI(auth)));

            auth = new("app-65292008b6f15", "pb4651");
            await auth.RefreshAccessToken();
            apartamentProfits.AddRange(await GetApartamentForProfit(new ProfitBaseAPI(auth)));
        }

    }
}
