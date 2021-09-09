using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehousing.Web.Application.Queries;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Controllers
{
    public class ResourcesController : Controller
    {
        private WarehouseQueries _warehouseQueries;

        public ResourcesController(WarehouseQueries warehouseQueries)
        {
            _warehouseQueries = warehouseQueries;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetWarehouses([FromForm] DTParameterModel request)
        {
            string error = null;

            DTResponse res = null; 

            try
            {
                res = _warehouseQueries.GetWarehouses(request);
            }
            catch(Exception e)
            {
                error = e.Message;

                res = new DTResponse()
                {
                    Data = new List<WarehouseLotDTO>(),
                    Draw = request.Draw,
                    Error = error,
                    RecordsTotal = 0,
                    RecordsFiltered = 0,
                    AdditionalParameters = new Dictionary<string, object>()
                };
            }

            return Json(res);
        }

        [HttpGet]
        public IActionResult _AddWarehouseModalBody()
        {
            return PartialView();
        }
    }
}
