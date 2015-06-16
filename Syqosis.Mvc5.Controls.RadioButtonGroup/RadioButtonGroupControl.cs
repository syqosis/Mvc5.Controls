namespace Syqosis.Mvc5.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;

	public static class RadioButtonGroup
	{
		private static MvcHtmlString RadioButtonGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<SelectListItem> selectListItems,
			IDictionary<string, object> htmlAttributes = null)
		{
			var container = new TagBuilder("div");
			container.AddCssClass("radio-button-group");
			if (htmlAttributes != null)
				foreach (var htmlAttribute in htmlAttributes)
					if (htmlAttribute.Key == "class")
						container.AddCssClass(htmlAttribute.Value.ToString());
					else
						container.MergeAttribute(htmlAttribute.Key, htmlAttribute.Value.ToString());

			foreach (var item in selectListItems)
			{
				var radio = new TagBuilder("input");
				radio.MergeAttribute("type", "radio");
				radio.MergeAttribute("id", htmlHelper.IdFor(expression).ToHtmlString());
				radio.MergeAttribute("name", htmlHelper.NameFor(expression).ToHtmlString());
				radio.MergeAttribute("value", item.Value);

				var value = expression.Value(htmlHelper.ViewData);
				if (value.ToString() == item.Value)
					radio.MergeAttribute("checked", "checked");

				var label = new TagBuilder("label");
				label.InnerHtml = item.Text;

				container.InnerHtml += string.Concat(radio, label);
			}
			return MvcHtmlString.Create(container.ToString());
		}

		public static MvcHtmlString RadioButtonGroupFor<TModel, TProperty, TKey>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<TKey> items,
			Func<TKey, TProperty> keySelector,
			Func<TKey, string> valueSelector,
			object htmlAttributes,
			TProperty value = default(TProperty))
		{
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
			return htmlHelper.RadioButtonGroupFor(expression, items.ToSelectList(keySelector, valueSelector, value), attributes);
		}

		public static MvcHtmlString RadioButtonGroupFor<TModel, TProperty, TKey>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<TKey> items,
			Func<TKey, TProperty> keySelector,
			Func<TKey, string> valueSelector,
			IDictionary<string, object> htmlAttributes = null,
			TProperty value = default(TProperty))
		{
			return htmlHelper.RadioButtonGroupFor(expression, items.ToSelectList(keySelector, valueSelector, value), htmlAttributes);
		}

		public static MvcHtmlString RadioButtonGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			Type enumerableType,
			object htmlAttributes,
			Enum value = null)
		{
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
			return htmlHelper.RadioButtonGroupFor(expression, Extensions.EnumToSelectList(enumerableType, value), attributes);
		}

		public static MvcHtmlString RadioButtonGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			Type enumerableType,
			IDictionary<string, object> htmlAttributes = null,
			Enum value = null)
		{
			return htmlHelper.RadioButtonGroupFor(expression, Extensions.EnumToSelectList(enumerableType, value), htmlAttributes);
		}
	}
}