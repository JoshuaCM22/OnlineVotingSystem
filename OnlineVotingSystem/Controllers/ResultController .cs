using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Controllers
{
    [Route("result")]
    [Authorize(Roles = "Admin, Voter")]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
        private readonly IHubContext<ElectionResultHub> _hubContext;
        public ResultController(IResultService resultService, IHubContext<ElectionResultHub> hubContext)
        {
            _resultService = resultService;
            _hubContext = hubContext;
        }


        [Route("election-list")]
        [HttpGet]
        public async Task<IActionResult> ElectionList(DateTime? fromDate, DateTime? toDate)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            List<ElectionViewModel> electionList = null;
            try
            {
                if (!fromDate.HasValue) fromDate = DateTime.Now.AddMonths(-1);
                if (!toDate.HasValue) toDate = DateTime.Now;
                electionList = await _resultService.GetElectionList(fromDate.Value, toDate.Value);
                HttpContext.Session.Remove("Selected_ElectionID");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in ElectionList(). Error Message: " + ex.Message;
            }
            return View(electionList);
        }

        [Route("set-election-id-session/{id}")]
        [HttpGet]
        public IActionResult SetElectionIDSession(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");
            HttpContext.Session.SetInt32("Selected_ElectionID", id);

            return RedirectToAction("candidate-list", "result");
        }

        [Route("candidate-list")]
        [HttpGet]
        public async Task<IActionResult> CandidateList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            var electionID = HttpContext.Session.GetInt32("Selected_ElectionID");
            if (electionID == null) throw new Exception("electionID is null");

            List<ResultViewModel> resultList = null;

            try
            {
                resultList = await _resultService.GetCandidateList(Convert.ToInt32(electionID));
                ViewBag.ElectionName = await _resultService.GetElectionName(Convert.ToInt32(electionID));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in CandidateList(). Error Message: " + ex.Message;
            }
            return View(resultList);
        }

    }
}
