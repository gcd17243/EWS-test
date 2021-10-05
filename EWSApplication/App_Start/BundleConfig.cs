using System.Web;
using System.Web.Optimization;

namespace EWSApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/slick").Include(
                      "~/Assets/lib/slick/slick.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Assets/css/site.css",
                      "~/Assets/css/font-awesome.min.css",
                      "~/Assets/css/all.min.css",
                      "~/Assets/css/line_awesome.css",
                      "~/Assets/css/line_awesome.min.css",
                      "~/Assets/css/responsive.css"
                      ));
            bundles.Add(new StyleBundle("~/Assets/animate").Include(
                        "~/Assets/css/animate.css"
                ));
            bundles.Add(new StyleBundle("~/Assets/style").Include(
                        "~/Assets/css/style.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Assets/js/site.js"));

            bundles.Add(new StyleBundle("~/Assets/codemirror").Include(
                        "~/Assets/lib/codemirror/codemirror.css",
                        "~/Assets/lib/codemirror/modestyle.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/codemirror").Include(
                            "~/Assets/lib/codemirror/codemirror.js",
                            "~/Assets/lib/codemirror/simple.js",
                            "~/Assets/lib/codemirror/placeholder.js",
                            "~/Assets/lib/codemirror/anhmode.js"
                            ));
            bundles.Add(new ScriptBundle("~/bundles/countdown").Include("~/Assets/lib/countdown/countdown.js")
                );
            bundles.Add(new StyleBundle("~/bundles/table").Include(
                             "~/Assets/css/table.css"          
                ));

           
        }
    }
}
