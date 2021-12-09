using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;

namespace karma.Pages
{
    public partial class Listings
    {
        private UserInfo MainUser = UserInfo.GetInstance();
        private List<Listing> _listings;

        protected override void OnInitialized()
        {
            using (var db = new db_a7d4c3_karmaContext())
            {
                _listings = db.Listings.OrderByDescending(x => x.Added).ToList();
            }
        }

        // Define dialog
        //REQUIREMENT 2.8
        async Task OpenDialog()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogAddNewListing>("Add listing", options);
            var result = await dialog.Result;
            
            if (!result.Cancelled)
            {
                Listing listing = (Listing) result.Data;
                listing.Added = DateTime.Now;

                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Add(listing);
                    db.SaveChanges();
                }

                _listings.Insert(0, listing);
            }
        }
        async Task OpenDialogApply(int Id)
        {
            var parameters = new DialogParameters { ["id"] = Id };
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogApply>("Apply",parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                Message message = (Message) result.Data;
                message.ListingId = Id;
                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Messages.Add(message);
                    db.SaveChanges();
                }
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
                }
            }
        }
    }
}
