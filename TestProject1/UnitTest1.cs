using Moq;
using ProfitBaseAPILibraly.Controllers;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void CreateUrl_ReturnsValidUrl()
        {
            // Arrange
            var mockAuth = new Mock<Auth>();

            var profitBaseAPI = new ProfitBaseAPI(mockAuth.Object);
            var keyValues = new Dictionary<string, string>
            {
                { "houseId", "123" }
            };
            var endPoint = "property";

            // Act
            var url = profitBaseAPI.CreateUrl(keyValues, endPoint);

            // Assert
            Assert.NotNull(url);
            Assert.Equal($"{mockAuth.Object.BaseUrl}{endPoint}?access_token={mockAuth.Object.AccessToken}&houseId=123", url.ToString());
        }
    }
}