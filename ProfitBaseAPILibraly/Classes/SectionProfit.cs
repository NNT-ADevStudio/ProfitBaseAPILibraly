using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class SectionProfit
    {
        public string Title { get; set; }

        public List<FloorProfit> Floors { get; set; }

        public HouseProfit House { get; set; }

        public int HouseId { get; set; }

        public int Id { get; }

        private SectionProfit(int id) => Id = id;

        public SectionProfit(int id, HouseProfit house) : this(id)
        {
            House = house;
        }

        public SectionProfit(int id, int houseId) : this(id)
        {
            HouseId = houseId;
        }

        public SectionProfit(int id, HouseProfit house, string title) : this(id, house)
        {
            Title = title;
        }
    }
}
