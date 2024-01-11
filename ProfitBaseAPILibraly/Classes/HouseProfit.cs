using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class HouseProfit
    {
        public string Title { get; set; }

        public ProjectProfit Project { get; set; }

        public int ProjectId { get; set; }

        public int MinFloor { get; set; }

        public int MaxFloor { get; set; }

        public int Id { get; set; }

        public List<SectionProfit> Sections { get; set; }

        private HouseProfit(int id) => Id = id;

        public HouseProfit(int id, ProjectProfit project) : this(id) => Project = project;

        public HouseProfit(int id, int projectID) : this(id) => ProjectId = projectID;

        public HouseProfit(int id, int projectID, string title) : this(id, projectID) => Title = title;
    }
}
