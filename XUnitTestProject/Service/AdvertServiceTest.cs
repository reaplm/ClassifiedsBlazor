using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using ClassifiedsBlazor.Entities;
using ClassifiedsBlazor.Repository;
using ClassifiedsBlazor.Repository.Impl;
using ClassifiedsBlazor.Services.Impl;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTestProject.Service
{
    public class AdvertServiceTest
    {
        [Fact]
        public void TestFindAll()
        {
            //Mocking HttpCleint
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

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com")
            };

            var service = new AdvertService(httpClient); 
            var task = service.FindAll();
            var adverts = task.Result as IEnumerable<Advert>;
         
            Assert.True(task.IsCompleted);
            Assert.NotNull(task.Result);
            Assert.Equal(7, adverts.Count());
        }
        private List<Advert> GetAdverts()
        {
            List<Advert> adverts = new List<Advert>
            {
                new Advert{ID=1, SubmittedDate= new DateTime(2019,1,3)},
                new Advert{ID=2, SubmittedDate= new DateTime(2018,10,5)},
                new Advert{ID=3, SubmittedDate= new DateTime(2018,12,20)},
                new Advert{ID=4, SubmittedDate= new DateTime(2019,3,11)},
                new Advert{ID=5, SubmittedDate= new DateTime(2018,10,15)},
                new Advert{ID=6, SubmittedDate= new DateTime(2019,2,28)},
                new Advert{ID=7, SubmittedDate= new DateTime(2018,7,3)}
            };
            return adverts;
        }
    }
}
