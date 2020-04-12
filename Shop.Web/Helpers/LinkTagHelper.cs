using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.Web.Helpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id")]
    public class LinkTagHelper: TagHelper
    {
        [HtmlAttributeName("northwind-id")]
        public int ImageId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("href", "/images/" + this.ImageId);
        }
    }
}
