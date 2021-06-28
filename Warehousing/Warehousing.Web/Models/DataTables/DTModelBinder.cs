using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DataTables
{
    public class DTModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var requestForm = bindingContext.HttpContext.Request.Form;
            // Retrieve request data
            var draw = Convert.ToInt32(requestForm["draw"]);
            var start = Convert.ToInt32(requestForm["start"]);
            var length = Convert.ToInt32(requestForm["length"]);
            // Search
            var search = new DTSearch
            {
                Value = requestForm["search[value]"],
                Regex = Convert.ToBoolean(requestForm["search[regex]"])
            };

            // Order
            var o = 0;
            var order = new List<DTOrder>();
            while (!string.IsNullOrEmpty(requestForm["order[" + o + "][column]"]))
            {
                order.Add(new DTOrder
                {
                    Column = Convert.ToInt32(requestForm["order[" + o + "][column]"]),
                    Dir = requestForm["order[" + o + "][dir]"]
                });
                o++;
            }

            // Columns
            var c = 0;
            var columns = new List<DTColumn>();
            while (!string.IsNullOrEmpty(requestForm["columns[" + c + "][name]"]) || !string.IsNullOrEmpty(requestForm["columns[" + c + "][data]"]))
            {
                columns.Add(new DTColumn
                {
                    Data = requestForm["columns[" + c + "][data]"],
                    Name = requestForm["columns[" + c + "][name]"],
                    Orderable = Convert.ToBoolean(requestForm["columns[" + c + "][orderable]"]),
                    Searchable = Convert.ToBoolean(requestForm["columns[" + c + "][searchable]"]),
                    Search = new DTSearch
                    {
                        Value = requestForm["columns[" + c + "][search][value]"],
                        Regex = Convert.ToBoolean(requestForm["columns[" + c + "][search][regex]"])
                    }
                });
                c++;
            }

            bindingContext.Result = ModelBindingResult.Success(new DTParameterModel
            {
                Draw = draw,
                Start = start,
                Length = length,
                Search = search,
                Order = order,
                Columns = columns
            });

            return Task.CompletedTask;
        }
    }
}
