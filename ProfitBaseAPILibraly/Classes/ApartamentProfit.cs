using LivingComplexLib.Models;
using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class ApartamentProfit : Apatrament
    {
        public int Id { get; set; }

        public CastomStatus Status { get; set; }

        public IEnumerable<CastomProperty> CustomProperties { get; set; }
    }
}
