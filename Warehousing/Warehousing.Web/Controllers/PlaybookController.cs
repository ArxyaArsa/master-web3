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
    public class PlaybookController : Controller
    {
        #region Attributes and Ctors
        private WarehouseQueries _warehouseQueries;
        private ParcelQueries _parcelQueries;
        private ContractQueries _contractQueries;
        private ParcelTypeQueries _parcelTypeQueries;
        private IWarehouseLotRepository _warehouseLotRepository;
        private readonly IMapper _mapper;

        public PlaybookController(WarehouseQueries warehouseQueries, ParcelQueries parcelQueries, ContractQueries contractQueries, ParcelTypeQueries parcelTypeQueries, IWarehouseLotRepository warehouseLotRepository, IMapper mapper)
        {
            _warehouseQueries = warehouseQueries;
            _parcelQueries = parcelQueries;
            _contractQueries = contractQueries;
            _parcelTypeQueries = parcelTypeQueries;
            _warehouseLotRepository = warehouseLotRepository;
            _mapper = mapper;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetParcelTypes([FromForm] DTParameterModel request)
        {
            string error;
            DTResponse res;

            try
            {
                res = _parcelTypeQueries.GetParcelTypes(request);
            }
            catch (Exception e)
            {
                error = e.Message;

                res = new DTResponse()
                {
                    Data = new List<ParcelTypeDTO>(),
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
        public async Task<IActionResult> _AddEditParcelTypeModalBody(int? id)
        {
            var model = new ParcelTypeDTO();

            if ((id ?? 0) != 0)
            {
                var wh = await _warehouseLotRepository.GetParcelTypeAsync(id.Value);
                model = _mapper.Map<ParcelTypeDTO>(wh);
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult AddParcelType([Bind] ParcelTypeDTO pt)
        {
            try
            {
                _warehouseLotRepository.AddParcelType(pt.Name, pt.Description, pt.MinWeightOccupied, pt.MaxWeight, pt.IsPerishable, pt.FreezerLifetime, pt.DryLifetime);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditParcelType([Bind] ParcelTypeDTO pt)
        {
            try
            {
                var pte = await _warehouseLotRepository.GetParcelTypeAsync(pt.Id);
                pte.Update(pt.Name, pt.Description, pt.MinWeightOccupied, pt.MaxWeight, pt.IsPerishable, pt.FreezerLifetime, pt.DryLifetime);
                _warehouseLotRepository.Update(pte);

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
