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
        private ParcelQueries _parcelQueries;
        private ContractQueries _contractQueries;
        private ParcelTypeQueries _parcelTypeQueries;
        private IWarehouseLotRepository _warehouseLotRepository;
        private readonly IMapper _mapper;

        public ResourcesController(WarehouseQueries warehouseQueries, ParcelQueries parcelQueries, ContractQueries contractQueries, ParcelTypeQueries parcelTypeQueries, IWarehouseLotRepository warehouseLotRepository, IMapper mapper)
        {
            _warehouseQueries = warehouseQueries;
            _parcelQueries = parcelQueries;
            _contractQueries = contractQueries;
            _parcelTypeQueries = parcelTypeQueries;
            _warehouseLotRepository = warehouseLotRepository;
            _mapper = mapper;
        }
        #endregion

        #region Warehouses
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

        [HttpGet]
        public async Task<IActionResult> _DeleteWarehouseModalBody(int? id)
        {
            if ((id ?? 0) != 0)
            {
                var wh = await _warehouseLotRepository.GetAsync(id.Value);
                var model = _mapper.Map<WarehouseLotDTO>(wh);

                return PartialView(model);
            }
            else
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel("No such warehouse! Please go back and try again!"));
            }
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

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            try
            {
                var wh = await _warehouseLotRepository.GetAsync(id);

                _warehouseLotRepository.Remove(wh);

                return Ok();
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return BadRequest("An Error occured! Cannot delete this Warehouse! --> " + e.Message + " - " + (e.InnerException?.Message));
            }
        }
        #endregion

        #region Warehouse Parcels
        [HttpGet]
        public async Task<IActionResult> WarehouseDetails(int id)
        {
            try
            {
                var wh = await _warehouseLotRepository.GetAsync(id, includeParcels: true);
                var model = _mapper.Map<WarehouseLotDTO>(wh);

                return View(model);
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }

        [HttpPost]
        public IActionResult GetParcels([FromForm] DTParameterModel request, int warehouseId)
        {
            string error;
            DTResponse res;

            try
            {
                res = _parcelQueries.GetParcels(warehouseId, request);
            }
            catch (Exception e)
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
        public async Task<IActionResult> _AddEditParcelModalBody(int? id, int whId)
        {
            var model = new ParcelDTO();

            if ((id ?? 0) != 0)
            {
                var p = await _warehouseLotRepository.GetParcelAsync(id.Value);
                model = _mapper.Map<ParcelDTO>(p);
            }

            var wh = await _warehouseLotRepository.GetAsync(whId);
            var whm = _mapper.Map<WarehouseLotDTO>(wh);
            ViewBag.Warehouse = whm;
            model.WarehouseLotId = whId;

            ViewBag.Contracts = _contractQueries.GetContractsSimple().ToList();
            ViewBag.ParcelTypes = _parcelTypeQueries.GetParcelTypesSimple().ToList();

            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> _DeleteParcelModalBody(int? id, int whId)
        {
            if ((id ?? 0) != 0)
            {
                var p = await _warehouseLotRepository.GetParcelAsync(id.Value);
                var model = _mapper.Map<ParcelDTO>(p);

                var wh = await _warehouseLotRepository.GetAsync(whId);
                var whm = _mapper.Map<WarehouseLotDTO>(wh);
                ViewBag.Warehouse = whm;
                model.WarehouseLotId = whId;

                ViewBag.Contracts = _contractQueries.GetContractsSimple().ToList();
                ViewBag.ParcelTypes = _parcelTypeQueries.GetParcelTypesSimple().ToList();

                return PartialView(model);
            }
            else
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel("No such warehouse! Please go back and try again!"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddParcel([Bind] ParcelDTO p)
        {
            try
            {
                var pt = await _warehouseLotRepository.GetParcelTypeAsync(p.ParcelTypeId);
                var wh = await _warehouseLotRepository.GetAsync(p.WarehouseLotId, includeParcels: true);

                wh.AddParcel(p.ContractId, pt, p.Weight);

                _warehouseLotRepository.Update(wh);

                return RedirectToAction("WarehouseDetails", new { id = p.WarehouseLotId });
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditParcel([Bind] ParcelDTO p)
        {
            try
            {
                var pt = await _warehouseLotRepository.GetParcelTypeAsync(p.ParcelTypeId);
                var wh = await _warehouseLotRepository.GetAsync(p.WarehouseLotId, includeParcels: true);

                wh.UpdateParcel(p.Id, p.ContractId, pt, p.Weight);

                _warehouseLotRepository.Update(wh);

                return RedirectToAction("WarehouseDetails", new { id = p.WarehouseLotId });
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            try
            {
                var p = await _warehouseLotRepository.GetParcelAsync(id);
                var wh = await _warehouseLotRepository.GetAsync(p.WarehouseLotId, includeParcels: true);

                wh.RemoveParcel(p);

                _warehouseLotRepository.Update(wh);

                return Ok();
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return BadRequest("An Error occured! Cannot delete this Parcel! --> " + e.Message + " - " + (e.InnerException?.Message));
            }
        }
        #endregion
    }
}
