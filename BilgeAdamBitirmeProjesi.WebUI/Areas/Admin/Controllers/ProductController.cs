using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Category;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IProductApi _productApi;
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public ProductController(
            IWebHostEnvironment env,
            IProductApi productApi,
            ICategoryApi categoryApi,
            IMapper mapper)
        {
            _env = env;
            _productApi = productApi;
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            var listModelResult = await _productApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                model = _mapper.Map<List<ProductViewModel>>(listModelResult.Content);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            List<CategoryResponse> model = new List<CategoryResponse>();
            var listModelResult = await _categoryApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                model = listModelResult.Content;
            ViewBag.Categories = new SelectList(model, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CreateProductViewModel item, List<IFormFile> files)
        {
            item.UserId = Guid.Parse(User.Claims?.FirstOrDefault(x => x.Type == "Id").Value);
            if (ModelState.IsValid)
            {
                bool imgResult;
                string img = Upload.ImageUpload(files, _env, out imgResult);
                if (imgResult)
                    item.Image = img;
                else
                {
                    ViewBag.Message = img;
                    return View();
                }

                var insertResult = await _productApi.Post(_mapper.Map<ProductRequest>(item));
                if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Kayıt işlemi sırasında bir hata oluştu. Lütfen tüm alanları kontrol edip tekrar deneyin.";
                }
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu!.. Lütfen tüm alanları kontrol edip tekrar deneyin.";
            }
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            //Gelen veri türünü mapleyip geri gönderiyorum.
            List<CategoryResponse> categoryList = new List<CategoryResponse>();
            var listModelResult = await _categoryApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                categoryList = listModelResult.Content;
            ViewBag.Categories = new SelectList(categoryList, "Id", "CategoryName");

            UpdateProductViewModel updateModel = new UpdateProductViewModel();
            var updateModelResult = await _productApi.Get(id);
            if (updateModelResult.IsSuccessStatusCode && updateModelResult.Content != null)
                updateModel = _mapper.Map<UpdateProductViewModel>(updateModelResult.Content);
            return View(updateModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel item)
        {
            item.UserId = Guid.Parse(User.Claims?.FirstOrDefault(x => x.Type == "Id").Value).ToString();
            if (ModelState.IsValid)
            {
                var updateResult = await _productApi.Put(item.Id, _mapper.Map<ProductRequest>(item));
                if (updateResult.IsSuccessStatusCode || updateResult.Content != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm alanları kontrol edip tekrar deneyin.";
                }
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu!.. Lütfen tüm alanları kontrol edip tekrar deneyin.";
            }
            return View(item);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _productApi.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(Guid id)
        {
            var sonuc = await _productApi.Activate(id);
            return RedirectToAction("Index");
        }
    }
}
