﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using MvcPL.Models;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.RememberMe);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Lot");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterViewModel viewModel)
        {
            //if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            //{
            //    ModelState.AddModelError("Captcha", "Incorrect input.");
            //    return View(viewModel);
            //}

            if (ModelState.IsValid)
            {
                var users = _userService.GetAllUserEntities().ToList();
                var userWithSameLogin = users.Any(user => user.Login.Contains(viewModel.Login));

                if (userWithSameLogin)
                {
                    ModelState.AddModelError("", "User with this login already exist.");
                    return View(viewModel);
                }

                var membershipUser = ((CustomMembershipProvider) Membership.Provider)
                    .CreateUser(viewModel);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                    return RedirectToAction("Index", "Lot");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }

        ////В сессии создаем случайное число от 1111 до 9999.
        ////Создаем в ci объект CatchaImage
        ////Очищаем поток вывода
        ////Задаем header для mime-типа этого http-ответа будет "image/jpeg" т.е. картинка формата jpeg.
        ////Сохраняем bitmap в выходной поток с форматом ImageFormat.Jpeg
        ////Освобождаем ресурсы Bitmap
        ////Возвращаем null, так как основная информация уже передана в поток вывод
        //[AllowAnonymous]
        //public ActionResult Captcha()
        //{
        //    Session[CaptchaImage.CaptchaValueKey] =
        //        new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
        //    var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

        //    // Change the response headers to output a JPEG image.
        //    this.Response.Clear();
        //    this.Response.ContentType = "image/jpeg";

        //    // Write the image to the response stream in JPEG format.
        //    ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

        //    // Dispose of the CAPTCHA image object.
        //    ci.Dispose();
        //    return null;
        //}

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
    }
}