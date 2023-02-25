// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanTransactionsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Ccdscan.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the Ccdscan transactions.
    /// </summary>
    public class CcdscanTransactionsRequest
    {
        /// <summary>
        /// Initialize <see cref="CcdscanTransactionsRequest"/>
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="first">Get first transactions.</param>
        /// <param name="after">After.</param>
        public CcdscanTransactionsRequest(
            string query,
            int first = 50,
            string? after = null)
        {
            Query = query;
            First = first;
            After = after;
        }

        /// <summary>
        /// Search query.
        /// </summary>
        /// <remarks>
        /// For example - account address.
        /// </remarks>
        /// <example>4D44RYigFqPkABrRAHXSBBQqG4VNhXEsyJrt2GH6V2H8tS1tN3</example>
        public string? Query { get; set; }

        /// <summary>
        /// Get first transactions.
        /// </summary>
        /// <example>50</example>
        public int First { get; set; }

        /// <summary>
        /// After.
        /// </summary>
        /// <example>Mzc4NjQ5</example>
        public string? After { get; set; }
    }
}