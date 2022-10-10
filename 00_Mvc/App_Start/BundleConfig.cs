using System.Web;
using System.Web.Optimization;

namespace _00_Mvc
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //                            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery224").Include(
                                  "~/Scripts/vendor/jquery-2.2.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapmin").Include(
                "~/Scripts/vendor/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/easingmin").Include(
                "~/Scripts/easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/hoverIntent").Include(
               "~/Scripts/hoverIntent.js"));

            //bundles.Add(new ScriptBundle("~/bundles/superfish").Include(
            //  "~/Scripts/superfish.js"));

            bundles.Add(new ScriptBundle("~/bundles/superfishmin").Include(
              "~/Scripts/superfish.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajaxchimp").Include(
                                 "~/Scripts/jquery.ajaxchimp.min.js"));
                     
            bundles.Add(new ScriptBundle("~/bundles/jquerymagnific").Include(
                                  "~/Scripts/jquery.magnific-popup.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/owlcarousel").Include(
              "~/Scripts/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerysticky").Include(
                                     "~/Scripts/jquery.sticky.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerynice").Include(
                                  "~/Scripts/jquery.nice-select.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/parallaxmin").Include(
               "~/Scripts/parallax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/waypoints").Include(
                                "~/Scripts/waypoints.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycounterup").Include(
                                 "~/Scripts/jquery.counterup.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/mail").Include(
                                 "~/Scripts/mail-script.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include(
                                "~/Scripts/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/youtube").Include(
                              "~/Scripts/youtube.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //          "~/Scripts/jquery.validate*"));
            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));


            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(

            //         "~/Scripts/bootstrap.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                           "~/Content/linearicons.css",
                           "~/Content/font-awesome.min.css",
                          "~/Content/bootstrap.css",
                          "~/Content/magnific-popup.css",
                          "~/Content/nice-select.css",
                          "~/Content/animate.min.css",
                          "~/Content/owl.carousel.css",
                                  "~/Content/main.css"));

            
        }
    }
}
    
   