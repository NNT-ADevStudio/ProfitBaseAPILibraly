using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class FloorController : MainController
    {
        public FloorController(Auth auth) : base(auth)
        {
        }

        public async Task<List<FloorProfit>> GetFloors(HouseProfit house)
        {
            return null;
        }
    }
}
