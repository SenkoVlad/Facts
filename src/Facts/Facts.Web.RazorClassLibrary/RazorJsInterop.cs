using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Facts.Web.RazorClassLibrary
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class RazorJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public RazorJsInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Facts.Web.RazorClassLibrary/RazorInterop.js").AsTask());
        }

        public async ValueTask<string> ShowToast(string title, string message, string type = "info")
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("showToast", title, message, type);
        }        
        
        public async ValueTask<string> CopyToClipboard(string text)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("copyToClipboard", text);
        }

        public async ValueTask SetTagTotal(int value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setTagsTotal", "TotalTags", value);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
