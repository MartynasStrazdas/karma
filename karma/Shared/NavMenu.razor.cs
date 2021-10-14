using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma.Components.Dialogs;


namespace karma.Shared
{
    public partial class NavMenu
    {
        private String MenuButtonClass = "nav-not-pressed"; //Determines <nav> class, to open mobile navbar.

        private void ExpandMenu()
        {
            MenuButtonClass = "nav-pressed"; //Changes <nav> class to expand mobile menu.
        }
        private void CloseMenu()
        {
            MenuButtonClass = "nav-not-pressed"; //Changes <nav> class to close mobile menu.
        }
        private async Task OpenDialog()
        {
            var dialog = _dialogService.Show<DialogLogin>("Login");
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
        }
    }
}
