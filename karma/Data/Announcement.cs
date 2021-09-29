using System;

namespace karma.Data
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }

        public Announcement(int Id, string Title, string Description, DateTime Added)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Added = Added;
        }
    }
}
