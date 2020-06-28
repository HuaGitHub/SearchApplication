using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SearchApplication.Controllers;
using SearchApplication.Data.Classes;
using SearchApplication.Services;
using SearchApplication.ViewModel;

namespace UnitTestProject
{
    public class ContactControllerTests
    {
        private ContactController _contactController;
        private Mock<IContactService> _contactMockService;
        private Mock<ILogger<ContactController>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            //Setup Mock services
            _contactMockService = new Mock<IContactService>();
            _loggerMock = new Mock<ILogger<ContactController>>();
            _mapperMock = new Mock<IMapper>();
            _contactController = new ContactController(_loggerMock.Object, _contactMockService.Object, _mapperMock.Object);
        }

        [Test]
        public void When_GetAllContactsIsSuccessful_Then_Status200WillBeReturned()
        {
            //Arrange 
            _contactMockService.Setup(x => x.GetAllContacts()).Returns(It.IsAny<IEnumerable<Contact>>());
            _mapperMock.Setup(x => x.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(It.IsAny<IEnumerable<Contact>>())).Returns(It.IsAny<IEnumerable<ContactViewModel>>());

            //Act
            var result = (ObjectResult)_contactController.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        [TestCase("Sid")]
        public void When_ContactsAreFoundInDB_Then_Status200WillBeReturned(string name)
        {
            //Arrange 
            var contactResponse = new List<Contact>()
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "Sidney",
                    LastName = "Crosby",
                    Age = 32,
                    Address = "15 Charles Street, Pittsburgh PA",
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Sid",
                    LastName = "Crosby",
                    Age = 34,
                    Address = "15 Charles Street, Pittsburgh PA",
                }
            };

            var mapperResponse = new List<ContactViewModel>()
            {
                new ContactViewModel()
                {
                    FirstName = "Sidney",
                    LastName = "Crosby",
                    Age = 32,
                    Address = "15 Charles Street, Pittsburgh PA",
                },
                new ContactViewModel()
                {
                    FirstName = "Sid",
                    LastName = "Crosby",
                    Age = 34,
                    Address = "15 Charles Street, Pittsburgh PA",
                }
            };

            _contactMockService.Setup(x => x.GetContact(It.IsAny<string>())).Returns(contactResponse);
            _mapperMock.Setup(x => x.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(It.IsAny<IEnumerable<Contact>>())).Returns(mapperResponse);

            //Act
            var result = (ObjectResult)_contactController.Get(name);
            var model = (IEnumerable<ContactViewModel>) result.Value;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [Test]
        [TestCase("NotFound")]
        public void When_GetContactReturnsNull_Then_Status404WillBeReturned(string name)
        {
            //Arrange 
            _contactMockService.Setup(x => x.GetContact(It.IsAny<string>())).Returns((List<Contact>)null);

            //Act
            var result = (NotFoundResult)_contactController.Get(name);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void When_AddContactPostIsSuccessful_Then_Status201WillBeReturned()
        {
            //Arrange 
            _contactMockService.Setup(x => x.AddContact(It.IsAny<Contact>()));
            _mapperMock.Setup(x => x.Map<ContactViewModel, Contact>(It.IsAny<ContactViewModel>())).Returns(It.IsAny<Contact>());

            //Act
            var result = (ObjectResult)_contactController.Post(new ContactViewModel());

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.True((bool) result.Value);
        }

        [Test]
        public void When_AddContactPostModelStateIsInvalid_Then_Status400WillBeReturned()
        {
            //Arrange 
            _contactController.ModelState.AddModelError("error", "ModelState is invalid");

            //Act
            var result = (ObjectResult)_contactController.Post(new ContactViewModel());

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
