using MukSoft.Core.Extensions;
using NUnit.Framework;
using System;

namespace MukSoft.UnitTest.Core.Extensions
{
    [TestFixture]
    public class NullExtensionTests
    {
        [Test]
        public void ThrowIfNull_NullInput_ThrowsException()
        {
            string input = null;
            Assert.Throws<ArgumentNullException>(() => input.ThrowIfNull(nameof(input)));
        }
    }
}
