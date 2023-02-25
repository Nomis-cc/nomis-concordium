// ------------------------------------------------------------------------------------------------------
// <copyright file="ConcordiumHelpers.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

namespace Nomis.Ccdscan.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods for Concordium.
    /// </summary>
    public static class ConcordiumHelpers
    {
        /// <summary>
        /// Convert Wei value to Concordium.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Concordium.</returns>
        public static decimal ToC(this string? valueInWei)
        {
            return BigInteger
                .TryParse(valueInWei, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out var value)
                ? value.ToC()
                : 0;
        }

        /// <summary>
        /// Convert Wei value to Concordium.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Concordium.</returns>
        public static decimal ToC(this in BigInteger valueInWei)
        {
            if (valueInWei > new BigInteger(decimal.MaxValue))
            {
                return (decimal)(valueInWei / new BigInteger(100_000));
            }

            return (decimal)valueInWei * 0.000_001M;
        }

        /// <summary>
        /// Convert Wei value to Concordium.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Concordium.</returns>
        public static decimal ToC(this in decimal valueInWei)
        {
            return new BigInteger(valueInWei).ToC();
        }
    }
}