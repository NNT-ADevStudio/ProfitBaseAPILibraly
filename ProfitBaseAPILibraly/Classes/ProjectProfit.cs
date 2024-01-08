using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class ProjectProfit
    {
        public int Id { get; }

        public string Title { get; set; }

        public List<HouseProfit> HouseList { get; set; }

        public string Subdomain { get; }

        public string Locality { get; set; }

        public ProjectProfit(int id, string subdomain)
        {
            Id = id;
            Subdomain = subdomain;
        }
    }
}
