using Newtonsoft.Json;
using ProfitBaseAPILibraly.Classes.SubClasses;
using System.Collections.Generic;
using System.Linq;

namespace ProfitBaseAPILibraly.Classes
{
    public class ApartamentProfit
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("propertyType")]
        public string PropertyType { get; set; }

        [JsonProperty("customStatusId")]
        public int CustomStatusId { get; set; }

        [JsonIgnore]
        public CastomStatus Status { get; set; }

        [JsonProperty("status")]
        public string ProfitStatus { get; set; }

        [JsonIgnore]
        private List<CastomProperty> _customProperties;
        [JsonProperty("custom_fields")]
        public List<CastomProperty> CustomProperties
        {
            get => _customProperties ?? (_customProperties = new List<CastomProperty>());
            set => _customProperties = value.ToList();
        }

        [JsonProperty("positionOnFloor")]
        public int? PositionOnFloor { get; set; }
        
        [JsonProperty("bookedAt")]
        public string BookedAt { get; set; } = null;

        [JsonProperty("responsibleName")]
        public string ResponsibleName { get; set; } = null;

        [JsonProperty("rooms_amount")]
        public int RoomCount { get; set; }

        [JsonProperty("layoutCode")]
        public string Kod { get; set; } = null;

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonIgnore]
        private Price _price;
        [JsonProperty("price")]
        public Price Price 
        {
            get => _price;
            set => _price = value;
        }

        [JsonIgnore]
        private FloorProfit _floor;
        [JsonIgnore]
        public FloorProfit Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                FloorId = value?.Id ?? 0;
            }
        }

        [JsonProperty("area")]
        public Area Area { get; set; }

        [JsonIgnore]
        public int FloorId { get; set; }

        [JsonIgnore]
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
