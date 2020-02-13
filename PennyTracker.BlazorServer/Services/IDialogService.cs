using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using Radzen;

namespace PennyTracker.BlazorServer.Services
{
    public interface IDialogService
    {
        Task<dynamic> OpenAsync<T>(
            string title,
            Dictionary<string, object> parameters = null,
            DialogOptions options = null) where T : ComponentBase;

        void Close(dynamic result = null);
    }
}
