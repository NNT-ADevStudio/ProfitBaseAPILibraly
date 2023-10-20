using LivingComplexLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitBaseAPILibraly.Classes
{
    public class SectionProfit : Section
    {
        public int CountFloor { get; set; }

        public int Id { get; }

        public SectionProfit(int id, string title, int count, HouseProfit house) : base(title, house)
        {
            Id = id;
            CountFloor = count;
        }
    }
}
