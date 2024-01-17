using System.Collections.Generic;
using System.Linq;

namespace ProfitBaseAPILibraly.Classes
{
    public class ApartamentProfit
    {
        public int Id { get; set; }

        public CastomStatus Status { get; set; }

        public string ProfitStatus { get; set; }

        private List<CastomProperty> _customProperties;
        public IEnumerable<CastomProperty> CustomProperties
        {
            get => _customProperties ?? (_customProperties = new List<CastomProperty>());
            set => _customProperties = value.ToList();
        }

        public string BookedAt { get; set; }

        public string ResponsibleName { get; set; }

        public int RoomCount { get; set; }

        public string Kod { get; set; }

        public string Number { get; set; }

        public double Price { get; set; }

        private FloorProfit _floor;
        public FloorProfit Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                FloorId = value?.Id ?? 0;
            }
        }

        public int FloorId { get; set; }

        public double TotalArea { get; set; }

        public string SummerRoom { get; set; }

        internal ApartamentProfit() { }

        private ApartamentProfit(int id, string number)
        {
            Id = id;
            Number = number;
        }

        public ApartamentProfit(int id, string number, int floorId) : this(id, number) => FloorId = floorId;

        public ApartamentProfit(int id, string number, FloorProfit floorProfit) : this(id, number) => Floor = floorProfit;
    }
}
