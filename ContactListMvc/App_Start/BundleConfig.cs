using System.Web;
using System.Web.Optimization;

namespace ContactListMvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/nprogress.js",
                "~/Scripts/app/xlsuploader.js",                
                "~/Backload/Client/blueimp/fileupload/js/jquery.fileupload.js",
                "~/Scripts/gijgo/combined/gijgo.js",                
                "~/Scripts/bootstrap-multiselect/bootstrap-multiselect.js",
                "~/Scripts/app/multiselector.js",
                "~/Scripts/app/app.js"));
                
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/material-icons.css",
                "~/Content/gijgo/combined/gijgo.css", 
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/nprogress.css",
                "~/Content/bootstrap-multiselect/bootstrap-multiselect.css"));            
        }
    }
}
