using ProfitBaseAPILibraly.Controllers;

Auth auth = new("app-651fa1092e0e8", "pb14686");
await auth.RefreshAccessToken();
ProfitBaseAPI profitBaseAPI = new(auth);
//await profitBaseAPI.GetApartamentById(10256166);
await profitBaseAPI.GetProject();
