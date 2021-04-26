using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraSocial.Models.API
{
    public class TokenCreateModel
    {
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
    }
}
