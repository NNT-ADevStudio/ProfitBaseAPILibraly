using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class HouseProfit
    {
        public string Title { get; set; }

        public ProjectProfit Project { get; set; }

        public int ProjectId { get; set; }

        public List<SectionProfit> Sections { get; set; }

        public int Id { get; set; }

        public HouseProfit(int id, ProjectProfit project)
        {
            Id = id;
            Project = project;
        }

        public HouseProfit(int id, int projectID)
        {
            Id = id;
            ProjectId = projectID;
        }

        public HouseProfit(int id, int projectID, string title) 
        {
            Id = id;
            ProjectId = projectID;
            Title = title;
        }
    }
}
