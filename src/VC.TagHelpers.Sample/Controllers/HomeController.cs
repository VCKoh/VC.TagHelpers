using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VC.TagHelpers.Sample.Extensions;
using VC.TagHelpers.Sample.Models;

namespace VC.TagHelpers.Sample.Controllers
{
    public class HomeController : Controller
    {
        private const string ItemListSessionKey = "ItemList";
        private const int PageSize = 10;

        public IActionResult Index(int page = 1)
        {
            if (page < 1)
                page = 1;

            ViewBag.PageSize = PageSize;

            //Sample data
            var vm = HttpContext.Session.GetObjectFromJson<List<ItemViewModel>>(ItemListSessionKey);

            if (vm == null)
            {
                GenerateSampleData();
                vm = HttpContext.Session.GetObjectFromJson<List<ItemViewModel>>(ItemListSessionKey);
            }

            ViewBag.TotalItemCount = vm.Count;

            return View(vm.Page(page, PageSize).ToList());
        }

        private void GenerateSampleData()
        {
            var itemList = new List<ItemViewModel>();

            for (var i = 1; i <= 100; i++)
                itemList.Add(new ItemViewModel
                {
                    Id = i.ToString(),
                    ItemName = $"Item {i}"
                });

            HttpContext.Session.SetObjectAsJson(ItemListSessionKey, itemList);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}