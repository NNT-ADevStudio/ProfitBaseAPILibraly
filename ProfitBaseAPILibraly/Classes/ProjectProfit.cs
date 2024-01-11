using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class ProjectProfit
    {
        public int Id { get; }

        public string Title { get; set; }

        private List<HouseProfit> _houseList;
        public List<HouseProfit> HouseList
        {
            get => _houseList ?? (_houseList = new List<HouseProfit>());
            set => _houseList = value;
        }

        public string Subdomain { get; }

        public string Locality { get; set; }

        public ProjectProfit(int id, string subdomain)
        {
            Id = id;
            Subdomain = subdomain;
        }
    }
}
