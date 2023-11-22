using LivingComplexLib.Models;

namespace ProfitBaseAPILibraly.Classes
{
    public class HouseProfit : House
    {
        public int Id { get; }

        public HouseProfit(int id, string title, ProjectProfit project) : base(title, project)
            => Id = id;

        public HouseProfit() => Project = new Project();
    }
}
