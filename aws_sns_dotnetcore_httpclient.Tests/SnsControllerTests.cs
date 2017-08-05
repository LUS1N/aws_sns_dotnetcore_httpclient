using System.IO;
using System.Threading.Tasks;
using aws_sns_dotnetcore_httpclient.Models;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using Newtonsoft.Json;



namespace aws_sns_dotnetcore_httpclient.Tests
{
    public class SnsControllerTests
    {
        [Fact]
        public async Task TestConfirmation()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/SubscriptionConfirmation.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            // return the length of the body received by the GET request of the 
            // confirmationUrl
            // http://example.com
            Assert.True(int.Parse(response.Body) > 0);
            Assert.Equal(response.StatusCode, 200);
        }


        [Fact]
        public async Task TestNotification()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/Notification.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            // extract the person from the response body
            var person = JsonConvert.DeserializeObject<Person>(response.Body);
            Assert.Equal(response.StatusCode, 200);
            Assert.Equal(person.Name, "John");
            Assert.Equal(person.Surname, "Doe");
        }
    }
}
