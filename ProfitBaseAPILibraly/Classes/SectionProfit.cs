using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class SectionProfit
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonIgnore]
        private List<FloorProfit> _floors;
        [JsonIgnore]
        public List<FloorProfit> Floors
        {
            get => _floors ?? (_floors = new List<FloorProfit>());
            set => _floors = value;
        }

        [JsonIgnore]
        public HouseProfit House { get; set; }

        [JsonIgnore]
        public int HouseId { get; set; }

        [JsonProperty("section_id")]
        public int Id { get; }

        [JsonProperty("count")]
        public int CountFloor { get; set; }

        [JsonProperty("above-ground")]
        public int AboveGround {get;set;}
        
        [JsonProperty("underground")]
        public int Underground { get; set; }

        private SectionProfit(int id) => Id = id;

        public SectionProfit(int id, int houseId) : this(id) => HouseId = houseId;

        public SectionProfit(int id, HouseProfit house) : this(id, house.Id) => House = house;

        public SectionProfit(int id, HouseProfit house, string title) : this(id, house) => Title = title;
    }
}
