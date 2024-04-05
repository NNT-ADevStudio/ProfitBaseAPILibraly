using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class HouseProfit
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonIgnore]
        public ProjectProfit Project { get; set; }

        [JsonProperty("projectId")]
        public int ProjectId { get; set; }

        [JsonProperty("minFloor")]
        public int MinFloor { get; set; }

        [JsonProperty("maxFloor")]
        public int MaxFloor { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonIgnore]
        private List<SectionProfit> _sections;
        [JsonIgnore]
        public List<SectionProfit> Sections
        {
            get => _sections ?? (_sections = new List<SectionProfit>());
            set => _sections = value;
        }

        internal HouseProfit() { }

        private HouseProfit(int id) => Id = id;

        public HouseProfit(int id, ProjectProfit project) : this(id) => Project = project;

        public HouseProfit(int id, int projectID) : this(id) => ProjectId = projectID;

        public HouseProfit(int id, int projectID, string title) : this(id, projectID) => Title = title;
    }
}
