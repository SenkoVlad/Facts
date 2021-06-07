using Facts.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facts.Web.RazorClassLibrary
{
    partial class TagsEditorComponent
    {
        [Parameter]
        public List<string> Tags { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        private ITagSearchService tagSearchService { get; set; }
        protected List<string> Founded { get; set; }
        protected string TagName { get; set; }

        protected override void OnInitialized()
        {
            if (Tags == null)
                Tags = new List<string>();
        }

        protected async Task DeleteTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }

            var tagToDelete = Tags.SingleOrDefault(x => x == tag);
            if (tagToDelete is null)
            {
                return;
            }

            Tags.Remove(tag);

            await new RazorJsInterop(JsRuntime).SetTagTotal(Tags.Count);
        }

        protected void SearchTag(ChangeEventArgs args)
        {
            if (args?.Value == null || string.IsNullOrWhiteSpace(args.Value.ToString()))
            {
                Founded = null;
                return;
            }
            Founded = tagSearchService.SearchTags(args.Value.ToString());
        }

        protected async Task AddTag(string value)
        {
            var tag = value?.ToLower().Trim();
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }

            Tags ??= new List<string>();

            if (!Tags.Exists(x => x.Equals(tag, StringComparison.InvariantCulture)))
            {
                Tags.Add(tag);
                // notify TagsTotal changed
                await new RazorJsInterop(JsRuntime).SetTagTotal(Tags.Count);
            }

            TagName = string.Empty;
            Founded = null;
        }
    }
}
