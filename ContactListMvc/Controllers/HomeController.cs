using ContactListMvc.Models.Repository;
using ContactListMvc.Models.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public JsonResult GetPersons(int? page
            , int? limit
            , string sortBy
            , string direction
            , string lastname
            , string phone
            , string city
            , string category
            , string isvalid)
        {
            try
            {
                var model = _repository.GetPersons(page, limit, sortBy, direction, lastname, phone, city, category, isvalid);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                MvcApplication.log.Error(ex, "Не удалось получить список контактов");
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities()
        {
            try
            {
                var model = _repository.GetCities();

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                MvcApplication.log.Error(ex, "Не удалось получить список городов");
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategories()
        {
            try
            {
                var model = _repository.GetCategories();

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                MvcApplication.log.Error(ex, "Не удалось получить спсок категорий");
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}