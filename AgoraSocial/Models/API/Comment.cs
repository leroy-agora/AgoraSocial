using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraSocial.Models.API
{
    public class Comment
    {
        public string Message { get; set; }
        public Guid Parent { get; set; }

    }
}
