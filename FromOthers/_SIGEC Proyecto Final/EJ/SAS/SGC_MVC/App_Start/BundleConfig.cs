using System.Web;
using System.Web.Optimization;

namespace SGC_MVC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jhtmlarea").Include(
                        "~/Scripts/jHtmlArea-{version}.js",
                        "~/Scripts/jHtmlArea.ColorPickerMenu-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                       "~/Scripts/DataTables-1.9.4/media/js/*.js"));

            bundles.Add(new StyleBundle("~/Content/DataTables-1.9.4/media/css/css").Include(
                        "~/Content/DataTables-1.9.4/media/css/*.css"));

            bundles.Add(new StyleBundle("~/bundles/jhtmlarea/css").Include(
                        "~/Content/jHtmlArea/*.css"));

            bundles.Add(new StyleBundle("~/Content/chosen/css").Include(
                        "~/Content/chosen/chosen.css"));

            bundles.Add(new ScriptBundle("~/bundles/chosenjquery").Include(
                        "~/Scripts/chosen.jquery.min.js",
                        "~/Scripts/chosen.jquery.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/eis_framework.css"));
            bundles.Add(new StyleBundle("~/Content/jqte/css").Include("~/Content/jqte/jquery-te-1.4.0.css"));
            

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}