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

        Rest.DepartmentLogController _departmentLogApi = new Rest.DepartmentLogController();
        Rest.GreenTireController _greenTireApi = new Rest.GreenTireController();
        Rest.PartByGoodyearSerialNumberController _partByGoodyearSerialNumberApi = new Rest.PartByGoodyearSerialNumberController();

        public ActionResult Index()
        {
            var viewModel = new DepartmentLogViewModel();
            viewModel.DepartmentLogs = _departmentLogApi.Get().ToList();    //materializing
            return View(viewModel);
        }

        public ActionResult Details(long id)
        {
            var log = _departmentLogApi.Get(id);
            var viewModel = new DepartmentLogViewModel();
            viewModel.SelectedDepartmentLog = log;

            return View(viewModel);
        }

        // GET: /DepartmentLog/Create

        public ActionResult Create()
        {
            var viewModel = new DepartmentLogViewModel();
            return View(viewModel);
        }

        //
        // POST: /DepartmentLog/Create

        [HttpPost]
        public ActionResult Create(DepartmentLogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentLogApi.InitializePost();
                    _departmentLogApi.Post(viewModel.SelectedDepartmentLog);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    viewModel.Message = ex.Message;
                }
            }

            return View(viewModel);
        }

        public ActionResult CreatePartGreenTireChange()
        {
            var viewModel = new PartGreenTireChangeViewModel();
            viewModel.Message = null;
            viewModel.GreenTireSelectList = new SelectList(_greenTireApi.GetGreenTires());
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CreatePartGreenTireChange(PartGreenTireChangeViewModel viewModel)
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
                    
                    _departmentLogApi.InitializePost();
                    _departmentLogApi.Post(log);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    viewModel.Message = ex.Message;
                }
            }

            viewModel.GreenTireSelectList = new SelectList(_greenTireApi.GetGreenTires());
            return View(viewModel);
        }

        public ActionResult CreatePartSerialNumberChange()
        {
            var viewModel = new PartSerialNumberChangeViewModel();
            viewModel.Message = null;
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CreatePartSerialNumberChange(PartSerialNumberChangeViewModel viewModel)
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

                    _departmentLogApi.InitializePost();
                    _departmentLogApi.Post(log);

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
