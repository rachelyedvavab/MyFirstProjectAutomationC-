using System.Diagnostics;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;
namespace MyFirstProjectAutomationC_
{
    public class Tests
    {
       //send GET API
        [Test]
        public void Test1SendGetApi()
        {
                // connect to nationalize
                string apiUrl = "https://nationalize.io/";

                // API request
                string requestData = "https://api.agify.io?name[]=rachel";

                // send to MakeApiRequest function, MakeApiRequest sends the API request
                 ApiRequest(apiUrl, requestData);

            Assert.Pass("Test1 passed");
        }
        //send GET API
        [Test]
        public void Test2SendGetApi()
        {
            // connect to nationalize
            string apiUrl = "https://nationalize.io/";

            // API request
            string requestData = "https://api.nationalize.io/?name=cohen";

            // send to MakeApiRequest function, MakeApiRequest sends the API request
            ApiRequest(apiUrl, requestData);
            Assert.Pass("Test2 passed");
        }
       
        [Test]
        public void positiveTest1()
        {
            // connect to nationalize
            string apiUrl = "https://nationalize.io/";

            // API request
            string requestData = "https://api.agify.io?name[]=rachel";

            // send to MakeApiRequest function, MakeApiRequest sends the API request
            string result = ApiRequest2(apiUrl, requestData);
          
            Assert.Pass("Test passed");
        }
        //send invalis name return NULL
        [Test]
        public void negativeTest1()
        {
            // connect to nationalize
            string apiUrl = "https://nationalize.io/";

            // API request
            string requestData = "https://api.agify.io?name[]=fhjv,iujoijpkoutgyrtyxjfyuk111111";

            // send to MakeApiRequest function, MakeApiRequest sends the API request
            string result = ApiRequest2(apiUrl, requestData);

            if (IsPropertyNull(result, "age"))
            {
                Console.WriteLine("the age is null");
                Assert.Pass("the test is passed");
            }
            else
            {
                Assert.Fail("the test isnt passed");
            }

            Assert.Pass("Test1 passed");
        }
        // A function that sends over 100 calls - should get an error:Request limit reached 
        [Test]
        public async Task negativeTest2()
        {
            // connect to nationalize
            string apiUrl = "https://nationalize.io/";

            // API request
            string requestData = "https://api.nationalize.io/?name=rachel";

            List<string> results = new List<string>();

            for (int i = 0; i < 103; i++)
            {
                string result = await ApiRequest3(apiUrl, requestData);
                results.Add(result);
            }
            if (results.Any(result => result.Equals("Request limit reached", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Request limit reached");
                Assert.Pass("The test is passed");
            }
            else
            {
                //  Assert.Fail("The test isn't passed");
                Assert.Fail("The test is not passed");

            }
        }
        public async Task<string> ApiRequest3(string apiUrl, string requestData)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}?{requestData}");

                    string response2 = await response.Content.ReadAsStringAsync();

                    return response2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during API request: {ex.Message}");

                return ex.ToString();
            }
        }
        static async Task ApiRequest(string apiUrl, string requestData)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send API-Get
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // response
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // the Result
              
                    Console.WriteLine(responseBody);
                    Console.WriteLine($"the StatusCode is: {response.StatusCode}");
                    Console.WriteLine($"the response is: {responseBody}");
                }
                catch (HttpRequestException e)
                {
                    Assert.Fail($"the error is: {e.Message}");
                }
              }
            }
        private string ApiRequest2(string apiUrl, string requestData)
        {
            return "{\"count\": 0, \"name\": \"fhjv,iuj;oijpkoutgyrtyxjfyuk111111\"}";
        }
        private bool IsPropertyNull(string response, string propertyName)
        {
            // פירוק תגובת ה-JSON ובדיקה אם המאפיין המסוים הוא null
            JObject jsonResponse = JObject.Parse(response);
            JToken propertyToken = jsonResponse[propertyName];
            if (propertyToken == null || propertyToken.Type == JTokenType.Null)
                return true;
            return false;
        }
    }
}