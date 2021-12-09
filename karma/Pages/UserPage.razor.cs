using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;
using Microsoft.EntityFrameworkCore;

namespace karma.Pages
{
    public partial class UserPage
    {
        private UserInfo _mainUser = UserInfo.GetInstance();
        private List<Announcement> _announcements;
        private List<Listing> _listings;
        private List<Message> _messages;
        private List<User> _users;
      
        public User getUserByID(int thisId)
        {
            using (var db = new db_a7d4c3_karmaContext())
            {
                User user = db.Users.Where(b => b.Id == thisId).FirstOrDefault();
                return user;
            }
            
        }
        protected override async Task OnInitializedAsync()
        {
            using (var db = new db_a7d4c3_karmaContext())
            {
                _announcements = await db.Announcements.OrderByDescending(x => x.Added).ToListAsync();
                _listings = await db.Listings.OrderByDescending(x => x.Added).ToListAsync();
                _messages = await db.Messages.ToListAsync();
            }
        }
        async Task OpenDialogConfirmationListing(int Id)
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogConfirmation>("Confirmation", options);
            var result = await dialog.Result;


            if (!result.Cancelled)
            {
                var listing = new Listing { Id = Id };
                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Listings.Attach(listing);
                    db.Listings.Remove(listing);
                    db.SaveChanges();
                    NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
                }
            }
        }
        async Task OpenDialogConfirmationAnnouncement(int Id)
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogConfirmation>("Confirmation", options);
            var result = await dialog.Result;


            if (!result.Cancelled)
            {
                var announcement = new Announcement { Id = Id };
                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Announcements.Attach(announcement);
                    db.Announcements.Remove(announcement);
                    db.SaveChanges();
                }
            }
        }
    }
    
}
