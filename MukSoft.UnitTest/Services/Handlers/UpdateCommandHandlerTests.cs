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
    public class UpdateCommandHandlerTests
    {
        private UpdateCommandHandler _target;
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
            UpdateCommand updateCommand = null;
            _target = _fixture.Freeze<UpdateCommandHandler>();
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _target.Handle(updateCommand, default(CancellationToken)));
        }

        [Test]
        public void When_InputIsValid_HandlerContactSuccessfully()
        {
            var expected = _fixture.Create<bool>();
            var command = _fixture.Create<UpdateCommand>();

            _mockContactsService = _fixture.Freeze<Mock<IContactService>>();
            _mockContactsService.Setup(x => x.UpdateContactAsync(It.IsAny<Contact>())).ReturnsAsync(expected);
            _target = _fixture.Create<UpdateCommandHandler>();

            var actual = _target.Handle(command, default(CancellationToken));

            Assert.AreEqual(expected, actual.Result);

            _mockContactsService.Verify(x => x.UpdateContactAsync(It.IsAny<Contact>()), Times.Once);
        }
        [Test]
        public void When_ContactServiceFails_HandlerThrowsException()
        {
            var command = _fixture.Create<UpdateCommand>();

            _mockContactsService = _fixture.Freeze<Mock<IContactService>>();
            _mockContactsService.Setup(x => x.UpdateContactAsync(It.IsAny<Contact>()))
                    .ThrowsAsync(new ArgumentNullException());

            _target = _fixture.Create<UpdateCommandHandler>();

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _target.Handle(command, default(CancellationToken)));

            _mockContactsService.Verify(x => x.UpdateContactAsync(It.IsAny<Contact>()), Times.Once);

        }
    }
}

