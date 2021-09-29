using System;

namespace karma.Data
{
    public class Charity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }
        public string Website { get; set; }

        public Charity(int Id, string Name, string Description, DateTime Added, string Website)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Added = Added;
            this.Website = Website;
        }
        public Charity(string Name, string Description, string Website)
        {
            this.Name = Name;
            this.Description = Description;
            this.Website = Website;
        }
    }
}
