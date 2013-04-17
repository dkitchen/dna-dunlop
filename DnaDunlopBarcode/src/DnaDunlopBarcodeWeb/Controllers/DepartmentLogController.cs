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

        public ActionResult Index()
        {
            var api = new DnaDunlopBarcodeWeb.Controllers.Rest.DepartmentLogController();
            var model = api.Get();

            //bind to viewModel (TODO - viewModel Builder for this?)
            var viewModel = new List<DepartmentLogViewModel>();
            foreach (var modelItem in model)
            {
                var viewModelItem = new DepartmentLogViewModel()
                {
                    Id = (long)modelItem.DEPARTMENT_LOG_ID,
                    EventName = modelItem.EVENT_NAME,
                    MachineName = modelItem.MACHINE_NAME,
                    OperatorSerialNumber = modelItem.OPERATOR_SERIAL_NUMBER,
                    PartSerialNumber = modelItem.PART_SERIAL_NUMBER,
                    DataName1 = modelItem.DATA_NAME_1,
                    DataValue1 = modelItem.DATA_VALUE_1,
                    DataName2 = modelItem.DATA_NAME_2,
                    DataValue2 = modelItem.DATA_VALUE_2,
                    DataName3 = modelItem.DATA_NAME_3,
                    DataValue3 = modelItem.DATA_VALUE_3,
                    DataName4 = modelItem.DATA_NAME_4,
                    DataValue4 = modelItem.DATA_VALUE_4,
                    DataName5 = modelItem.DATA_NAME_5,
                    DataValue5 = modelItem.DATA_VALUE_5
                };

                viewModel.Add(viewModelItem);
            }

            return View(viewModel);
        }

        public ActionResult Details(long id)
        { 
            var api = new DnaDunlopBarcodeWeb.Controllers.Rest.DepartmentLogController();
            var model = api.Get(id);
            //bind db model to viewModel
            var viewModel = new DepartmentLogViewModel()
            {
                 Id = (long)model.DEPARTMENT_LOG_ID,
                 EventName = model.EVENT_NAME,
                 MachineName = model.MACHINE_NAME,
                 OperatorSerialNumber = model.OPERATOR_SERIAL_NUMBER,
                 PartSerialNumber = model.PART_SERIAL_NUMBER,
                 DataName1 = model.DATA_NAME_1,
                 DataValue1 = model.DATA_VALUE_1,
                 DataName2 = model.DATA_NAME_2,
                 DataValue2 = model.DATA_VALUE_2,
                 DataName3 = model.DATA_NAME_3,
                 DataValue3 = model.DATA_VALUE_3,
                 DataName4 = model.DATA_NAME_4,
                 DataValue4 = model.DATA_VALUE_4,
                 DataName5 = model.DATA_NAME_5,
                 DataValue5 = model.DATA_VALUE_5
            };

            return View(viewModel);
        }

        // GET: /DepartmentLog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DepartmentLog/Create

        [HttpPost]
        public ActionResult Create(DepartmentLogViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                //bind viewModel to db model
                var model = new Models.DEPARTMENT_LOG();
                model.CREATED = DateTime.Now;
                model.EVENT_NAME = viewModel.EventName;
                model.MACHINE_NAME = viewModel.MachineName;
                model.OPERATOR_SERIAL_NUMBER = viewModel.OperatorSerialNumber;
                model.PART_SERIAL_NUMBER = viewModel.PartSerialNumber;
                model.DATA_NAME_1 = viewModel.DataName1;
                model.DATA_VALUE_1 = viewModel.DataValue1;
                model.DATA_NAME_2 = viewModel.DataName2;
                model.DATA_VALUE_2 = viewModel.DataValue2;
                model.DATA_NAME_3 = viewModel.DataName3;
                model.DATA_VALUE_3 = viewModel.DataValue3;
                model.DATA_NAME_4 = viewModel.DataName4;
                model.DATA_VALUE_4 = viewModel.DataValue4;
                model.DATA_NAME_5 = viewModel.DataName5;
                model.DATA_VALUE_5 = viewModel.DataValue5;

                var api = new DnaDunlopBarcodeWeb.Controllers.Rest.DepartmentLogController();
                api.Post(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
