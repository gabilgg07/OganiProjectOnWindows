using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ogani.WebUI.AppCode.TagHelpers
{
    [HtmlTargetElement("image")]
    public class ImageInputTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-has-label")]
        public bool HasLabel { get; set; } = true;

        [HtmlAttributeName("asp-class")]
        public string CssClass { get; set; }

        [HtmlAttributeName("asp-path")]
        public string Path { get; set; }

        [HtmlAttributeName("asp-for")]
        public string Name { get; set; }

        [HtmlAttributeName("asp-caption")]
        public string Caption { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            if (!string.IsNullOrWhiteSpace(CssClass))
                output.Attributes.Add("class", CssClass);

            bool isReadonly = false;

            if (output.Attributes.ContainsName("readonly"))
                isReadonly = true;

            output.Content.SetHtmlContent((HasLabel ? $"<label class='control-label'>{Caption}</label>" : "") +
                $"<label class='image-input' for='{Name}' {(string.IsNullOrWhiteSpace(Path) ? "" : $"data-ImagePath='{Path}'")}>" +
                $"<input type='hidden' name='{Name}Path' value='{(System.IO.Path.GetFileName(Path) ?? "")}' />" +
                (isReadonly ? "" : $"<span>&times;</span>") +
                $"</label>" +
                (isReadonly ? "" : $"<input name='{Name}' id='{Name}' type='file' accept='image/x-png,image/gif,image/jpeg' />"));


        }
    }
}

