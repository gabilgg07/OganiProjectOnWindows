using System;
using System.Text.RegularExpressions;

namespace Ogani.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{
		public static bool IsEmail(this string value)
		{
			if (value == null)
			{
				return false;
			}
			return Regex.IsMatch(value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}
	}
}

