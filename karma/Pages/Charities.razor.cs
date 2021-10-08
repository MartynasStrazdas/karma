using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;

namespace karma.Pages
{
    public partial class Charities
    {
        private List<Charity> _charities;

        protected override void OnInitialized()
        {
            using (var db = new dbkarmaContext())
            {
                _charities = db.Charities.OrderByDescending(x => x.Added).ToList();
            }
        }

        async Task OpenDialog()
        {
            var dialog = _dialogService.Show<DialogAddNewCharity>("Add charity");
            var result = await dialog.Result;
            
            if (!result.Cancelled)
            {
                Charity charity = (Charity) result.Data;
                charity.Added = DateTime.Now;

                using (var db = new dbkarmaContext())
                {
                    db.Add(charity);
                    db.SaveChanges();
                }

                _charities.Insert(0, charity);
            }
        }
    }
}