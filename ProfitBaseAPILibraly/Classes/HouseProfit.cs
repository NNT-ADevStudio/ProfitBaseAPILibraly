using LivingComplexLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitBaseAPILibraly.Classes
{
    public class HouseProfit : House
    {
        public int Id { get; }

        public HouseProfit(int id, string title, ProjectProfit project) : base(title, project)
        {
            Id = id;
        }
    }
}
