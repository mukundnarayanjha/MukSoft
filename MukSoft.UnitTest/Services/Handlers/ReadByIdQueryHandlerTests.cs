using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using MukSoft.Core.Domain;
using MukSoft.Core.DTO;
using MukSoft.Data.Interfaces;
using MukSoft.Services.Contact.Query;
using MukSoft.Services.Contact.QueryHandlers;
using NUnit.Framework;
using System;
using System.Threading;

namespace MukSoft.UnitTest.Services.Handlers
{
    public class ReadByIdQueryHandlerTests
    {

        private ReadByIdQueryHandler _target;
        private Mock<IMapper> _mapper;
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
            _mapper = null;
        }
        [Test]
        public void When_InputIsNull_MapThrowsException()
        {
            ReadByIdQuery getReadByIdQuery = null;
            _target = _fixture.Freeze<ReadByIdQueryHandler>();
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _target.Handle(getReadByIdQuery, default(CancellationToken)));
        }
        [Test]
        public void When_InputIsValid_HandlerContactSuccessfully()
        {
            var expected = _fixture.Create<ContactDto>();
            var query = _fixture.Create<ReadByIdQuery>();
            var expectedModel = _fixture.Create<Contact>();

            _mapper = _fixture.Freeze<Mock<IMapper>>();
            _mapper.Setup(x => x.Map<ContactDto>(It.IsAny<Contact>())).Returns(expected);

            _mockContactsService = _fixture.Freeze<Mock<IContactService>>();
            _mockContactsService.Setup(x => x.GetContactByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedModel);

            _target = _fixture.Create<ReadByIdQueryHandler>();

            var actual = _target.Handle(query, default(CancellationToken));

            Assert.AreEqual(expected, actual.Result);

            _mapper.Verify(x => x.Map<ContactDto>(It.IsAny<Contact>()), Times.Once);

            _mockContactsService.Verify(x => x.GetContactByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
