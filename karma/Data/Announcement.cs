using System;

namespace karma.Data
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }

        public Announcement(int Id, string Name, string Description, DateTime Added)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Added = Added;
        }
    }
}
