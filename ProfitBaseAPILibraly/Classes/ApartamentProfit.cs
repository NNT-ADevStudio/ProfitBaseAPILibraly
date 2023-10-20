using LivingComplexLib.Models;

namespace ProfitBaseAPILibraly.Classes
{
    public class ApartamentProfit : Apatrament
    {
        public int Id { get; set; }

        public CastomStatus Status { get; set; }

        public int CountRoom { get; set; }

        public double RenovationPrice { get; set; }
    }
}
