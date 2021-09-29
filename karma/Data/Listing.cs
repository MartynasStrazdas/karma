using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace karma.Data
{
    public class Listing
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Added { get; set; }

            public Listing(int Id, string Title, string Description, DateTime Added)
            {
                this.Id = Id;
                this.Title = Title;
                this.Description = Description;
                this.Added = Added;
            }
    }
}
