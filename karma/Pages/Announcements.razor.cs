using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using karma.Ex;
using MudBlazor;

namespace karma.Pages
{
    public partial class Announcements
    {
        private User.UserInfo MainUser = User.UserInfo.GetInstance();
        private List<Announcement> _announcements;

        //REQUIREMENT 2.1
        //REQUIREMENT 2.5
        public Lazy<string> _announcementTitleExample = new Lazy<string>(() => new string("Announcement Title Example"));
        private Lazy<string> _extremelyLongAnnouncementTitleExample = new Lazy<string>(() => new string("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent quis risus quis erat malesuada dignissim in ac tortor. Nullam sed mi lacus. Quisque viverra ex at elit consectetur, sed tempor tellus rhoncus. Vestibulum laoreet ipsum sit amet lorem ullamcorper viverra. Donec sed nibh congue, euismod nisl a, commodo neque. Integer cursus quam vitae odio condimentum fermentum. Morbi at erat libero. Integer a vehicula turpis, quis ornare purus. Nam est ante, accumsan eu maximus sit amet, pellentesque non leo. Maecenas nec ante mauris. Praesent ornare pulvinar ex eget eleifend. Praesent tincidunt et nunc in accumsan. Morbi erat turpis, aliquet sed urna. "));
       // private bool _printAnnouncementTitle = true;

        // REQUIREMENT 2.4
        // The "publisher"
        class TitleAnnouncer
        {
            public delegate void TitleEventHandler(object o);
            public event TitleEventHandler TitleCalled;
            public void PrintTitle(string title)
            {
                Console.WriteLine(title);
                OnTitleCalled();
            }

            protected virtual void OnTitleCalled()
            {
                if(TitleCalled != null)
                {
                    TitleCalled(this);
                }
            }
        }

        // REQUIREMENT 2.4
        // The "subscriber"
        class TitleAnnouncerImpl
        {
            public void OnTitleCalled(object o)
            {
                Console.WriteLine("i got the message");
            }
        }

        protected override void OnInitialized()
        {
            // REQUIREMENT 2.4
            var titleAnnouncer = new TitleAnnouncer();
            var titleAnnouncerImpl = new TitleAnnouncerImpl();
            // linking the titleAnnouncer to it's "subscriber"
            titleAnnouncer.TitleCalled += titleAnnouncerImpl.OnTitleCalled;
            titleAnnouncer.PrintTitle("Local Man Absolutely Loves Using Delegates & Events");

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
                try
                {
                    if (NameChecker.isStringCorrect(announcement.Title))
                    {
                        using (var db = new dbkarmaContext())
                        {
                            db.Add(announcement);
                            db.SaveChanges();
                        }
                        _announcements.Insert(0, announcement);
                        Snackbar.Add("Announcement added!", Severity.Success);
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

        async Task OpenDialogConfirmation(int Id)
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogConfirmation>("Confirmation", options);
            var result = await dialog.Result;
            

            if (!result.Cancelled)
            {   
                var announcement = new Announcement { Id = Id };
                using (var db = new dbkarmaContext())
                {
                    db.Announcements.Attach(announcement);
                    db.Announcements.Remove(announcement);
                    db.SaveChanges();
                }
            }
        }
    }
}