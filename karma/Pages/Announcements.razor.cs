using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;
using MudBlazor;
using Microsoft.EntityFrameworkCore;


namespace karma.Pages
{
    public partial class Announcements
    {
        private UserInfo _mainUser = UserInfo.GetInstance();
        private List<Announcement> _announcements;

        //REQUIREMENT 2.1
        //REQUIREMENT 2.5
        public Lazy<string> _announcementTitleExample = new Lazy<string>(() => new string("Announcement Title Example"));
        private Lazy<string> _extremelyLongAnnouncementTitleExample = new Lazy<string>(() => new string("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent quis risus quis erat malesuada dignissim in ac tortor. Nullam sed mi lacus. Quisque viverra ex at elit consectetur, sed tempor tellus rhoncus. Vestibulum laoreet ipsum sit amet lorem ullamcorper viverra. Donec sed nibh congue, euismod nisl a, commodo neque. Integer cursus quam vitae odio condimentum fermentum. Morbi at erat libero. Integer a vehicula turpis, quis ornare purus. Nam est ante, accumsan eu maximus sit amet, pellentesque non leo. Maecenas nec ante mauris. Praesent ornare pulvinar ex eget eleifend. Praesent tincidunt et nunc in accumsan. Morbi erat turpis, aliquet sed urna. "));

        public class TitleTooLongException : Exception
        {
            public TitleTooLongException()
            {
                Console.WriteLine("The announcement title is too long.");
            }
        }
        public class CurseWordException : Exception
        {
            public CurseWordException()
            {
                Console.WriteLine("The announcement title contains curse words.");
            }
        }

        bool _isStringCorrect(string str)
        {
            if (str.Length > 100)
            {
                throw new TitleTooLongException();
            }
            if (str.Contains("ipsum"))
            {
                throw new CurseWordException();
            }
            return true;
        }

        private bool _printAnnouncementTitle = true;

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

        protected override async Task OnInitializedAsync()
        {
            // REQUIREMENT 2.4
            var titleAnnouncer = new TitleAnnouncer();
            var titleAnnouncerImpl = new TitleAnnouncerImpl();
            // linking the titleAnnouncer to it's "subscriber"
            titleAnnouncer.TitleCalled += titleAnnouncerImpl.OnTitleCalled;
            titleAnnouncer.PrintTitle("Local Man Absolutely Loves Using Delegates & Events");

            using (var db = new db_a7d4c3_karmaContext())
            {
                _announcements = await db.Announcements.OrderByDescending(x => x.Added).ToListAsync();
            }
            if (_printAnnouncementTitle)
            {
                try
                {
                    if (_isStringCorrect(_extremelyLongAnnouncementTitleExample.Value))
                    {
                        Console.WriteLine(_extremelyLongAnnouncementTitleExample.Value);
                    }
                }
                catch (TitleTooLongException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (CurseWordException ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Add(announcement);
                    db.SaveChanges();
                }

                _announcements.Insert(0, announcement);
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
                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Announcements.Attach(announcement);
                    db.Announcements.Remove(announcement);
                    db.SaveChanges();
                    _announcements = await db.Announcements.OrderByDescending(x => x.Added).ToListAsync();
                }
            }
        }

        async Task OpenDialogUpdateAnnouncement(int Id)
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var dialog = _dialogService.Show<DialogUpdateAnnouncement>("Update", options);
            var result = await dialog.Result;
            
            if (!result.Cancelled)
            {   
                var announcement = (Announcement) result.Data;
                announcement.Id = Id;
                using (var db = new db_a7d4c3_karmaContext())
                {
                    db.Announcements.Attach(announcement);
                    db.Announcements.Update(announcement);
                    db.SaveChanges();
                    _announcements = await db.Announcements.OrderByDescending(x => x.Added).ToListAsync();
                }
            }
        }
    }
}