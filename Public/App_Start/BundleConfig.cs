using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Public.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts").Include(
                        "~/Scripts/jquery-3.6.0.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/modernizr-2.8.3.js"));

            bundles.Add(new StyleBundle("~/Content").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/validation.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/custom-styles.css"));
        }
    }
}