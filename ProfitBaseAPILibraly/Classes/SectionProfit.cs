using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class SectionProfit
    {
        public string Title { get; set; }

        private List<FloorProfit> _floors;
        public List<FloorProfit> Floors 
        { 
            get => _floors ?? (_floors = new List<FloorProfit>());
            set => _floors = value;
        }

        public HouseProfit House { get; set; }

        public int HouseId { get; set; }

        public int Id { get; }

        public int CountFloor { get; set; }

        private SectionProfit(int id) => Id = id;

        public SectionProfit(int id, int houseId) : this(id) => HouseId = houseId;

        public SectionProfit(int id, HouseProfit house) : this(id, house.Id) => House = house;

        public SectionProfit(int id, HouseProfit house, string title) : this(id, house) => Title = title;
    }
}
