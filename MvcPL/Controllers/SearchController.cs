using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuctionLog;
using BLL.Interface.InterfaceServices;

namespace MvcPL.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILotService _lotService;

        public SearchController(ILotService lotService)
        {
            _lotService = lotService;
        }

        struct SearchResponseElement
        {
            public string Type { get; set; }
            public string Title { get; set; }
            public string Link { get; set; }
        }

        [HttpGet]
        public JsonResult Search(string term)
        {
            var list = new List<SearchResponseElement>();
            try
            {
                var lots = _lotService.GetActiveLots().Where(t => t.LotName.ToUpper().Contains(term.ToUpper()));
                foreach (var lot in lots)
                {
                    list.Add(new SearchResponseElement()
                    {
                        Type = "Lot",
                        Title = lot.LotName,
                        Link = Url.Action("LotDetails", "Lot", new { id = lot.Id })
                    });
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return
                    Json(new List<SearchResponseElement>()
                    {
                        new SearchResponseElement()
                        {
                            Type = "Error",
                            Link = Url.Action("Index", "Lot"),
                            Title = "Search error"
                        }
                    });
            }
        }

    }
}