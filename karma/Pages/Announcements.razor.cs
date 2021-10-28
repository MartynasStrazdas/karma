using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;

namespace karma.Pages
{
    public partial class Announcements
    {
        private List<Announcement> _announcements;

        protected override void OnInitialized()
        {
            using (var db = new dbkarmaContext())
            {
                _announcements = db.Announcements.OrderByDescending(x => x.Added).ToList();
            }
        }

        // Define dialog
        //REQUIREMENT 2.8
        async Task OpenDialog()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogAddNewAnnouncement>("Add announcement", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Announcement announcement = (Announcement) result.Data;
                announcement.Added = DateTime.Now;
                announcement.ValidUntil = DateTime.Now;

                using (var db = new dbkarmaContext())
                {
                    db.Add(announcement);
                    db.SaveChanges();
                }

                _announcements.Insert(0, announcement);
            }
        }
    }
}