using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassifiedsBlazor.Controllers;
using ClassifiedsBlazor.Entities;
using ClassifiedsBlazor.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace XUnitTestProject.Controller
{
    public class TestAdvertController
    {
        [Fact]
        public void TestFindAll()
        {
            var mockAdvertRepo = new Mock<IAdvertRepo>();
            mockAdvertRepo.Setup(x => x.FindAll())
                .Returns(Task.FromResult(GetAdverts()));

            var controller = new AdvertController(mockAdvertRepo.Object);
            var task = controller.FindAll();
            var result = task.Result as OkObjectResult;
            var value = result.Value as List<Advert>;

            Assert.NotNull(task);
            Assert.NotNull(result);
            Assert.NotNull(value);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(7, value.Count);
        }
        [Fact]
        public void TestFindAll_Exception()
        {

            var mockAdvertRepo = new Mock<IAdvertRepo>();
            mockAdvertRepo.Setup(x => x.FindAll())
                .Throws(new Exception());

            var controller = new AdvertController(mockAdvertRepo.Object);
            var task = controller.FindAll();
            var result = task.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal("Error retrieving objects from the database", result.Value);
                
        }
        /// <summary>
        /// Retrieve advert list
        /// </summary>
        private List<Advert> GetAdverts() {
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

