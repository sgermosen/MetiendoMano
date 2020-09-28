using SGC_MVC.Helpers;
using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Data.Entity;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using WebMatrix.WebData;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Data.Common;
using System.Resources;
using SGC_MVC.Properties;

namespace SGC_MVC.CustomCode
{
    public class Helper
    {
        public static string getDataJson(HttpRequestBase Request, int? companyID, SASContext db)
        {
            EISDataTables TableParams = new EISDataTables();
            TableParams.BindModel(Request);

            QueryBuilder query = new QueryBuilder(TableParams);
            query.FilterPagingSortingSearch(companyID);

            ButtonTemplates buttonTemplates = new ButtonTemplates();
            buttonTemplates.fillTemplates(TableParams.buttonsOptions);

            jsonBuilder json = new jsonBuilder(TableParams);

            return json.getDataJson(db, query, buttonTemplates);
        }

        public static int? GetCompanyID(SASContext db)
        {
            return db.Users.Find(WebSecurity.CurrentUserId).companyID;
        }

        /// <summary>
        /// Metodo utilizado para almacenar los nombres de los controladores
        /// y sus acciones asociadas en la base de datos.
        /// </summary>
        public static void InsertControllerAndActions()
        {
            ControllerActionProvider cp = new ControllerActionProvider();
            bool insertNew = false;
            using (SASContext db = new SASContext())
            {
                foreach (string controllerName in cp.GetControllerNames())
                {
                    SGC_MVC.Models.Controller c = db.Controllers.FirstOrDefault(
                                                        t => t.name == controllerName);

                    if (c == null)
                    {
                        insertNew = true;
                        c = new SGC_MVC.Models.Controller();
                        c.name = controllerName;
                    }

                    List<SGC_MVC.Models.Action> actions = new List<SGC_MVC.Models.Action>();


                    string[] lstActions = cp.GetActionNames(controllerName).Distinct().ToArray();
                    foreach (string action in lstActions)
                    {
                        SGC_MVC.Models.Action a = new SGC_MVC.Models.Action();
                        a.name = action;
                        a.isView = true;
                        actions.Add(a);
                    }
                    if (insertNew)
                    {
                        c.Actions = actions;
                    }
                    else
                    {
                        foreach (SGC_MVC.Models.Action a in actions)
                        {
                            if (!c.Actions.Contains(a, new Compare<SGC_MVC.Models.Action>((r, l) => r.name == l.name)))
                            {
                                c.Actions.Add(a);
                            }
                        }
                    }
                    if (db.Controllers.FirstOrDefault(con => con.name == controllerName) == null)
                    {
                        db.Controllers.Add(c);
                    }

                    if (insertNew)
                    {
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(c).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                    insertNew = false;
                }
            }
        }

        public static string getFileNameWithOutFormat(string url)
        {
            if (string.IsNullOrEmpty(url))
                return "N/A";
            string[] fileName = Path.GetFileName(url).Split(new string[] { "$$" }, StringSplitOptions.None);
            string labelText = Path.GetFileName(url);
            if (fileName.Length > 1)
            {
                labelText = fileName[1];
            }
            return labelText;
        }

        /// <summary>
        /// Helper function to get the filename of a server repository item without its file extension,
        /// and without the format headers used to the convention names.
        /// </summary>
        /// <param name="url">The url to get the Filename from filesystem</param>
        /// <param name="returnNull">Boolean condition to allow a null value to be returned</param>
        /// <returns></returns>
        public static string getFileNameWithOutFormat(string url, bool returnNull)
        {
            if (string.IsNullOrEmpty(url))
                return returnNull ? null : "N/A";
            string[] fileName = Path.GetFileName(url).Split(new string[] { "$$" }, StringSplitOptions.None);
            string labelText = Path.GetFileName(url);
            if (fileName.Length > 1)
            {
                labelText = fileName[1];
            }
            return labelText;
        }

        public static List<SelectListItem> GetRulesSelect(SASContext db)
        {
            List<SelectListItem> rls = new List<SelectListItem>();
            int? cID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var company = db.Companies.Include(c => c.Rules).FirstOrDefault(c => c.ID == cID);
            int[] rules = company.Rules.Select(rl => rl.ID).ToArray();
            db.Rules.Where(r => r.status == true && rules.Contains(r.ID)).ToList()
                .ForEach(item => rls.Add(
                    new SelectListItem
                    {
                        Value = item.ID.ToString(),
                        Text = item.code + " - " + item.name
                    })
                );

            return rls;
        }

        public static List<SelectListItem> GetRulesSelect(SASContext db, int[] SelectedVals)
        {
            List<SelectListItem> rls = new List<SelectListItem>();
            int? cID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var company = db.Companies.Include(c => c.Rules).FirstOrDefault(c => c.ID == cID);
            int[] rules = company.Rules.Select(rl => rl.ID).ToArray();
            db.Rules.Where(r => r.status == true && rules.Contains(r.ID)).ToList()
                .ForEach(item => rls.Add(
                    new SelectListItem
                    {
                        Value = item.ID.ToString(),
                        Text = item.code + " - " + item.name,
                        Selected = SelectedVals.Contains(item.ID)
                    })
                );

            return rls;
        }

        public static void updateObjectFields(object obj)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {

                if (descriptor.PropertyType.Name == "String"
                    && descriptor.GetValue(obj) == null
                    )
                {
                    PropertyInfo prop = obj.GetType()
                        .GetProperty(descriptor.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (prop.CanWrite)
                    {
                        prop.SetValue(obj, "", null);
                    }
                }
            }
        }

        public static string GetCompanyImage(SASContext db)
        {
            int? companyId = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var company = db.Companies.Find(companyId);
            return company.logo;
        }

        public static void InsertDefaultData()
        {
            using (SASContext db = new SASContext())
            {
                int create_user = db.Users
                    .FirstOrDefault(u => u.username == "mail@eisdr.com").ID;

                Status s = new Status();
                s.name = "Creacion";
                s.description = "Estatus de creacion";
                s.createUser = create_user;

                Department d = new Department();
                d.name = "Recursos Humanos";
                d.description = "Departamento de recursos humanos";
                d.status = true;
                d.createUser = create_user;

                Company c = new Company();
                c.name = "EIS";
                c.description = "Excellent Integrity Solutions";
                c.companyText = ":D";
                c.status = true;
                c.email = "mail@eisdr.com";
                c.logo = "(-_-)";

                if (db.Status.Count() == 0) { db.Status.Add(s); }
                if (db.Departments.Count() == 0) { db.Departments.Add(d); }
                if (db.Companies.Count() == 0) { db.Companies.Add(c); }

                var actions = db.Actions.Include(con => con.Controller);
                foreach (SGC_MVC.Models.Action a in actions)
                {
                    SGC_MVC.Models.View v = new View();
                    v.description = string.Format("{0}-{1}", a.name, a.Controller.name);
                    v.name = string.Format
                        (
                         "{0}-{1}",
                         (string.IsNullOrEmpty(a.displayName)) ? a.name : a.displayName,
                         (string.IsNullOrEmpty(a.Controller.displayName)) ? a.Controller.name : a.Controller.displayName
                        );
                    v.actionID = a.ID;

                    var views = db.Views.ToList();
                    if (!views.Contains(v, new Compare<SGC_MVC.Models.View>((r, l) => r.description == l.description)))
                    {
                        db.Views.Add(v);
                    }

                }

                db.SaveChanges();
            }
        }

        public static string getAllProperties(Type myclass, string separator)
        {
            new List<int>();
            string fields = "";
            PropertyInfo[] propertyInfos = myclass.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (PropertyInfo property in propertyInfos)
            {
                fields += property.Name + separator;
            }
            fields += "**";
            fields = fields.Replace(separator + "**", "");
            return fields;
        }

        public enum StatusTypes
        {
            None,
            Creacion = 1,
            Modificacion = 2,
            Aprobacion = 3
        }

        public enum EntityTypes
        {
            None,
            Cliente = 1,
            Proveedor = 2
        }

        public enum DocumentTypes
        {
            None,
            BaseLegal = 1,
            Mision = 2,
            Vision = 3,
            Norma = 4,
            Valores = 5,
            Politica = 6,
            Politicas = 7,
            Manual = 8,
            Objetivo = 9
        }

        public enum FieldTypes
        {
            None,
            Numerico = 1,
            EMail = 2,
            TextoSimple = 3,
            Comentarios = 4,
            Fecha = 5,
            ListaDesplegable = 6,
            ListaRadioButton = 7,
            ListaMultiSelect = 8
        }

        public static string t(string s)
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));

            try
            {
                s = String.IsNullOrEmpty(rm.GetString(s)) ? s : rm.GetString(s);
            }
            catch (Exception)
            {

            }

            return s;
        }

    }

    public static class HtmlHelpers
    {

        /// <summary>
        /// Metodo estático para construir un checkboxlist
        /// a partir de una lista de valores especificada.
        /// </summary>
        /// <param name="htmlHelper">El objeto HtmlHelper usado para ejecutar este metodo</param>
        /// <param name="listOfValues"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, IEnumerable<SelectListItem> listOfValues)
        {
            var sb = new StringBuilder();

            if (listOfValues != null)
            {

                foreach (var item in listOfValues)
                {
                    sb.Append("<label class='checkbox' >");

                    var label = htmlHelper
                        .Label(item.Value, HttpUtility.HtmlEncode(item.Text));
                    var checkbox = htmlHelper
                        .CheckBox(item.Text, new { id = item.Value, @checked = item.Selected }).ToHtmlString();

                    sb.AppendFormat("{0}{1}", checkbox, label);

                    sb.Append("</label>");
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static IEnumerable<SelectListItem> ToCheckBoxListSource<T>(this IEnumerable<T> checkedCollection,
                                          IEnumerable<T> allCollection)
            where T : SelectListItem
        {
            var result = new List<SelectListItem>();

            foreach (var allItem in allCollection)
            {
                var selectItem = new SelectListItem();
                selectItem.Text = allItem.ToString();
                selectItem.Value = allItem.Value.ToString();
                selectItem.Selected = (checkedCollection.Count(c => c.Value == allItem.Value) > 0);

                result.Add(selectItem);
            }

            return result;
        }

        /// <summary>
        /// Retorna el texto para un link utlizando el _tag por defecto de la clase.
        /// </summary>
        /// <param name="linkText">Texto para el link</param>
        /// <param name="actionName">Accción para el link</param>
        /// <param name="ControllerName">Controlador para el link</param>
        /// <returns></returns>
        public static MvcHtmlString AuthorizeActionLink
            (
             this System.Web.Mvc.HtmlHelper htmlHelper,
             MenuAuthorize menuAuthorize,
             string linkText, string actionName, string ControllerName, object htmlOptions = null
            )
        {

            string options = "";
            //NASA HERE
            if (htmlOptions != null)
                foreach (var item in htmlOptions.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    options += string.Format("{0}='{1}'", item.Name, item.GetValue(htmlOptions, null));
                }
            if (menuAuthorize.HasPermission(actionName, ControllerName))
            {
                //return htmlHelper.Action(actionName, ControllerName);
                //return htmlHelper.ActionLink(linkText, actionName);

                return MvcHtmlString.Create(
                    string.Format("{0}<a href=\"{6}/{2}/{3}\" {5} >{4}</a>{1}",
                        menuAuthorize.Tag,
                        menuAuthorize.TagClose,
                        ControllerName,
                        actionName,
                        linkText,
                        options,
                        System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
                        ));
            }
            return MvcHtmlString.Create(
                string.Format("{0}<a class=\"disable\" disabled=\"disabled\" {5} >{4}</a>{1}",
                    menuAuthorize.Tag,
                    menuAuthorize.TagClose,
                    ControllerName, actionName,
                    linkText,
                    options));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class GenericExtensions
    {
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
    }

    public class Compare<T> : IEqualityComparer<T>
    {
        Func<T, T, bool> compareFunction;
        Func<T, int> hashFunction;

        public Compare(Func<T, T, bool> compareFunction)
        {
            this.compareFunction = compareFunction;
        }

        public Compare(Func<T, T, bool> compareFunction, Func<T, int> hashFunction)
        {
            this.compareFunction = compareFunction;
            this.hashFunction = hashFunction;
        }

        public bool Equals(T x, T y)
        {
            return compareFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return hashFunction(obj);
        }
    }

    public static class ModelExtensions
    {

        public static string GetDisplayName<TModel, TProperty>
            (
             this TModel model,
             Expression<Func<TModel, TProperty>> expression
            )
        {
            return ModelMetadata.FromLambdaExpression<TModel, TProperty>(
                expression,
                new ViewDataDictionary<TModel>(model)
                ).DisplayName;
        }
    }

    public static class HtmlExtensions
    {
        public static MvcHtmlString LabelForR<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "")
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), labelText);
        }

        public static IHtmlString DisplayFormattedData(this HtmlHelper htmlHelper, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return MvcHtmlString.Empty;
            }

            string myString = data;
            myString = myString.Replace("\n", "<br>");

            return new HtmlString(myString);
        }

        private static MvcHtmlString LabelHelper(HtmlHelper html,
            ModelMetadata metadata, string htmlFieldName, string labelText)
        {

            if (string.IsNullOrEmpty(labelText))
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            bool isRequired = false;
            if (metadata.ContainerType != null)
                isRequired = metadata.IsRequired;

            TagBuilder tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            if (isRequired) tag.Attributes.Add("class", "label-required");
            if (isRequired)
            {
                var asteriskTag = new TagBuilder("span");
                asteriskTag.Attributes.Add("class", "required");
                asteriskTag.SetInnerText("*");
                tag.InnerHtml = labelText + asteriskTag.ToString(TagRenderMode.Normal);
            }
            else
            {
                tag.InnerHtml = labelText;
            }
            var output = tag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(output);
        }

    }


}