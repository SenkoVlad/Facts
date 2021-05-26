using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Facts.Web.Infrastructure.Services;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Facts.Web.Controllers.Facts.Queries
{
    public class FactRssRequest : RequestBase<string>
    {
        public int countLastFacts { get; set; } = 20;

        public FactRssRequest(int countLastFacts)
        {
            this.countLastFacts = countLastFacts;
        }
    }

    public class FactRssRequestHandler : RequestHandlerBase<FactRssRequest, string>
    {
        private readonly IFactService factService;

        public FactRssRequestHandler(IFactService factService)
        {
            this.factService = factService;
        }

        public async override Task<string> Handle(FactRssRequest request, CancellationToken cancellationToken)
        {
            await using var sw = new StringWriterWithEncoding(Encoding.UTF8);
            await using XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings
            {
                Async = true,
                Indent = true
            });
            var writer = new RssFeedWriter(xmlWriter);
            await writer.WriteTitle(".NET Programming");
            await writer.WriteDescription("RSS 2.0!");
            await writer.Write(new SyndicationLink(new Uri("https://www.calabonga.net")));
            await writer.Write(new SyndicationPerson("Calabonga", "dev@calabonga.net (Sergei Calabonga)", RssContributorTypes.ManagingEditor));
            await writer.WritePubDate(DateTimeOffset.Now);

            var posts = factService.GetLast(request.countLastFacts);
            foreach (var post in posts)
            {
                var item = new SyndicationItem
                {
                    Id = post.Id.ToString(),
                    Title = $"Факт на тему \"{post.Tags!.OrderBy(_ => Guid.NewGuid()).First().Name}\"",
                    Description = post.Content,
                    Published = post.CreatedAt,
                    LastUpdated = post.UpdatedAt ??= post.CreatedAt
                };

                item.AddLink(new SyndicationLink(new Uri($"https://www.calabonga.net/blog/post/{post.Id}")));
                foreach (var tag in post.Tags!)
                {
                    item.AddCategory(new SyndicationCategory($"{tag.Name}"));
                }

                item.AddContributor(new SyndicationPerson("Calabonga", "dev@calabonga.net (Sergei Calabonga)"));

                await writer.Write(item);
            }
            await writer.WriteRaw("</channel>");
            await writer.WriteRaw("</rss>");
            await xmlWriter.FlushAsync();
            return sw.ToString();

        }
    }
}
