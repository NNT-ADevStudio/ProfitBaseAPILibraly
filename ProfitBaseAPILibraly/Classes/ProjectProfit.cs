using LivingComplexLib.Models;

namespace ProfitBaseAPILibraly.Classes
{
    public class ProjectProfit : Project
    {
        public int Id { get; }

        public string Subdomain { get; }

        public ProjectProfit(int id, string title, string subdomain) : base(title)
        {
            Id = id;
            Subdomain = subdomain;
        }
    }
}
