﻿using Facts.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.Helpers
{
    public class TagCloudHelper
    {
        public static List<TagCloudViewModel> Generate(List<TagCloudViewModel> items, int clusterCount)
        {
            var totalCount = items.Count;
            var tagsCloud = items.OrderBy(x => x.Total).ToList();

            var clusters = new List<List<TagCloudViewModel>>();
            if (totalCount > 0)
            {
                var min = tagsCloud.Min(c => c.Total);
                var max = tagsCloud.Max(c => c.Total) + min;
                var completeRange = max - min;
                var groupRange = completeRange / (double)clusterCount;
                var cluster = new List<TagCloudViewModel>();
                var currentRange = min + groupRange;
                for (var i = 0; i < totalCount; i++)
                {
                    while (tagsCloud.ToArray()[i].Total > currentRange)
                    {
                        clusters.Add(cluster);
                        cluster = new List<TagCloudViewModel>();
                        currentRange += groupRange;
                    }
                    cluster.Add(tagsCloud.ToArray()[i]);
                }
                clusters.Add(cluster);
            }
            var result = new List<TagCloudViewModel>();
            for (var i = 0; i < clusters.Count; i++)
            {
                result.AddRange(clusters[i].Select(item => new TagCloudViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    CssClass = "tag" + i,
                    Total = item.Total
                }));
            }

            return result.OrderBy(x => x.Name).ToList();
        }
    }
}