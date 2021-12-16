using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace karma.Data
{
    public class RenderedMessage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Img { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public int ListerId { get; set; }

    }
}
