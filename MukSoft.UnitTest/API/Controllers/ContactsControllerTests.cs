using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MukSoft.API.Controllers;
using MukSoft.Core.Domain;
using MukSoft.Core.DTO;
using MukSoft.Services.Contact.Command;
using MukSoft.Services.Contact.Query;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.UnitTest.API.Controllers
{
    [TestFixture]
    public class ContactsControllerTests
    {
        private Mock<IMediator> _mediator;
        private ContactsController _target;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _target = new ContactsController(_mediator.Object);

        }

        [TearDown]
        public void TearDown()
        {
            _fixture = null;
            _target = null;
            _mediator = null;
        }

        #region Insert 

        [Test]
        public async Task WhenInsert_InputIsNull_ControllerReturnsBadRequestObjectResult()
        {
            const Contact input = null;
            var expected = _fixture.Create<Guid>();

            _mediator.Setup(x => x.Send(It.IsAny<InsertCommand>(), default(CancellationToken))).ReturnsAsync(expected);

            var result = await _target.Create(input) as ObjectResult;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task WhenInsert_InputIsValid_ControllerReturnsValidResponse()
        {
            Contact input = new Contact();
            var expected = _fixture.Create<Guid>();

            _mediator.Setup(x => x.Send(It.IsAny<InsertCommand>(), default(CancellationToken))).ReturnsAsync(expected);

            var result = await _target.Create(input);

            Assert.IsInstanceOf<CreatedResult>(result);
            Assert.NotNull(result);
        }

        #endregion

        #region Update
        [Test]
        public async Task WhenUpdatingContact_Model_IsNull_ControllerReturnsBadObject()
        {
            var input = "c09e1c5b-fa74-4436-aded-4fcb7bc9c7d2";
            _mediator.Setup(x => x.Send(It.IsAny<UpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<bool>);

            var result = await _target.Update(new Guid(input), It.IsAny<Contact>());

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

            Assert.NotNull(result);
        }

        [Test]
        public async Task WhenUpdatingContact_InputIsValid_ControllerReturnsValidResponse()
        {
            var input = "c09e1c5b-fa74-4436-aded-4fcb7bc9c7d2";
            var model = _fixture.Create<Contact>();

            _mediator.Setup(x => x.Send(It.IsAny<UpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<bool>);

            var result = await _target.Update(new Guid(input), model);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            Assert.NotNull(result);
        }
        #endregion

        #region Delete
        [Test]
        public async Task WhenDeletingContact_InputIsNull_ControllerReturnsBadRequestObjectResult()
        {
            var result = await _target.Delete(null) as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task WhenDeletingContact_InputIsEmptyString_ControllerReturnsBadRequestObjectResult()
        {
            var result = await _target.Delete(Guid.Empty) as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region Get
        [Test]
        public async Task WhenQueryingContact_InputIsValid_ControllerReturnsValidResponse()
        {
            var expected = _fixture.Create<IList<ContactDto>>();

            _mediator.Setup(x => x.Send(It.IsAny<ReadQuery>(), default(CancellationToken))).ReturnsAsync(expected);

            var result = await _target.GetAll() as ObjectResult;

            Assert.IsInstanceOf<ObjectResult>(result);
        }

        #endregion

        #region GetById
        [Test]
        public async Task WhenQueryingContact_InputIsEmptyString_ControllerReturnsBadRequestObjectResult()
        {
            var result = await _target.GetById(Guid.Empty);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task WhenQueryingContact_InputIsValid_ControllerReturnsValidResult()
        {
            var input = "c09e1c5b-fa74-4436-aded-4fcb7bc9c7d2";
            var expected = _fixture.Create<Task<ContactDto>>();

            _mediator.Setup(x => x.Send(It.IsAny<ReadByIdQuery>(), default(CancellationToken))).Returns(expected);

            var result = await _target.GetById(new Guid(input)) as ObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion
    }
}
