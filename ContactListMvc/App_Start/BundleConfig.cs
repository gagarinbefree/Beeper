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
                "~/Backload/Client/blueimp/fileupload/js/jquery.fileupload.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-theme.css",
                "~/Content/site.css",
                "~/Content/nprogress.css"));            
        }
    }
}
