using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class FloorProfit
    {
        [JsonProperty("floor_id")]
        public int Id { get; set; }

        [JsonProperty("floor")]
        public string Number { get; set; }

        [JsonProperty("count")]
        public int CountApartament { get; set; }

        [JsonProperty("section_id")]
        public int SectionId { get; set; }

        [JsonIgnore]
        public SectionProfit Section { get; set; }

        [JsonIgnore]
        private List<ApartamentProfit> _apartaments;
        [JsonIgnore]
        public List<ApartamentProfit> Apartaments
        {
            get => _apartaments ?? (_apartaments = new List<ApartamentProfit>());
            set => _apartaments = value;
        }

        internal FloorProfit() { }

        public FloorProfit(string number, int sectionId)
        {
            Number = number;
            SectionId = sectionId;
        }

        public FloorProfit(string number, SectionProfit section) : this(number, section.Id) => Section = section;
    }
}
