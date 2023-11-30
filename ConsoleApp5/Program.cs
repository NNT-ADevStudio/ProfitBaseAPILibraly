// See https://aka.ms/new-console-template for more information
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers;

Console.WriteLine("Hello, World!");

Auth auth = new("app-65291fbf36ee5", "pb14686");
await auth.RefreshAccessToken();
ProfitBaseAPI profitBase = new(auth);
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
                Thread.Sleep(1000);
                if (apartament == null) continue;

                foreach (var item in apartament)
                {
                    apartamentProfits.Add(item);
                }

                Console.WriteLine($"{project.Title}" +
                    $" {house.Title}" +
                    $" {section.Title}" +
                    $" {floor.Number}");
            }
        }
    }
}
