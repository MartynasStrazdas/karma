using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace karma.Pages
{
    public partial class Charities
    {
        private UserInfo MainUser = UserInfo.GetInstance();
        private List<Charity> _charities;

        private static int addRemoveDialogsOpened;

        protected override async Task OnInitializedAsync()
        {
            addRemoveDialogsOpened = 0;

            using (var db = new db_a7d4c3_karmaContext())
            {
                // REQUIREMENT 1.4
                // REQUIREMENT 1.10
                _charities = await db.Charities.OrderByDescending(x => x.Added).ToListAsync();
            }
        }
        //REQUIREMENT 2.8
        async Task OpenDialog()
        {
            Interlocked.Increment(ref addRemoveDialogsOpened);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogAddNewCharity>("Add charity", options);
            var result = await dialog.Result;
            
            if (!result.Cancelled)
            {
                Charity charity = (Charity) result.Data;
                charity.Added = DateTime.Now;

                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Add(charity);
                    db.SaveChanges();
                }

                _charities.Insert(0, charity);
            }
        }

        //REQUIREMENT 2.8
        async Task OpenDialogConfirmation(int Id)
        {
            Interlocked.Increment(ref addRemoveDialogsOpened);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogConfirmation>("Confirmation", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var charity = new Charity { Id = Id };
                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Charities.Attach(charity);
                    db.Charities.Remove(charity);
                    db.SaveChanges();
                }
            }
        }
    }
}
