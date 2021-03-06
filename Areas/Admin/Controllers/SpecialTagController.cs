﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Data;
using Online_Shop.Models;

namespace Online_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize]
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagController(ApplicationDbContext db)
        {
            _db = db;
        }

        //SpecialTags SpecialTags = new SpecialTags();
        public IActionResult Index()
        {
            var data = _db.SpecialTags.ToList();
            return View(data);
        }


        //Create GET Method
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                // Create
                return View(new SpecialTags());
            }
            //Update
            var SpecialTg = _db.SpecialTags.Find(id);
            if (SpecialTg == null)
            {
                return NotFound();
            }
            return View(SpecialTg);
        }

        //Create Post Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecialTags SpecialTags)
        {
            if (ModelState.IsValid)
            {
                var searchProductType = _db.SpecialTags.FirstOrDefault(
                    x => x.SpecialTag == SpecialTags.SpecialTag);
                if (searchProductType != null)
                {
                    ViewBag.message = "This Tag is Already Exists";
                    return View(SpecialTags);
                }
                if (SpecialTags.Id == 0)
                {
                    TempData["save"] = "Special Tag has been Saved";
                    _db.SpecialTags.Add(SpecialTags);
                }
                else
                {
                    TempData["save"] = "Update";
                    _db.SpecialTags.Update(SpecialTags);

                }

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(SpecialTags);
        }

        //Details Page
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var SpecialTg = _db.SpecialTags.Find(id);
            if (SpecialTg == null)
            {
                return NotFound();
            }
            return View(SpecialTg);
        }

        //Delete Get Method
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var SpecialTg = _db.SpecialTags.Find(id);
            if (SpecialTg == null)
            {
                return NotFound();
            }
            return View(SpecialTg);
        }

        //Delete Post Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? Id, SpecialTags SpecialTags)
        {
            if (Id == null) return NotFound();
            if (Id != SpecialTags.Id) return NotFound();
            var SpecialTg = _db.SpecialTags.Find(Id);
            if (SpecialTg == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                TempData["save"] = "Delete";
                _db.Remove(SpecialTg);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(SpecialTags);
        }

    }
}