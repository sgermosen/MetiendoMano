using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AspNetMvcHelpersLib.Helpers
{
    public static class MvcHelperExtensions
    {

        /// <summary>
        /// Método extensión utilizado para crear un arreglo de objetos del tipo
        /// SelectListItem utilizado para construir instancias de objetos html 
        /// utilizando la sintaxis de razor que requieren un arreglo de objetos
        /// de este tipo tales como DropDownList, ListBox, etc.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto del cual generar el arreglo.</typeparam>
        /// <param name="items">Colección de objetos utilizados para generar el arreglo.</param>
        /// <param name="nameSelector">
        /// Función lambda utilizada para establecer el texto a mostrar por el objeto html
        /// cuando dicho item sea seleccionado.
        /// </param>
        /// <param name="valueSelector">
        /// Función lambda utilizada para establecer el valor interno que representará al
        /// item seleccionado del objeto html generado.
        /// </param>
        /// <param name="selected">
        /// Función lambda utilizada para establecer cual(es) objeto(s) está(n) seleccionado(s)
        /// por defecto en el objeto html generado utilizando el arreglo generado.
        /// </param>
        /// <returns>
        /// Un arreglo del tipo SelecListItem para ser usado para la generación de objetos html.
        /// </returns>
        public static IEnumerable<SelectListItem> ToSelectListItems<T>
            (
             this IEnumerable<T> items,
             Func<T, string> nameSelector,
             Func<T, string> valueSelector,
             Func<T, bool> selected
            )
        {
            return items.OrderBy(item => nameSelector(item))
                   .Select(item =>
                           new SelectListItem
                           {
                               Selected = selected(item),
                               Text = nameSelector(item),
                               Value = valueSelector(item)
                           });
        }

        /// <summary>
        /// Método extensión utilizado para crear un arreglo de objetos del tipo
        /// SelectListItem utilizado para construir instancias de objetos html 
        /// utilizando la sintaxis de razor que requieren un arreglo de objetos
        /// de este tipo tales como DropDownList, ListBox, etc.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto del cual generar el arreglo.</typeparam>
        /// <param name="items">Colección de objetos utilizados para generar el arreglo.</param>
        /// <param name="nameSelector">
        /// Función lambda utilizada para establecer el texto a mostrar por el objeto html
        /// cuando dicho item sea seleccionado.
        /// </param>
        /// <param name="valueSelector">
        /// Función lambda utilizada para establecer el valor interno que representará al
        /// item seleccionado del objeto html generado.
        /// </param>
        /// <returns>
        /// Un arreglo del tipo SelecListItem para ser usado para la generación de objetos html.
        /// </returns>
        public static IEnumerable<SelectListItem> ToSelectListItems<T>
            (
             this IEnumerable<T> items,
             Func<T, string> nameSelector,
             Func<T, string> valueSelector
            )
        {
            return items.OrderBy(item => nameSelector(item))
                   .Select(item =>
                           new SelectListItem
                           {
                               Text = nameSelector(item),
                               Value = valueSelector(item)
                           });
        }

        /// <summary>
        /// Método extensión utilizado para generar una colección de objetos SelectListItem
        /// utilizados para la construcción de dropdown lists.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de datos del objeto Enumerado.</typeparam>
        /// <typeparam name="TAttributeType">Tipo de instancia interna de una opción del enumerado.</typeparam>
        /// <param name="enumObj">Instancia de objeto enumeración para invocar el método.</param>
        /// <param name="useIntegerValue">Valor que especifica si se desea usar el valor int interno del enumerado.</param>
        /// <returns>Colección de tipo SelectListItem con las opciones del enumerado.</returns>
        public static IEnumerable<SelectListItem> EnumToSelectList<TEnum, TAttributeType>(this TEnum enumObj, bool useIntegerValue)
            where TEnum : struct
        {
            Type type = typeof(TEnum);

            var fields = type.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public);

            var values = from field in fields
                         select new SelectListItem
                         {
                             Value = (useIntegerValue) ? field.GetRawConstantValue().ToString() : field.Name,
                             Text = field.GetCustomAttributes(typeof(TAttributeType), true).
                                        FirstOrDefault().ToString() ?? field.Name,
                             Selected =
                                 (useIntegerValue)
                                     ? (Convert.ToInt32(field.GetRawConstantValue()) &
                                        Convert.ToInt32(enumObj)) ==
                                       Convert.ToInt32(field.GetRawConstantValue())
                                     : enumObj.ToString().Contains(field.Name)
                         };


            return values;
        }

        /// <summary>
        /// Método extensión utilizado para generar una colección de objetos SelectListItem
        /// utilizados para la construcción de dropdown lists.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de datos del objeto Enumerado.</typeparam>
        /// <param name="enumObj">Instancia de objeto enumeración para invocar el método.</param>
        /// <param name="useIntegerValue">Valor que especifica si se desea usar el valor int interno del enumerado.</param>
        /// <returns>Colección de tipo SelectListItem con las opciones del enumerado.</returns>
        public static IEnumerable<SelectListItem> EnumToSelectList<TEnum>(this TEnum enumObj, bool useIntegerValue)
            where TEnum : struct
        {
            Type type = typeof(TEnum);

            var fields = type.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public);

            var values = from field in fields
                         select new SelectListItem
                         {
                             Value = (useIntegerValue) ? field.GetRawConstantValue().ToString() : field.Name,
                             Text = field.Name.Replace('_', ' '),
                             Selected =
                                 (useIntegerValue)
                                     ? Convert.ToInt32(enumObj) == Convert.ToInt32(field.GetRawConstantValue())
                                     : enumObj.ToString().Contains(field.Name)
                         };


            return values;
        }

        /// <summary>
        /// Metodo utilitario para convertir un objeto enumerado en una lista de tipo string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary<T>(this T obj)
            where T : struct
        {
            return Enum.GetValues(typeof(T))
               .Cast<T>()
               .ToDictionary(t => Convert.ToInt32(t), t => t.ToString().Replace('_', ' '));
        }

        /// <summary>
        /// Html LabelFor helper diseñado para añadir una clase custom
        /// utilizada para los mensajes de error de bootstrap
        /// </summary>
        /// <typeparam name="TModel">Modelo del cual generar</typeparam>
        /// <typeparam name="TValue">Valor del modelo</typeparam>
        /// <param name="html">objeto HtmlHelper referenciado</param>
        /// <param name="expression">Expresion Lambda pra conocer el campo seleccionado</param>
        /// <param name="htmlAttributes">Atributos html a agregar al tag</param>
        /// <returns></returns>
        public static MvcHtmlString CLabelFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "control-label");

            return html.LabelFor(expression, attributes);
        }

        public static MvcHtmlString CLabel(
            this HtmlHelper html,
            string expression,
            string labelText = null,
            object htmlAttributes = null) 
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "control-label");

            return html.Label(expression, labelText, attributes);
        }

        public static MvcHtmlString CTextBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextBoxFor(expression, attributes);
        }

        public static MvcHtmlString CTextAreaFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString CPasswordFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.PasswordFor(expression, attributes);
        }

        public static MvcHtmlString CValidationMessageFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(null, out attributes, "help-block");

            return html.ValidationMessageFor(expression, null, attributes);
        }

        public static MvcHtmlString CValidationMessage(
            this HtmlHelper html,
            string modelName,
            string validationMessage = null,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "help-block");

            return html.ValidationMessage(modelName, validationMessage, attributes);
        }

        public static MvcHtmlString CTextBox(
            this HtmlHelper html,
            string name,
            object value = null,
           object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextBox(name, value, attributes);
        }

        public static MvcHtmlString CTextArea(
            this HtmlHelper html,
            string name,
           object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextArea(name, attributes);
        }

        public static MvcHtmlString CPassword(
            this HtmlHelper html,
            string name,
            object value = null,
            IDictionary<string, object> htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.Password(name, value, attributes);
        }

        public static SelectList ToSelectList<EnumType>(this EnumType enumObject)
            where EnumType : struct, IComparable, IFormattable, IConvertible
        {
            var values = from EnumType e in Enum.GetValues(typeof(EnumType))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObject);
        }

        public static MvcHtmlString CDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.DropDownListFor(expression, selectList, (optionLabel == null) ? string.Empty : optionLabel, attributes);
        }

        public static MvcHtmlString CListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.ListBoxFor(expression, selectList, attributes);
        }

        public static MvcHtmlString CDropDownList(
            this HtmlHelper html,
            string name,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.DropDownList(name, selectList, (optionLabel == null) ? string.Empty : optionLabel, attributes);
        }

        private static void addCustomClass(
            object htmlAttributes,
            out IDictionary<string, object> attributes,
            string Customclass)
        {
            attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            object cssClass;
            if (attributes.TryGetValue("class", out cssClass) == false)
            {
                cssClass = "";
            }
            attributes["class"] = cssClass + " " + Customclass;
        }

        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper, string imageClass, string action, object htmlAttributes)
        {
            TagBuilder builder;
            UrlHelper urlHelper;
            urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            builder = new TagBuilder("a");
            builder.InnerHtml = string.Format("<i class=\"glyphicon {0}\">", imageClass);
            builder.Attributes["href"] = urlHelper.Action(action);
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageClass, string actionName, string title = "", object routeValues = null, object htmlAttributes = null)
        {
            TagBuilder builder;
            UrlHelper urlHelper;
            htmlAttributes = (htmlAttributes == null) ? new Dictionary<string, object>() : htmlAttributes;
            var attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            builder = new TagBuilder("a");
            builder.InnerHtml = string.Format("<i class=\"glyphicon glyphicon-{0} \"></i>", imageClass, title);
            builder.Attributes["href"] = urlHelper.Action(actionName, routeValues);
            attributes.Add("title", title);
            attributes.Add("data-toggle", "tooltip");
            builder.MergeAttributes(new RouteValueDictionary(attributes));

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageClass, string actionName, string controllerName, string title = "", object routeValues = null, object htmlAttributes = null)
        {
            TagBuilder builder;
            UrlHelper urlHelper;
            htmlAttributes = (htmlAttributes == null) ? new Dictionary<string, object>() : htmlAttributes;
            var attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            builder = new TagBuilder("a");
            builder.InnerHtml = string.Format("<i class=\"glyphicon glyphicon-{0} \"></i>", imageClass, title);
            builder.Attributes["href"] = urlHelper.Action(actionName, controllerName, routeValues);
            attributes.Add("title", title);
            attributes.Add("data-toggle", "tooltip");

            builder.MergeAttributes(new RouteValueDictionary(attributes));

            return MvcHtmlString.Create(builder.ToString());
        }
        public delegate MvcHtmlString ErrorFormatter(object s);
        public static MvcHtmlString CValidationSummary(this HtmlHelper html, bool excludeFieldErrors = false, string message = null, object htmlAttributes = null, ErrorFormatter Formatter = null)
        {
            var errors = new List<ModelError>();
            var modelMetadata = html.ViewData.ModelMetadata;
            var propertyNames = modelMetadata.Properties.Select(p => p.PropertyName).ToList();
            var attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (excludeFieldErrors)
            {
                var formModelState = html.ViewData.ModelState
                    .Where(m => !propertyNames.Contains(m.Key)).SelectMany(m => m.Value.Errors).ToList();
                if (formModelState != null)
                {
                    errors = formModelState;
                }
            }
            else
            {
                errors = html.ViewData.ModelState.SelectMany(c => c.Value.Errors).ToList();
            }

            if (!errors.Any())
            {
                return null;
            }
            else
            {
                if (Formatter == null)
                {
                    StringBuilder builder = new StringBuilder();
                    if (message != null)
                    {
                        builder.Append("<span>");
                        builder.Append(html.Encode(message));
                        builder.AppendLine("</span>");
                    }
                    builder.AppendLine("<ul>");

                    foreach (var error in errors)
                    {
                        builder.Append("<li>");
                        builder.Append(html.Encode(error.ErrorMessage));
                        builder.AppendLine("</li>");
                    }

                    builder.Append("</ul>");

                    return MvcHtmlString.Create(builder.ToString());
                }
                else
                {
                    return Formatter(errors);
                }

            }

        }
        public static MvcHtmlString CValidationSummary(this HtmlHelper html, bool excludeFieldErrors, ErrorFormatter Formatter)
        {
            return CValidationSummary(html, excludeFieldErrors, null, null, Formatter);
        }

        public static MvcHtmlString CValidationSummary(this HtmlHelper html, ErrorFormatter Formatter)
        {
            return CValidationSummary(html, excludeFieldErrors: false, message: null, htmlAttributes: null, Formatter: Formatter);
        }

        public static string Encode(this string str)
        {
            return System.Net.WebUtility.HtmlEncode(str);
        }

        public static string Decode(this string str)
        {
            return System.Net.WebUtility.HtmlDecode(str);
        }
        public static MvcHtmlString SummaryFormatter(object e)
        {
            var errors = e as IEnumerable<ModelError>;
            StringBuilder builder = new StringBuilder();
            builder.Append("<div class=\"alert alert-dismissable alert-danger\">");
            builder.Append("<button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>");
            builder.Append("<ul>");
            foreach (var error in errors)
            {
                builder.Append("<li>");
                builder.Append(error.ErrorMessage.Encode());
                builder.AppendLine("</li>");
            }
            builder.Append("</ul>");
            builder.Append("</div>");

            return MvcHtmlString.Create(builder.ToString());
        }
    }

    public class AuthActionFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var route = filterContext.RouteData;
            var aDesc = filterContext.ActionDescriptor;
            var allowAnonymous = aDesc.GetCustomAttribute<AllowAnonymousAttribute>();

            if (allowAnonymous != null) return;

            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");

            if (!HtmlLinksHelper._perms(controller, action))
            {
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary
                 {
                     { "controller", "Home" },
                     { "action", "ErrorUnauthorized" }
                 });
            }
            else
            {
                return;
            }
        }
    }

    public static class HtmlLinksHelper
    {
        public delegate bool AuthChecker(params object[] prms);
        public delegate bool PermissionsChecker(string controller, string action);
        private static AuthChecker _del;
        public static PermissionsChecker _perms;
        public static AuthActionFilter Filter
        {
            get { return new AuthActionFilter(); }
        }
        public static void SetDelegate(AuthChecker del)
        {
            _del = del;
        }

        public static void SetDelegate(PermissionsChecker perms)
        {
            _perms = perms;
        }

        public static MvcHtmlString SActionLinkAuthorized(this HtmlHelper html, params object[] parameters)
        {
            bool authorized = _perms(parameters[2].ToString(), parameters[1].ToString());
            var controller = parameters[2].ToString();
            var action = parameters[1].ToString();
            var linkText = parameters[0].ToString();
            var routeValues = parameters.ElementAtOrDefault(3);
            var htmlAttrs = parameters.ElementAtOrDefault(4);
            var generateDisabled = (!authorized && parameters[parameters.Length - 1].GetType() == typeof(Boolean)) ? 
                (bool)parameters[parameters.Length - 1] : false;

            if (!generateDisabled && !authorized)
            {
                return MvcHtmlString.Empty;
            }
            else if (authorized)
            {
                return html.ActionLink(linkText, action, controller, routeValues, htmlAttrs);
            }
            else
            {
                var a = string.Format("<a href=\"javascript:void(0);\" >{0}</a>", linkText);

                return MvcHtmlString.Create(a);
            }
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), showActionLinkAsDisabled);
		}

		public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), showActionLinkAsDisabled);
		}

		public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), showActionLinkAsDisabled);
		}

		public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, routeValues, new RouteValueDictionary(), showActionLinkAsDisabled);
		}

		public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
		}

		public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, routeValues, htmlAttributes, showActionLinkAsDisabled);
		}

		public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
		{
			return htmlHelper.ActionLinkAuthorized(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
		}

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled)
        {
            if (_perms(controllerName, actionName))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
            {
                if (showActionLinkAsDisabled)
                {
                    TagBuilder tagBuilder = new TagBuilder("a");
                    tagBuilder.MergeAttributes(htmlAttributes);
                    tagBuilder.InnerHtml = linkText;
                    return MvcHtmlString.Create(tagBuilder.ToString());
                }
                else
                {
                    return MvcHtmlString.Empty;
                }
            }
        }

        //Authorization Links, Default implementations
        public static bool ActionAuthorized(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            ControllerBase controllerBase = string.IsNullOrEmpty(controllerName) ?
                htmlHelper.ViewContext.Controller : htmlHelper.GetControllerByName(controllerName);
            ControllerContext controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            if (actionDescriptor == null)
                return false;

            FilterInfo filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

            AuthorizationContext authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (IAuthorizationFilter authorizationFilter in filters.AuthorizationFilters)
            {
                authorizationFilter.OnAuthorization(authorizationContext);
                if (authorizationContext.Result != null)
                    return false;
            }
            return true;
        }

        public static ControllerBase GetControllerByName(this HtmlHelper htmlHelper, string controllerName)
        {
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = factory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);
            if (controller == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "The IControllerFactory '{0}' did not return a controller for the name '{1}'. Controller not Found.",
                        factory.GetType(), controllerName));
            }
            return (ControllerBase)controller;
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, null, routeValues, new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, null, routeValues, htmlAttributes, showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuth(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled)
        {
            if (htmlHelper.ActionAuthorized(actionName, controllerName))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
            {
                if (showActionLinkAsDisabled)
                {
                    TagBuilder tagBuilder = new TagBuilder("span");
                    tagBuilder.InnerHtml = linkText;
                    return MvcHtmlString.Create(tagBuilder.ToString());
                }
                else
                {
                    return MvcHtmlString.Empty;
                }
            }
        }

    }
}
