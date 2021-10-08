using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;

namespace karma.Pages
{
    public partial class Listings
    {
        private List<Listing> _listings;

        protected override void OnInitialized()
        {
            using (var db = new dbkarmaContext())
            {
                _listings = db.Listings.OrderByDescending(x => x.Added).ToList();
            }
        }

        // Define dialog
        async Task OpenDialog()
        {
            var dialog = _dialogService.Show<DialogAddNewListing>("Add listing");
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
    }
}