using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class FloorProfit
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int CountApartament { get; set; }

        public SectionProfit Section { get; set; }

        public List<ApartamentProfit> Apartaments { get; set; }

        public int SectionId { get; set; }

        public FloorProfit(int number, SectionProfit section)
        {
            Number = number;
            Section = section;
        }

        public FloorProfit(int number, int sectionId)
        {
            Number = number;
            SectionId = sectionId;
        }
    }
}
