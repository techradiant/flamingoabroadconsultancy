using log4net.Plugin;
using System.Runtime;
using System.Web;
using System.Web.Optimization;

namespace FAC.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/plugins/revolution/css/settings.css",//< !--REVOLUTION SETTINGS STYLES-- >
            //           "~/plugins/revolution/css/layers.css",//<!-- REVOLUTION LAYERS STYLES -->
            //           "~/plugins/revolution/css/navigation.css",//<!-- REVOLUTION NAVIGATION STYLES -->
            //          "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap-css").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/revolution-css").Include(                   
                    "~/plugins/revolution/css/settings.css",//< !--REVOLUTION SETTINGS STYLES-- >
                     "~/plugins/revolution/css/layers.css",//<!-- REVOLUTION LAYERS STYLES -->
                     "~/plugins/revolution/css/navigation.css"//<!-- REVOLUTION NAVIGATION STYLES -->
                    ));

            bundles.Add(new StyleBundle("~/Content/site-css").Include( 
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/popper").Include(
                        "~/Scripts/popper.js"));

            bundles.Add(new StyleBundle("~/bundles/revolution-slider").Include(
                    "~/plugins/revolution/js/jquery.themepunch.revolution.min.js", 
                    "~/plugins/revolution/js/jquery.themepunch.tools.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.actions.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.carousel.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.kenburn.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.layeranimation.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.migration.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.navigation.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.parallax.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.slideanims.min.js" 
                    ,"~/plugins/revolution/js/extensions/revolution.extension.video.min.js",
                    "~/Scripts/main-slider-script.js"));



        }
    }
}
