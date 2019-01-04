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
    [TestFixture]
    public class InsertCommandHandlerTests
    {
        private InsertCommandHandler _target;
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
            InsertCommand insertCommand = null;
            _target = _fixture.Freeze<InsertCommandHandler>();
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _target.Handle(insertCommand, default(CancellationToken)));
        }

        [Test]
        public void When_InputIsValid_HandlerContactSuccessfully()
        {
            var expected = _fixture.Create<Guid>();
            var command = _fixture.Create<InsertCommand>();

            _mockContactsService = _fixture.Freeze<Mock<IContactService>>();
            _mockContactsService.Setup(x => x.CreateContactAsync(It.IsAny<Contact>())).ReturnsAsync(expected);

            _target = _fixture.Create<InsertCommandHandler>();

            var actual = _target.Handle(command, default(CancellationToken));

            Assert.AreEqual(expected, actual.Result);

            _mockContactsService.Verify(x => x.CreateContactAsync(It.IsAny<Contact>()), Times.Once);
        }
    }
}
