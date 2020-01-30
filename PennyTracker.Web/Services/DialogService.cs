using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using Radzen;

namespace PennyTracker.Web.Services
{
    public class AppDialogService : IDialogService
    {
        private readonly DialogService dialogService;

        public AppDialogService(DialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public void Close(dynamic result = null)
        {
            this.dialogService.Close(result);
        }

        public async Task<dynamic> OpenAsync<T>(
            string title,
            Dictionary<string, object> parameters = null,
            DialogOptions options = null) where T : ComponentBase
        {
            return await this.dialogService.OpenAsync<T>(
                title,
                parameters,
                options);
        }
    }
}
