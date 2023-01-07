using System;
using System.Text.RegularExpressions;

namespace Ogani.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{
		public static string CleanPlainText(this string text, int? limit = null)
		{
			text = Regex.Replace(text,"<[^>]*>","");

			if (limit.HasValue && limit.Value > 5 && text.Length > limit.Value)
			{
				text = text.Substring(0, limit.Value-3) + "...";
			}
			return text;
		}
	}
}

