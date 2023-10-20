using ProfitBaseAPILibraly.Controllers;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            Auth auth = new Auth("app-65291fbf36ee5", "pb14686");
            await auth.RefreshAccessToken();

            ProfitBaseAPI profitBaseAPI = new ProfitBaseAPI(auth);

            profitBaseAPI.GetProject();
        }
    }
}