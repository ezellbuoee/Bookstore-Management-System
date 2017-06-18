﻿using BSMS.bsms.localhost;
using BSMS.Message;
using BSMS.Models;
using BSMS.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSMS.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            return View(AuthorModel.GetAuthors());
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            return View(AuthorModel.Filter(id));
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(AUTHOR author, HttpPostedFileBase thumbnail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (thumbnail != null && !String.IsNullOrEmpty(thumbnail.FileName))
                    {
                        String fname = Generator.RandomString(10) + "." + thumbnail.FileName.Split('.')[thumbnail.FileName.Split('.').Length - 1];
                        string path = Server.MapPath("~/UserImages/") + fname;
                        author.THUMBNAIL_PATH = "/UserImages/" + fname;
                        thumbnail.SaveAs(path);
                    }
                    else
                    {
                        author.THUMBNAIL_PATH = "/UserImages/avatar.png";
                    }
                    AuthorModel.Create(author);
                    ViewBag.Message = SuccessMessage.AUTHOR_ADDED;
                }
                else
                {
                    ViewBag.ErrorMessage = ErrorMessage.REQUIRED_ASTERIC_FIELDS;
                }

                return View();
            }
            catch
            {
                ViewBag.ErrorMessage = ErrorMessage.INTERNAL_ERROR;
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            return View(AuthorModel.Filter(id));
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AUTHOR author, HttpPostedFileBase thumbnail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (thumbnail != null && !String.IsNullOrEmpty(thumbnail.FileName))
                    {
                        String fname = Generator.RandomString(10) + "." + thumbnail.FileName.Split('.')[thumbnail.FileName.Split('.').Length - 1];
                        string path = Server.MapPath("~/UserImages/") + fname;
                        author.THUMBNAIL_PATH = "/UserImages/" + fname;
                        thumbnail.SaveAs(path);
                    }
                    else
                    {
                        author.THUMBNAIL_PATH = AuthorModel.Filter(id).THUMBNAIL_PATH;
                    }
                    AuthorModel.Update(author);
                    ViewBag.Message = SuccessMessage.AUTHOR_EDITED;
                }
                else
                {
                    ViewBag.ErrorMessage = ErrorMessage.REQUIRED_ASTERIC_FIELDS;
                }

                return View();
            }
            catch
            {
                ViewBag.ErrorMessage = ErrorMessage.INTERNAL_ERROR;
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            AuthorModel.Delete(id);
            return RedirectToAction("index");
        }

    }
}