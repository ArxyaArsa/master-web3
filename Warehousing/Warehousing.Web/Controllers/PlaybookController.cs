using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehousing.Domain.AggregateModels.SupplierAggregate;
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
        private SupplierQueries _supplierQueries;
        private IWarehouseLotRepository _warehouseLotRepository;
        private ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public PlaybookController(WarehouseQueries warehouseQueries, ParcelQueries parcelQueries, ContractQueries contractQueries, ParcelTypeQueries parcelTypeQueries, SupplierQueries supplierQueries, IWarehouseLotRepository warehouseLotRepository, ISupplierRepository supplierRepository, IMapper mapper)
        {
            _warehouseQueries = warehouseQueries;
            _parcelQueries = parcelQueries;
            _contractQueries = contractQueries;
            _parcelTypeQueries = parcelTypeQueries;
            _supplierQueries = supplierQueries;
            _warehouseLotRepository = warehouseLotRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        #endregion

        #region Parcel Types
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
        #endregion

        #region Suppliers
        public IActionResult Suppliers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetSuppliers([FromForm] DTParameterModel request)
        {
            string error;
            DTResponse res;

            try
            {
                res = _supplierQueries.GetSuppliers(request);
            }
            catch (Exception e)
            {
                error = e.Message;

                res = new DTResponse()
                {
                    Data = new List<SupplierDTO>(),
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
        public async Task<IActionResult> _AddEditSupplierModalBody(int? id)
        {
            var model = new SupplierDTO();

            if ((id ?? 0) != 0)
            {
                var s = await _supplierRepository.GetAsync(id.Value);
                model = _mapper.Map<SupplierDTO>(s);
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult AddSupplier([Bind] SupplierDTO sup)
        {
            try
            {
                var c = new SupplierContact(sup.Contact_FirstName, sup.Contact_LastName, sup.Contact_Phone, sup.Contact_Email, sup.Contact_Fax);
                var a = new SupplierAddress(sup.Address_State, sup.Address_Country, sup.Address_PostalCode, sup.Address_AddressLine1, sup.Address_AddressLine2);
                var se = new Supplier(sup.Name, sup.Description, c, a);

                _supplierRepository.Add(se);

                return RedirectToAction("Suppliers");
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditSupplier([Bind] SupplierDTO s)
        {
            try
            {
                var se = await _supplierRepository.GetAsync(s.Id);
                var c = new SupplierContact(s.Contact_FirstName, s.Contact_LastName, s.Contact_Phone, s.Contact_Email, s.Contact_Fax);
                var a = new SupplierAddress(s.Address_State, s.Address_Country, s.Address_PostalCode, s.Address_AddressLine1, s.Address_AddressLine2);
                se.Update(s.Name, s.Description, c, a);
                _supplierRepository.Update(se);

                return RedirectToAction("Suppliers");
            }
            catch (Exception e)
            {
                // more error handling? dev mode?
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message + " - " + (e.InnerException?.Message)));
            }
        }
        #endregion

        #region Contracts

        #endregion
    }
}
