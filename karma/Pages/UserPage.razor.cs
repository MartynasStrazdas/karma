using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;

namespace karma.Pages
{
    public partial class UserPage
    {
        private UserInfo _mainUser = UserInfo.GetInstance();
        private List<Announcement> _announcements;
        private List<Listing> _listings;

        protected override void OnInitialized()
        {
            using (var db = new db_a7d4c3_karmaContext())
            {
                _announcements = db.Announcements.OrderByDescending(x => x.Added).ToList();
                _listings = db.Listings.OrderByDescending(x => x.Added).ToList();
            }
        }
        async Task OpenDialogConfirmation(int Id)
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
    }
    
}
