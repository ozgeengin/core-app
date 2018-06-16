using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication1.TagHelpers
{
    [HtmlTargetElement("my-taghelper")]
    public class MyTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<div>Özge Engin</div>");
            output.Attributes.Add("class", "taghelper");
        }
    }
}
