using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Advert> Adverts;

        public TestAdvertController()
        {
            Initialize();
        }
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
        [Fact]
        public void TestFindById()
        {
            var mockAdvertRepo = new Mock<IAdvertRepo>();
            mockAdvertRepo.Setup(x => x.FindById(4))
                .Returns(Task.FromResult(GetAdvertById(4)));

            var controller = new AdvertController(mockAdvertRepo.Object);
            var task = controller.FindById(4);
            var result = task.Result as OkObjectResult;
            var value = result.Value as Advert;

            Assert.NotNull(task);
            Assert.NotNull(result);
            Assert.NotNull(value);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(4, value.ID);
            Assert.Equal("Tyres, Mag Wheels", value.Detail.Title);
            Assert.Equal("Your Professional Tyre Fitment Centre", value.Detail.Body);
            Assert.Equal("Gaborone", value.Detail.Location);
        }
        [Fact]
        public void TestFindById_Exception()
        {

            var mockAdvertRepo = new Mock<IAdvertRepo>();
            mockAdvertRepo.Setup(x => x.FindById(4))
                .Throws(new Exception());

            var controller = new AdvertController(mockAdvertRepo.Object);
            var task = controller.FindById(4);
            var result = task.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal("Error retrieving object from the database", result.Value);

        }
        /// <summary>
        /// Retrieve advert list
        /// </summary>
        private List<Advert> GetAdverts() {
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

