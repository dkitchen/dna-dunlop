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

        //
        // GET: /DepartmentLog/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DepartmentLog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DepartmentLog/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DepartmentLog/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreatePartGreenTireChange()
        {
            var viewModel = new PartGreenTireChangeViewModel();
            viewModel.GreenTireSelectList = new SelectList(_greenTireApi.GetGreenTires(), "GreenTireNumber", "GreenTireNumber");
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
                    viewModel.DepartmentLog.CreatedOn = DateTime.Now;
                    //viewModel.DepartmentLog.DataName1 = "OriginalGreenTireNumber";
                    //do this in sproc???
                    //viewModel.DepartmentLog.DataValue1 = Get current part and get current gtn
                    viewModel.DepartmentLog.DataName1 = "NewGreenTireNumber";
                    viewModel.DepartmentLog.DataValue1 = viewModel.NewGreenTireNumber;


                    _departmentLogApi.Post(viewModel.DepartmentLog);

                    return View();  // RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    viewModel.Message = ex.Message;
                }
            }

            viewModel.GreenTireSelectList = new SelectList(_greenTireApi.GetGreenTires(), "GreenTireNumber", "GreenTireNumber");
            return View(viewModel);
        }
    }
}
