using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;

namespace karma.Pages
{
    public partial class Charities
    {
        private User.UserInfo MainUser = User.UserInfo.GetInstance();
        private List<Charity> _charities;

        protected override void OnInitialized()
        {
            using (var db = new dbkarmaContext())
            {
                // REQUIREMENT 1.4
                // REQUIREMENT 1.10
                _charities = db.Charities.OrderByDescending(x => x.Added).ToList();
            }
        }
        //REQUIREMENT 2.8
        async Task OpenDialog()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogAddNewCharity>("Add charity", options);
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
        async Task OpenDialogConfirmation(int Id)
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogConfirmation>("Confirmation", options);
            var result = await dialog.Result;


            if (!result.Cancelled)
            {
                var charity = new Charity { Id = Id };
                using (var db = new dbkarmaContext())
                {
                    db.Charities.Attach(charity);
                    db.Charities.Remove(charity);
                    db.SaveChanges();
                }
            }
        }
    }
}
