using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownList.DBOperator;
using DropDownList.Models;

namespace DropDownList.Controllers
{
    public class ProvinceController : Controller
    {
        private AddressHelper db = new AddressHelper();
        //public ProvinceController()
        //{
        //    db = new AddressHelper();
        //}
        // GET: Province
        public ActionResult Index()
        {
            //先从数据中将数据查询出来,再将返回的数据以ViewBag的形式传递到页面,在页面进行遍历输出
            List<Province> lstProvince = db.GetAllProvince();
            ViewBag.ListProvince = lstProvince;
            return View();
        }
        public JsonResult GetAllCityByProvinceID(int id)
        {
            List<City> lstCity = db.GetCityListByProvinceID(id);
            return Json(lstCity, JsonRequestBehavior.AllowGet);
        }
    }
}