using karma.Components.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace karma.Pages
{
    public partial class Index
    {
        private async Task OpenDialogRegister()
        {
            var dialog = _dialogService.Show<DialogRegister>("Register");
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
        }
    }
}
