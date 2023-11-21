using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers;

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
                var houses = await profitBase.GetHouses(project);

                foreach (var house in houses)
                {
                    var sections = await profitBase.GetSection(house);

                    foreach (SectionProfit section in sections.Cast<SectionProfit>())
                    {
                        for (int i = 1; i <= section.CountFloor + 1; i++)
                        {
                            FloorProfit floor = new(i, section);
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

        public async Task GetApartamentById() 
        {
            Auth auth = new("app-6528fef718c38", "pb16992");
            await auth.RefreshAccessToken();
            ProfitBaseAPI profitBaseAPI = new(auth);
            await profitBaseAPI.GetApartamentById(11973607);

        }

        public async Task Update() 
        {
            List<ApartamentProfit> apartamentProfits = new();

            //Auth auth = new("app-65291fbf36ee5", "pb14686");
            //await auth.RefreshAccessToken();
            //apartamentProfits.AddRange(await GetApartamentForProfit(new ProfitBaseAPI(auth)));

            Auth auth = new("app-6528fef718c38", "pb16992");
            await auth.RefreshAccessToken();
            apartamentProfits.AddRange(await GetApartamentForProfit(new ProfitBaseAPI(auth)));

            //auth = new("app-65292008b6f15", "pb4651");
            //await auth.RefreshAccessToken();
            //apartamentProfits.AddRange(await GetApartamentForProfit(new ProfitBaseAPI(auth)));
        }

    }
}
