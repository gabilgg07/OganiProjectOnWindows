using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogani.WebUI.Models.ViewModel
{
    public class PagedViewModel<T>
        where T : class
    {
        const int maxPaginationButtonCount = 5;

        public IEnumerable<T> Items { get; set; }
        public int CurrentIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }

        public int MaxPageIndex
        {
            get
            {
                return (int)Math.Ceiling(TotalCount * 1.0 / PageSize);
            }
        }


        public PagedViewModel(IQueryable<T> query, int pageIndex, int pageSize, int? categoryId = null, int? tagId = null)
        {
            this.PageSize = pageSize;
            this.TotalCount = query.Count();

            if (pageIndex > MaxPageIndex && pageIndex > 1)
                pageIndex = MaxPageIndex;

            this.Items = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            this.CurrentIndex = pageIndex;

            this.CategoryId = categoryId;
            this.TagId = tagId;
        }

        public HtmlString GetPagination(IUrlHelper urlHelper, string action, string area = "")
        {
            if (PageSize >= TotalCount)
                return HtmlString.Empty;


            StringBuilder builder = new StringBuilder();

            builder.Append("<ul class='pagination'>");

            if (CurrentIndex > 1)
            {
                var link = urlHelper.Action(action, values: new
                {
                    pageindex = CurrentIndex - 1,
                    pagesize = PageSize,
                    categoryId = CategoryId,
                    tagId = TagId,
                    area
                });


                builder.Append($@"<li class='prev'>
                                <a href='{link}'><i class='fa fa-chevron-left'></i></a>
                                </li>");
            }
            else
            {
                builder.Append(" <li class='prev disabled'>" +
                   "<a><i class='fa fa-chevron-left'></i></a></li>");
            }

            int min = 1, max = MaxPageIndex;

            if (CurrentIndex > (int)Math.Floor(maxPaginationButtonCount / 2D))
            {
                min = CurrentIndex - (int)Math.Floor(maxPaginationButtonCount / 2D);
            }

            max = min + maxPaginationButtonCount - 1;

            if (max > MaxPageIndex)
            {
                max = MaxPageIndex;
                min = max - maxPaginationButtonCount + 1;
            }

            for (int i = (min < 1 ? 1 : min); i <= max; i++)
            {
                if (i == CurrentIndex)
                {
                    builder.Append($"<li class='active'><a>{i}</a></li>");
                    continue;
                }

                var link = urlHelper.Action(action, values: new
                {
                    pageindex = i,
                    pagesize = PageSize,
                    categoryId = CategoryId,
                    tagId = TagId,
                    area
                });

                builder.Append($"<li><a href='{link}'>{i}</a></li>");

            }


            if (CurrentIndex < MaxPageIndex)
            {
                var link = urlHelper.Action(action, values: new
                {
                    pageindex = CurrentIndex + 1,
                    pagesize = PageSize,
                    categoryId = CategoryId,
                    tagId = TagId,
                    area
                });

                builder.Append($@"<li class='next'>
                                <a href='{link}'><i class='fa fa-chevron-right'></i></a>
                                </li>");
            }
            else
            {
                builder.Append(" <li class='next disabled'>" +
                   "<a><i class='fa fa-chevron-right'></i></a></li>");
            }


            builder.Append("</ul>");

            return new HtmlString(builder.ToString());
        }
    }
}
