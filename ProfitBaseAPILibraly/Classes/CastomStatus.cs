using Newtonsoft.Json;

namespace ProfitBaseAPILibraly.Classes
{
    public class CastomStatus
    {
        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("baseStatus")]
        public string BaseStatus { get; set; }

        [JsonProperty("isProtected")]
        public bool IsProtected { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        internal CastomStatus() { }

        public CastomStatus(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public CastomStatus(int id, string name, string baseStatus) : this(id, name) => BaseStatus = baseStatus;

        public CastomStatus(int id, string name, string baseStatus, bool isProtected) : this(id, name, baseStatus) => IsProtected = isProtected;

        public CastomStatus(int id, string name, string baseStatus, bool isProtected, string alias) : this(id, name, baseStatus, isProtected) => Alias = alias;
    }
}
