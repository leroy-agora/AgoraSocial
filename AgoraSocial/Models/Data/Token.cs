using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraSocial.Models.Data
{
    public class Token
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User ID of token creator
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Client ID of the content provider
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Unique identifier of the piece of content
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// URI for the content
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// True if token has been consumed
        /// </summary>
        public bool Redeemed { get; set; }

        /// <summary>
        /// User who consumed the token
        /// </summary>
        public string RedeemedBy { get; set; }
    }
}
