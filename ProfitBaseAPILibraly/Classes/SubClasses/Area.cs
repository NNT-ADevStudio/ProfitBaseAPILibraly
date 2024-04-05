using Newtonsoft.Json;

namespace ProfitBaseAPILibraly.Classes.SubClasses
{
    public class Area
    {
        [JsonProperty("area_total")]
        public double? AreaTotal { get; set; }

        [JsonProperty("area_estimated")]
        public double? AreaEstimated { get; set; }

        [JsonProperty("area_living")]
        public double? AreaLiving { get; set; }

        [JsonProperty("area_kitchen")]
        public double? AreaKitchen { get; set; }

        [JsonProperty("area_balcony")]
        public double? AreaBalcony { get; set; }

        [JsonProperty("area_without_balcony")]
        public double? AreaWithoutBalcony { get; set; }

        internal Area() { }
    }
}
