using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class FloorProfit
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int CountApartament { get; set; }

        public SectionProfit Section { get; set; }

        private List<ApartamentProfit> _apartaments;
        public List<ApartamentProfit> Apartaments
        {
            get
            {
                return _apartaments ?? (_apartaments = new List<ApartamentProfit>());
            }
            set
            {
                _apartaments = value;
            }
        }

        public int SectionId { get; set; }

        public FloorProfit(int number, int sectionId)
        {
            Number = number;
            SectionId = sectionId;
        }

        public FloorProfit(int number, SectionProfit section) : this(number, section.Id) => Section = section;
    }
}
