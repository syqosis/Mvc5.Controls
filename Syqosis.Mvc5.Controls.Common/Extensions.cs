namespace Syqosis.Mvc5.Controls
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Web.Mvc;

	public static class Extensions
	{
		public static readonly SelectListItem DefaultEmptySelectListItem = new SelectListItem {
			Text = "",
			Value = null
		};

		public static string ToDescription(this Enum value)
		{
			var attributes = (DescriptionAttribute[]) value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		public static IEnumerable<SelectListItem> ToSelectList(this Enum value)
		{
			return from Enum e in Enum.GetValues(value.GetType())
			       select new SelectListItem {
					   Selected = e.Equals(value),
				       Text = e.ToDescription(),
				       Value = e.ToString()
			       };
		}

		public static IEnumerable<SelectListItem> EnumToSelectList(Type enumerableType, Enum value = null)
		{
			return from Enum e in Enum.GetValues(enumerableType)
				   select new SelectListItem
				   {
					   Selected = e.Equals(value),
					   Text = e.ToDescription(),
					   Value = e.ToString()
				   };
		}

		public static IList<SelectListItem> ToSelectList<TModel, TProperty>(this IEnumerable<TModel> enumerable, Func<TModel, TProperty> key, Func<TModel, string> text)
		{
			return enumerable.ToSelectList(key, text, null);
		}

		public static IList<SelectListItem> ToSelectList<TModel, TProperty>(this IEnumerable<TModel> enumerable, Func<TModel, TProperty> key, Func<TModel, string> text, TProperty currentKey, bool includeEmptyListItem = true)
		{
			return enumerable.ToSelectList(key, text, currentKey, includeEmptyListItem ? DefaultEmptySelectListItem : null);
		}

		public static IList<SelectListItem> ToSelectList<TModel, TProperty>(this IEnumerable<TModel> enumerable, Func<TModel, TProperty> key, Func<TModel, string> text, TProperty currentKey, SelectListItem emptyListItem)
		{
			return enumerable.ToSelectList(key, text, new[] {
				currentKey
			}, emptyListItem);
		}

		public static IList<SelectListItem> ToSelectList<TModel, TProperty>(this IEnumerable<TModel> enumerable, Func<TModel, TProperty> key, Func<TModel, string> text, IEnumerable<TProperty> currentKeys, bool includeEmptyListItem = true)
		{
			return enumerable.ToSelectList(key, text, currentKeys, includeEmptyListItem ? DefaultEmptySelectListItem : null);
		}

		public static IList<SelectListItem> ToSelectList<TModel, TProperty>(this IEnumerable<TModel> enumerable, Func<TModel, TProperty> key, Func<TModel, string> text, IEnumerable<TProperty> currentKeys, SelectListItem emptyListItem)
		{
			var selectList = new List<SelectListItem>();
			if (enumerable != null)
				selectList = enumerable.Select(x => new SelectListItem {
					Value = key.Invoke(x).ToString(),
					Text = text.Invoke(x),
					Selected = (currentKeys != null && currentKeys.Contains(key.Invoke(x)))
				}).ToList();
			if (emptyListItem != null)
				selectList.Insert(0, emptyListItem);
			return selectList;
		}

		public static object Value<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression, ViewDataDictionary<TModel> viewData)
		{
			return ModelMetadata.FromLambdaExpression(expression, viewData).Model;
		}
	}
}