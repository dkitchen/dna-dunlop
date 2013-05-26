using DnaDunlopBarcodeWeb.Models;
using DnaDunlopBarcodeWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DnaDunlopBarcodeWeb.Controllers
{
    public class DepartmentLogController : Controller
    {

        Rest.PartByGoodyearSerialNumberController _partByGoodyearSerialNumberApi = new Rest.PartByGoodyearSerialNumberController();

        public ActionResult Index(string department)
        {
            var viewModel = new DepartmentLogViewModel();
            viewModel.DepartmentName = department;
            var api = new Rest.DepartmentLogController(department);

            viewModel.DepartmentLogs = api.Get().ToList();    //materializing
            return View(viewModel);
        }

        public ActionResult Details(string department, long id)
        {
            var api = new Rest.DepartmentLogController(department);
            var log = api.Get(id);
            var viewModel = new DepartmentLogViewModel();
            viewModel.DepartmentName = department;
            viewModel.SelectedDepartmentLog = log;

            return View(viewModel);
        }

        // GET: /DepartmentLog/Create

        //public ActionResult Create()
        //{
        //    var viewModel = new DepartmentLogViewModel();
        //    return View(viewModel);
        //}

        //
        // POST: /DepartmentLog/Create

        [HttpPost]
        public ActionResult Create(string department, DepartmentLogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var api = new Rest.DepartmentLogController(department);
                    api.InitializePost();
                    api.Post(viewModel.SelectedDepartmentLog);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    viewModel.Message = ex.Message;
                }
            }

            return View(viewModel);
        }

        public ActionResult CreatePartGreenTireChange(string department)
        {
            var viewModel = new PartGreenTireChangeViewModel();
            viewModel.DepartmentName = department;
            viewModel.Message = null;
            var api = new Rest.GreenTireController(department);
            viewModel.GreenTireSelectList = new SelectList(api.GetGreenTires());
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CreatePartGreenTireChange(string department, PartGreenTireChangeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //write a departmentLog record
                    //defaults
                    var log = new DepartmentLog();
                    log.EventName = "PartGreenTireNumberChange";
                    log.MachineName = "Web";    //TODO, make this a default value in a parent class

                    //DANGER!! Don't allow a new 15 digit part serial to get assigned here or it will create a 
                    //  brand new part row - will metadata catch it?
                    log.OperatorSerialNumber = viewModel.OperatorSerialNumber;
                    log.PartSerialNumber = viewModel.GoodyearSerialNumber;
                    log.DataName1 = "NewGreenTireNumber";
                    log.DataValue1 = viewModel.NewGreenTireNumber;
                    var api = new Rest.DepartmentLogController(department);
                    api.InitializePost();
                    api.Post(log);

                    //return RedirectToAction("Index");
                    viewModel.Message = "Green Tire Number Changed!";
                }
                catch (Exception ex)
                {
                    viewModel.Message = ex.Message;
                }
            }
            var greenTireApi = new Rest.GreenTireController(department);
            viewModel.GreenTireSelectList = new SelectList(greenTireApi.GetGreenTires());
            return View(viewModel);
        }

        public ActionResult CreatePartSerialNumberChange(string department)
        {
            var viewModel = new PartSerialNumberChangeViewModel();
            viewModel.DepartmentName = department;
            viewModel.Message = null;
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CreatePartSerialNumberChange(string department, PartSerialNumberChangeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //write a departmentLog record
                    //defaults
                    var log = new DepartmentLog();
                    log.EventName = "PartSerialNumberChange";
                    log.MachineName = "Web";    //TODO, make this a default value in a parent class

                    log.OperatorSerialNumber = viewModel.OperatorSerialNumber;
                    log.PartSerialNumber = viewModel.OldGoodyearSerialNumber;
                    log.DataName1 = "NewGoodyearSerialNumber";
                    log.DataValue1 = viewModel.NewGoodyearSerialNumber;
                    var api = new Rest.DepartmentLogController(department);
                    api.InitializePost();
                    api.Post(log);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    viewModel.Message = ex.Message;
                }
            }

            return View(viewModel);
        }
    }
}
