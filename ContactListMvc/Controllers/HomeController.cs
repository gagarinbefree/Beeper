using ContactListMvc.Models.Repository;
using ContactListMvc.Models.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactListMvc.Controllers
{
    public class HomeController : Controller
    {
        private IRepEf _repository;

        public HomeController(IRepEf repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }       
 
        public JsonResult Get(int? page, int? limit, string sortBy, string direction)
        {
            var records = _repository.GetPersons(page, limit, sortBy, direction);

            return this.Json(new { records }, JsonRequestBehavior.AllowGet);
        }
    }
}