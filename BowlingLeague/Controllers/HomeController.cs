using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        //private BowlingDbContext _blahContext { get; set; }
        private IBowlingRepository repo { get; set; }

        public HomeController(IBowlingRepository temp, BowlingDbContext temp2)
        {
            repo = temp;
            //_blahContext = temp2;
        }

        public IActionResult Index(string teamName, int pageNum =1)
        {
            int pageSize = 20;
            //var bowlers = repo.Bowlers
            //    .OrderBy(b=>b.BowlerID)
            //    .Include(x => x.Team)
            //    .Skip(pageNum - 1 * pageSize)
            //    .Take();
            ViewData["TeamName"] = teamName;

            var bowlers = new BowlersViewModel
            {
                Bowlers = repo.Bowlers
                .Include(t => t.Team)
                .Where(t => t.Team.TeamName == teamName || teamName == null)
                .OrderBy(b => b.BowlerID)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBowlers = (teamName == null ? repo.Bowlers.Count() : repo.Bowlers
                    .Where(x => x.Team.TeamName == teamName).Count()),
                    BowlersPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            
            ViewBag.Teams = repo.Teams.ToList();
            return View(bowlers);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            

            ViewBag.Teams = repo.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Teams = repo.Teams.ToList();

                repo.CreateBowler(bowler);
                repo.SaveBowler(bowler);
                return RedirectToAction("Index");
            }

    else
            {
                ViewBag.Teams = repo.Teams.ToList();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id) 
	    {
            var editBowler = repo.Bowlers.Single(x=>x.BowlerID == id);
            ViewBag.Teams = repo.Teams.ToList();
            return View("EditBowler", editBowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                repo.SaveBowler(bowler);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = repo.Teams.ToList();
                return View("EditBowler");
            }
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteBowler = repo.Bowlers.Single(x => x.BowlerID == id);
            return View(deleteBowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            repo.DeleteBowler(b);
            return RedirectToAction("Index");
        }

    }
}
