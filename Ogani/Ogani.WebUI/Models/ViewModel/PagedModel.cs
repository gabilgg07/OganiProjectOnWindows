 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.Models.ViewModel
{
	public class PagedModel<T>
		 where T : class
	{
		public List<T> Items { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public int TotalCount { get; set; }

        public PagedModel(IQueryable<T> query, int pageIndex, int pageSize)
		{
			this.Items = query.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			this.TotalCount = query.Count();
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
        }

		public IHtmlContent GetPagination(IUrlHelper urlHelper, string actionName)
		{
			if (TotalCount <= PageSize)
                return HtmlString.Empty;

			int maxPageCount = (int)Math.Ceiling(TotalCount * 1.0 / PageSize);

			StringBuilder sb = new StringBuilder();
			UrlCreator uc = new UrlCreator(urlHelper, actionName, PageSize);
			string activeC = "class='active'";
			

            sb.Append("<div class= 'product-pagination'>");

			if (maxPageCount > 3)
			{
				if (PageIndex > 3)
				{
                    sb.Append($"<a href='{uc.GetUrl(1)}'><i class='fa-solid fa-angles-left'></i></a>");
                }
				if (PageIndex > 2)
				{
                    sb.Append($"<a href='{uc.GetUrl(PageIndex - 1)}'><i class= 'fa fa-long-arrow-left'></i></a>");
                }

				if (PageIndex <= 2)
				{

                    for (int i = 1; i <= 3; i++)
                    {
                        sb.Append($"<a href='{uc.GetUrl(i)}' {((i==PageIndex) ? activeC : "")}> {i} </a>");
                    }
				}
				else if (PageIndex > 2)
				{
					if (maxPageCount-PageIndex>0)
                    {
                        for (int i = PageIndex - 1; i <= PageIndex + 1; i++)
                        {
                            sb.Append($"<a href='{uc.GetUrl(i)}' {((i == PageIndex) ? activeC : "")}> {i} </a>");
                        }
					}
					else
					{
                        for (int i = maxPageCount - 2; i <= maxPageCount; i++)
                        {
                            sb.Append($"<a href='{uc.GetUrl(i)}' {((i == PageIndex) ? activeC : "")}> {i} </a>");
                        }
                    }
                }


				if ((PageIndex + 1) < maxPageCount)
				{
                    sb.Append($"<a href='{uc.GetUrl(PageIndex + 1)}'><i class= 'fa fa-long-arrow-right'></i></a>");
                }


                if ((PageIndex + 2) < maxPageCount)
                {
                    sb.Append($"<a href='{uc.GetUrl(maxPageCount)}'><i class='fa-solid fa-angles-right'></i></a>");
                }

            }
			else
			{
                for (int i = 1; i <= maxPageCount; i++)
                {
                    sb.Append($"<a href='{uc.GetUrl(i)}' {((i == PageIndex) ? activeC : "")}> {i} </a>");
                }
            }

			sb.Append("</div>");

            return new HtmlString(sb.ToString());
		}

		
	}

	 public class UrlCreator
	{
		readonly IUrlHelper UrlHelper;

		readonly string ActionName;

		readonly int PageSize;

        public UrlCreator(IUrlHelper urlHelper, string actionName, int pageSize)
		{
			this.UrlHelper = urlHelper;
			this.ActionName = actionName;
			this.PageSize = pageSize;
        }

        public string GetUrl(int index)
        {
            string url = UrlHelper.Action(ActionName, values: new
            {
                pageIndex = index,
                pageSize = PageSize
            });

            return url;
        }
    }
}

 