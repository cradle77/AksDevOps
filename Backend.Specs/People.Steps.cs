using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;
using Xunit;

namespace Backend.Specs
{
    [Binding]
    public class PeopleEndpointSteps
    {
        private HttpClient _client;
        private string _baseUrl;
        private HttpResponseMessage _response;
        private Person _person;

        public PeopleEndpointSteps()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("testconfig.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _client = new HttpClient();
            _baseUrl = config.GetValue<string>("baseUrl");
        }

        [Given(@"that the API is running")]
        public void GivenThatTheAPIIsRunning()
        {
            // do nothing
        }
        
        [Given(@"there is a person called ""(.*)"" in the database")]
        public void GivenThereIsAPersonCalledInTheDatabase(string name)
        {
            _person = new Person()
            {
                Name = name,
                Email = name,
            };

            var content = new StringContent(JsonConvert.SerializeObject(_person));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = _client.PostAsync(_baseUrl, content).Result;

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync().Result;

            _person = JsonConvert.DeserializeObject<Person>(result);
        }
        
        [When(@"I execute a People POST request with ""(.*)"" and ""(.*)""")]
        public void WhenIExecuteAPeoplePOSTRequestWithAnd(string name, string email)
        {
            _person = new Person()
            {
                Name = name,
                Email = email,
            };

            var content = new StringContent(JsonConvert.SerializeObject(_person));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _response = _client.PostAsync(_baseUrl, content).Result;
        }
        
        [When(@"I execute a People GET request")]
        public void WhenIExecuteAPeopleGETRequest()
        {
            _response = _client.GetAsync(_baseUrl).Result;
        }
        
        [When(@"I execute a People DELETE request on its id")]
        public void WhenIExecuteAPeopleDELETERequestOnItsId()
        {
            var url = $"{_baseUrl}/{_person.Id}";
            _response = _client.DeleteAsync(url).Result;
        }
        
        [Then(@"I get a (.*) status code")]
        public void ThenIGetAStatusCode(int statusCode)
        {
            var expected = (HttpStatusCode)statusCode;

            Assert.Equal(expected, _response.StatusCode);
        }

        [Then(@"a person called ""(.*)"" exists on the list")]
        public void ThenAPersonCalledExistsOnTheList(string name)
        {
            var response = _client.GetAsync(_baseUrl).Result;

            var body = response.Content.ReadAsStringAsync().Result;

            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(body);

            var exists = people.Any(x => x.Name == name);

            Assert.True(exists);
        }

        [Then(@"there are at least (.*) people")]
        public void ThenThereAreAtLeastPeople(int peopleCount)
        {
            var body = _response.Content.ReadAsStringAsync().Result;

            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(body);

            var actual = people.Count();

            Console.WriteLine($"{actual} people returned");

            Assert.True(peopleCount <= actual);
        }

        [Then(@"a person called ""(.*)"" does not exist on the list")]
        public void ThenAPersonCalledDoesNotExistOnTheList(string name)
        {
            var response = _client.GetAsync(_baseUrl).Result;

            var body = response.Content.ReadAsStringAsync().Result;

            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(body);

            var exists = people.Any(x => x.Name == name);

            Assert.False(exists);
        }

    }
}
