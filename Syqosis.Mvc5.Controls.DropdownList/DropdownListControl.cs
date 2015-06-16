namespace Syqosis.Mvc5.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;

	public static class DropDownList
	{
		public static MvcHtmlString DropDownListFor<TModel, TProperty, TKey>(this HtmlHelper<TModel> htmlHelper, 
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<TKey> items,
			Func<TKey, TProperty> keySelector, 
			Func<TKey, string> valueSelector, 
			object htmlAttributes,
			TProperty value = default(TProperty))
		{
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
			return htmlHelper.DropDownListFor(expression, items.ToSelectList(keySelector, valueSelector, value), attributes);
		}

		public static MvcHtmlString DropDownListFor<TModel, TProperty, TKey>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<TKey> items,
			Func<TKey, TProperty> keySelector,
			Func<TKey, string> valueSelector,
			IDictionary<string, object> htmlAttributes = null,
			TProperty value = default(TProperty))
		{
			return htmlHelper.DropDownListFor(expression, items.ToSelectList(keySelector, valueSelector, value), htmlAttributes);
		}

		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
			Expression<Func<TModel, TProperty>> expression, 
			Type enumerableType,
			object htmlAttributes = null,
			Enum value = null)
		{
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
			return htmlHelper.DropDownListFor(expression, Extensions.EnumToSelectList(enumerableType, value), attributes);
		}

		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
			Expression<Func<TModel, TProperty>> expression,
			Type enumerableType, 
			IDictionary<string, object> htmlAttributes = null,
			Enum value = null)
		{
			return htmlHelper.DropDownListFor(expression, Extensions.EnumToSelectList(enumerableType, value), htmlAttributes);
		}
	}
}