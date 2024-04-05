using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class ProjectProfit
    {
        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonIgnore]
        private List<HouseProfit> _houseList;
        [JsonIgnore]
        public List<HouseProfit> HouseList
        {
            get => _houseList ?? (_houseList = new List<HouseProfit>());
            set => _houseList = value;
        }

        [JsonIgnore]
        public string Subdomain { get; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        internal ProjectProfit() { }

        public ProjectProfit(int id, string subdomain)
        {
            Id = id;
            Subdomain = subdomain;
        }
    }
}
