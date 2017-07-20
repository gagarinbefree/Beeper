using Backload.Context.DataProvider;
using Backload.Contracts.Context;
using Backload.Contracts.Eventing;
using Backload.Contracts.FileHandler;
using Backload.Helper;
using ContactListMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ContactListMvc.Controllers
{
    public class FileHandlerController : Controller
    {
        private ILoader _loader;

        public FileHandlerController(ILoader loader)
        {
            _loader = loader;
        }

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            try
            {
                IFileHandler handler = Backload.FileHandler.Create();                

                using (var provider = new BackloadDataProvider(this.Request))
                {
                    var name = provider.Files[0].FileName;
                    provider.Files[0].FileName = string.Format("{0}.{1}", Guid.NewGuid(), "xlsx");

                    handler.Init(provider);
                    IBackloadResult result = await handler.Execute();
                    
                    return ResultCreator.Create(result);
                }
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        [HttpGet]
        public ActionResult LoadToDb(string filename, string comment)
        {
            _loader.LoadToDb(filename, comment);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}