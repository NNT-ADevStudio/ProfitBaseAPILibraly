using LivingComplexLib.Models;
using System.Collections.Generic;

namespace ProfitBaseAPILibraly.Classes
{
    public class ApartamentProfit : Apatrament
    {
        public int Id { get; set; }

        public CastomStatus Status { get; set; }

        public string ProfitStatus { get; set; }

        public IEnumerable<CastomProperty> CustomProperties { get; set; }

        public string BookedAt { get; set; }
    }
}
