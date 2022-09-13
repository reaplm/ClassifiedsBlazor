using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ClassifiedsBlazor.Entities;
using ClassifiedsBlazor.Services.Impl;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTestProject.Service
{
    public class AdvertServiceTest
    {
        private List<Advert> Adverts;
        public AdvertServiceTest()
        {
            Initialize();
        }
        [Fact]
        public void TestFindAll()
        {

            //Mocking HttpClient
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                true,
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(GetAdverts()))
                }).Verifiable();

            var _httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com")
            };
            var mockLogger = new Mock<ILogger<AdvertService>>();

            var service = new AdvertService(_httpClient, mockLogger.Object); 
            var task = service.FindAll();
            var adverts = task.Result as IEnumerable<Advert>;
         
            Assert.True(task.IsCompleted);
            Assert.NotNull(task.Result);
            Assert.Equal(7, adverts.Count());
        }
        [Fact]
        public void TestFindById()
        {

            //Mocking HttpClient
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                true,
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(GetAdvertById(4)))
                }).Verifiable();

            var _httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com")
            };
            var mockLogger = new Mock<ILogger<AdvertService>>();

            var service = new AdvertService(_httpClient, mockLogger.Object);
            var task = service.FindById(4);
            var advert = task.Result as Advert;

            Assert.True(task.IsCompleted);
            Assert.NotNull(task.Result);
            Assert.Equal(4, advert.ID);
            Assert.Equal("Tyres, Mag Wheels", advert.Detail.Title);
            Assert.Equal("Your Professional Tyre Fitment Centre", advert.Detail.Body);
            Assert.Equal("Gaborone", advert.Detail.Location);
        }
        private List<Advert> GetAdverts()
        {
            return Adverts;
        }
        private Advert GetAdvertById(int id)
        {
            return Adverts.FirstOrDefault(x => x.ID == id);
        }
        private void Initialize()
        {
           Adverts = new List<Advert>
            {
                new Advert{ID=1, SubmittedDate= new DateTime(2019,1,3)},
                new Advert{ID=2, SubmittedDate= new DateTime(2018,10,5)},
                new Advert{ID=3, SubmittedDate= new DateTime(2018,12,20)},
                new Advert{ID=4, SubmittedDate= new DateTime(2019,3,11),
                Detail = new AdvertDetail{
                    ID=3,Title="Tyres, Mag Wheels",
                    Body="Your Professional Tyre Fitment Centre",
                    Email="my@email.com",
                    Location="Gaborone"}
                },
                new Advert{ID=5, SubmittedDate= new DateTime(2018,10,15)},
                new Advert{ID=6, SubmittedDate= new DateTime(2019,2,28)},
                new Advert{ID=7, SubmittedDate= new DateTime(2018,7,3)}
            };
          
        }
    }
}
