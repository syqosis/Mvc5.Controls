namespace Mvc5.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;

	public static class CheckBox
	{
		public static MvcHtmlString CheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
			return htmlHelper.CheckBoxFor(expression, attributes);
		}

		public static MvcHtmlString CheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes = null)
		{
			var container = new TagBuilder("div");
			container.AddCssClass("checkbox");
			if (htmlAttributes != null)
				foreach (var htmlAttribute in htmlAttributes)
					if (htmlAttribute.Key == "class")
						container.AddCssClass(htmlAttribute.Value.ToString());
					else
						container.MergeAttribute(htmlAttribute.Key, htmlAttribute.Value.ToString());

			var checkbox = new TagBuilder("input");
			checkbox.MergeAttribute("type", "checkbox");
			checkbox.MergeAttribute("id", htmlHelper.IdFor(expression).ToHtmlString());
			checkbox.MergeAttribute("name", htmlHelper.NameFor(expression).ToHtmlString());
			checkbox.MergeAttribute("value", "true");

			var value = expression.Value(htmlHelper.ViewData);
			if (value is bool && (bool) value)
				checkbox.MergeAttribute("checked", "checked");

			var label = new TagBuilder("label");
			label.MergeAttribute("for", htmlHelper.IdFor(expression).ToHtmlString());

			container.InnerHtml = string.Concat(checkbox, label);
			return MvcHtmlString.Create(container.ToString());
		}
	}
}