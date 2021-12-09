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
        private User.UserInfo MainUser = User.UserInfo.GetInstance();
        private List<Listing> _listings;

        protected override void OnInitialized()
        {
            using (var db = new dbkarmaContext())
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

                using (var db = new dbkarmaContext())
                {
                    db.Add(listing);
                    db.SaveChanges();
                }

                _listings.Insert(0, listing);
            }
        }
        async Task OpenDialogApply()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogApply>("Aplly", options);
            var result = await dialog.Result;
        }
        async Task OpenDialogConfirmation(int Id)
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogConfirmation>("Confirmation", options);
            var result = await dialog.Result;


            if (!result.Cancelled)
            {
                var listing = new Listing { Id = Id };
                using (var db = new dbkarmaContext())
                {
                    db.Listings.Attach(listing);
                    db.Listings.Remove(listing);
                    db.SaveChanges();
                }
            }
        }
    }
}
