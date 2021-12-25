using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UserController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;
        public UserController(
            IWebHostEnvironment env,
            IUserApi userApi,
            IMapper mapper)
        {
            _env = env;
            _userApi = userApi;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> list = new List<UserViewModel>();
            //User sorgu çektim eğer data varsa liste dolar yoksa geri döner.
            var listResult = await _userApi.List();
            if (listResult.IsSuccessStatusCode && listResult.Content.Any())
                list = _mapper.Map<List<UserViewModel>>(listResult.Content);
            return View(list);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CreateUserViewModel item, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var insertResult = await _userApi.Post(_mapper.Map<UserRequest>(item));
                if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işleminde bir hata oluştu. Lütfen tüm alanları kontrol ediniz."; 
            }
            else
            {
                TempData["Message"] = "İşlem Başarısız oldu. Lütfen tüm alanları kontrol ediniz.";
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            UpdateUserViewModel model = new UpdateUserViewModel();
            var updateModelResult = await _userApi.Get(id);
            if (updateModelResult.IsSuccessStatusCode || updateModelResult.Content != null)
                model = _mapper.Map<UpdateUserViewModel>(updateModelResult.Content);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel item)
        {
            if (ModelState.IsValid)
            {
                var updateResult = await _userApi.Put(item.Id, _mapper.Map<UserRequest>(item));
                if (updateResult.IsSuccessStatusCode || updateResult.Content != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Güncelleme sırasında bir hata oluştu. Lütfen tüm alanları kontrol ediniz.";
            }
            else
                TempData["Message"] = "İşlem Başarısız oldu. Lütfen tüm alanları kontrol ediniz.";
            return View(item);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedResult = await _userApi.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(Guid id)
        {
            var activateResult = await _userApi.Activate(id);
            return RedirectToAction("Index");
        }
    }
}
