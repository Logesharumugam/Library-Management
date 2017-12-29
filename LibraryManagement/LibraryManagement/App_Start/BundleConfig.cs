using System.Collections.Generic;
using System.Web.Optimization;

namespace LibraryManagement
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var customBundleOrder = new CustomBundleOrderer();
            BundleTable.EnableOptimizations = !System.Diagnostics.Debugger.IsAttached;
            bundles.UseCdn = true;

            GetStyleBundles(bundles, customBundleOrder);
            GetScriptsBundle(bundles, customBundleOrder);
        }

        public static void GetStyleBundles(BundleCollection bundles, CustomBundleOrderer customBundleOrder)
        {
            bundles.Add(new LessBundle("~/styles/bootstrap") { Orderer = customBundleOrder }.Include(
               "~/Styles/Bootstrap/bootstrap.min.css"
         ));

        bundles.Add(new LessBundle("~/ejControls/themes/ej") { Orderer = customBundleOrder }.Include(
            "~/ejControls/themes/ej.widgets.core.bootstrap.css",
            "~/ejControls/themes/bootstrap-theme/ej.theme.css"
         ));

        }

        public static void GetScriptsBundle(BundleCollection bundles, CustomBundleOrderer customBundleOrder)
        {
            bundles.UseCdn = true;
            bundles.Add(new ScriptBundle("~/scripts/jqueries") { Orderer = customBundleOrder }.Include(
            "~/Scripts/jQuery/jquery-2.1.4.min.js",
            "~/Scripts/jQuery/jquery.easing.1.3.min.js",
            "~/Scripts/jQuery/jquery.globalize.min.js",
            "~/Scripts/jQuery/jsrender.min.js",
            "~/Scripts/jQuery/jquery-ui.js",
            "~/Scripts/jQuery/jquery.validate.min.js",
            "~/Scripts/jQuery/jquery.validate.unobtrusive.min.js",
            "~/Scripts/jQuery/modernizr-2.6.2.js",
            "~/Scripts/Bootstrap/bootstrap.min.js",
            "~/Scripts/LogOut.js"
              ));

            bundles.Add(new ScriptBundle("~/scripts/validation") { Orderer = customBundleOrder }.Include(
            "~/Scripts/jQuery/jquery.validate.min.js",
            "~/Scripts/jQuery/jquery.validate.unobtrusive.min.js",
            "~/Scripts/jQuery/modernizr-2.6.2.js"
             ));

            bundles.Add(new ScriptBundle("~/scripts/ej") { Orderer = customBundleOrder }.Include(
                 "~/ejControls/scripts/ej.widget.all.js"
             ));
        }

        public class CustomBundleOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
    }
}