using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class ApartamentProfit
    {
        public int Id { get; set; }

        public CastomStatus Status { get; set; }

        public string ProfitStatus { get; set; }

        public IEnumerable<CastomProperty> CustomProperties { get; set; } = new List<CastomProperty>();

        public string BookedAt { get; set; }

        public string ResponsibleName { get; set; }

        public int RoomCount { get; set; }

        public string Kod { get; set; }

        public int Number { get; set; }

        public double Price { get; set; }

        public FloorProfit Floor { get; set; }

        public int FloorId { get; set; }

        public double TotalArea { get; set; }

        public string SummerRoom { get; set; }

        internal ApartamentProfit() { }

        private ApartamentProfit(int id, int number)
        {
            Id = id;
            Number = number;
        }

        public ApartamentProfit(int id, int number, int floorId) : this(id, number) => FloorId = floorId;

        public ApartamentProfit(int id, int number, FloorProfit floorProfit) : this(id, number) => Floor = floorProfit;
    }
}
