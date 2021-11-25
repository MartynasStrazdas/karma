using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;
using System.Threading;
using karma.Ex;
namespace karma.Pages
{
    public partial class Charities
    {
        private User.UserInfo MainUser = User.UserInfo.GetInstance();
        private List<Charity> _charities;

        private static int addRemoveDialogsOpened;

        protected override void OnInitialized()
        {
            addRemoveDialogsOpened = 0;

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
            Interlocked.Increment(ref addRemoveDialogsOpened);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogAddNewCharity>("Add charity", options);
            var result = await dialog.Result;
            

            if (!result.Cancelled)
            {
                Charity charity = (Charity) result.Data;
                charity.Added = DateTime.Now;
                try
                {
                    if (NameChecker.isStringCorrect(charity.Name))
                    {
                        using (var db = new dbkarmaContext())
                        {
                            db.Add(charity);
                            db.SaveChanges();
                        }
                        _charities.Insert(0, charity);
                        Snackbar.Add("Charity added!", Severity.Success);
                    }
                }
                catch (TitleTooLongException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (CurseWordException ex)
                {
                    Snackbar.Add(ex.Message, Severity.Error);
                }
            

                
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
