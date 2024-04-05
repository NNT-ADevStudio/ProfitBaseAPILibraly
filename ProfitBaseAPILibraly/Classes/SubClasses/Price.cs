using Newtonsoft.Json;

namespace ProfitBaseAPILibraly.Classes.SubClasses
{
    public class Price
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("priceReserved")]
        public decimal? PriceReserved { get; set; }

        [JsonProperty("pricePerMeter")]
        public decimal PricePerMeter { get; set; }

        internal Price() { }

        public Price(decimal value, decimal pricePerMeter) => (Value, PricePerMeter) = (value, pricePerMeter);

    }
}
