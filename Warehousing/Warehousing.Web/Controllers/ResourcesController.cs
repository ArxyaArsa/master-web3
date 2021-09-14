using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Warehousing.Web.Application.Queries;
using Warehousing.Web.Models;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Controllers
{
    public class ResourcesController : Controller
    {
        #region Attributes and Ctors
        private WarehouseQueries _warehouseQueries;
        private IWarehouseLotRepository _warehouseLotRepository;
        private readonly IMapper _mapper;

        public ResourcesController(WarehouseQueries warehouseQueries, IWarehouseLotRepository warehouseLotRepository, IMapper mapper)
        {
            _warehouseQueries = warehouseQueries;
            _warehouseLotRepository = warehouseLotRepository;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetWarehouses([FromForm] DTParameterModel request)
        {
            string error;
            DTResponse res; 

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
        public async Task<IActionResult> _AddEditWarehouseModalBody(int? id)
        {
            var model = new WarehouseLotDTO();

            if ((id ?? 0) != 0)
            {
                var wh = await _warehouseLotRepository.GetAsync(id.Value);
                model = _mapper.Map<WarehouseLotDTO>(wh);
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult AddWarehouse([Bind] WarehouseLotDTO wld)
        {
            try
            {
                var wh = new WarehouseLot(wld.Name, wld.Description, wld.Type, wld.Occupated, wld.WeightCapacity, wld.Manager_FirstName, wld.Manager_LastName, wld.Manager_Email, wld.Manager_Phone);

                _warehouseLotRepository.Add(wh);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditWarehouse([Bind] WarehouseLotDTO wld)
        {
            try
            {
                var wh = await _warehouseLotRepository.GetAsync(wld.Id);

                wh.UpdateName(wld.Name);
                wh.UpdateDescription(wld.Description);
                wh.UpdateType(wld.Type);
                wh.UpdateWeightCapacity(wld.WeightCapacity);
                wh.UpdateManagerInfo(wld.Manager_FirstName, wld.Manager_LastName, wld.Manager_Email, wld.Manager_Phone);

                _warehouseLotRepository.Update(wh);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }
    }
}
