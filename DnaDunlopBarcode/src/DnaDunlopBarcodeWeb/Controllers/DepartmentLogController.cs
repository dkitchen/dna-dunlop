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
            viewModel.DepartmentLogs = _departmentLogApi.Get();
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
                    //defaults for this kind of event
                    viewModel.DepartmentLog.EventName = "PartGreenTireNumberChange";
                    viewModel.DepartmentLog.MachineName = "Web";

                    //DANGER!! Don't allow a new 15 digit part serial to get assigned here or it will create a 
                    //  brand new part row
                    viewModel.DepartmentLog.PartSerialNumber = viewModel.GoodyearSerialNumber;                    
                    viewModel.DepartmentLog.DataName1 = "NewGreenTireNumber";
                    viewModel.DepartmentLog.DataValue1 = viewModel.NewGreenTireNumber;

                    _departmentLogApi.InitializePost();
                    _departmentLogApi.Post(viewModel.DepartmentLog);

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
    }
}
