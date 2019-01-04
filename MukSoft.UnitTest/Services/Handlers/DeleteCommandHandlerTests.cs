using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using MukSoft.Core.Domain;
using MukSoft.Data.Interfaces;
using MukSoft.Services.Contact.Command;
using MukSoft.Services.Contact.CommandHandlers;
using NUnit.Framework;
using System;
using System.Threading;

namespace MukSoft.UnitTest.Services.Handlers
{
    public class DeleteCommandHandlerTests
    {
        private DeleteCommandHandler _target;
        private Mock<IContactService> _mockContactsService;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [TearDown]
        public void TearDown()
        {
            _mockContactsService = null;
            _fixture = null;
            _target = null;
        }
        [Test]
        public void When_InputIsNull_MapThrowsException()
        {
            DeleteCommand deleteCommand = null;
            _target = _fixture.Freeze<DeleteCommandHandler>();
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
              await _target.Handle(deleteCommand, default(CancellationToken)));
        }

        [Test]
        public void When_InputIsValid_HandlerDeleteContactLogSuccessfully()
        {
            var expected = _fixture.Create<bool>();
            var command = _fixture.Create<DeleteCommand>();

            _mockContactsService = _fixture.Freeze<Mock<IContactService>>();
            _mockContactsService.Setup(x => x.DeleteContactAsync(It.IsAny<Contact>())).ReturnsAsync(expected);

            _target = _fixture.Create<DeleteCommandHandler>();

            var actual = _target.Handle(command, default(CancellationToken));
            Assert.AreEqual(expected, actual.Result);
            _mockContactsService.Verify(x => x.DeleteContactAsync(It.IsAny<Contact>()), Times.Once);
        }

        [Test]
        public void When_ServiceFails_HandlerThrowsException()
        {
            var command = _fixture.Create<DeleteCommand>();

            _mockContactsService = _fixture.Freeze<Mock<IContactService>>();
            _mockContactsService.Setup(x => x.DeleteContactAsync(It.IsAny<Contact>())).ThrowsAsync(new ArgumentNullException());

            _target = _fixture.Create<DeleteCommandHandler>();

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _target.Handle(command, default(CancellationToken)));

            _mockContactsService.Verify(x => x.DeleteContactAsync(It.IsAny<Contact>()), Times.Once);

        }
    }
}
