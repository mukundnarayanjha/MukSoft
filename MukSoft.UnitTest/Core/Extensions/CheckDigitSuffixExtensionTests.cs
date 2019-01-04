using MukSoft.Core.Extensions;
using NUnit.Framework;
using System;

namespace MukSoft.UnitTest.Core.Extensions
{
    [TestFixture]
    public class CheckDigitSuffixExtensionTests
    {
        [Test]
        public void GetCheckDigitSuffix_ValidInput_GetsSuffixSuccessfully()
        {
            var input = "123456700";
            var expected = "1234567008";
            var actual = input.GetCheckDigitSuffix();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCheckDigitSuffix_InvalidLength_ThrowsArgumentException()
        {
            var input = "12345670000";
            Assert.Throws<ArgumentException>(() => input.GetCheckDigitSuffix());
        }

        [Test]
        public void GetCheckDigitSuffix_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.GetCheckDigitSuffix());
        }
    }
}
