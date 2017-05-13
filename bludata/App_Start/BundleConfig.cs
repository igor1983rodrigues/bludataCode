using System.Web;
using System.Web.Optimization;

namespace Bludata
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                        "~/Scripts/jquery.inputmask/inputmask.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                        "~/Scripts/jquery.inputmask/inputmask.extensions.js",
                        "~/Scripts/jquery.inputmask/inputmask.date.extensions.js",
                        //and other extensions you want to include
                        "~/Scripts/jquery.inputmask/inputmask.numeric.extensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-messages.min.js",
                "~/Scripts/i18n/angular-locale_pt-br.js",
                "~/Scripts/angular-ui/ui-bootstrap.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/configs.js").Include(
                "~/Scripts/configurar/funcoes.js",
                "~/Scripts/view/Geral/config-angular.js",
                "~/Scripts/view/Principal/config-angular.js",
                "~/Scripts/view/Cliente/config-angular.js",
                "~/Scripts/view/Pessoa/config-angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/setup.js").Include(
                "~/Scripts/configurar/funcoes.js",
                "~/Scripts/view/Geral/config-angular.js",
                "~/Scripts/view/Cliente/config-angular.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/ui-bootstrap-csp.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css"));
        }
    }
}
