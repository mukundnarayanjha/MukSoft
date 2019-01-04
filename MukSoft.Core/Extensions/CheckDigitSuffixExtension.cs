using System;
using System.Diagnostics.CodeAnalysis;

namespace MukSoft.Core.Extensions
{
    public static class CheckDigitSuffixExtension
    {
        [ExcludeFromCodeCoverage]
        public static string GetCheckDigitSuffix(this string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number.Length != 9) throw new ArgumentException(nameof(number));
            var sum = 0;
            for (int i = number.Length - 1, multiplier = 2; i >= 0; i--)
            {
                sum += (int)char.GetNumericValue(number[i]) * multiplier;
                multiplier = multiplier + 1;
            }
            var validator = (11 - sum % 11).ToString();
            if (validator == "11") validator = "0";
            else if (validator == "10") validator = "X";
            return $"{number}{validator}";

        }
    }
}