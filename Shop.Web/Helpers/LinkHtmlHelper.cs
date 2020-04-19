using System;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Shop.Web.Helpers
{
    public static class LinkHtmlHelper
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper htmlHelper,
            int imageId, string linkText)
        {
            TagBuilder a = new TagBuilder("a");
            a.InnerHtml.Append(linkText);
            a.Attributes.Add("href", "/images/" + imageId);

            var writer = new System.IO.StringWriter();
            a.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
