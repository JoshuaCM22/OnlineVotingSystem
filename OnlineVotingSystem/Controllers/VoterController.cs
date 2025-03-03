using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Voter;
using OnlineVotingSystem.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Controllers
{
    [Route("voter")]
    [Authorize(Roles = "Admin, Voter")]
    public class VoterController : Controller
    {
        private readonly IVoterService _voterService;
        private readonly IHubContext<ElectionResultHub> _hubContext;
        public VoterController(IVoterService voterService, IHubContext<ElectionResultHub> hubContext)
        {
            _voterService = voterService;
            _hubContext = hubContext;
        }



        [Route("election-list")]
        [HttpGet]
        public async Task<IActionResult> ElectionList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            List<ElectionViewModel> electionList = null;
            try
            {
                electionList = await _voterService.GetElectionList(User.Identity.Name.Trim());
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

            return RedirectToAction("candidate-list", "voter");
        }

        [Route("candidate-list")]
        [HttpGet]
        public async Task<IActionResult> CandidateList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");
            var electionID = HttpContext.Session.GetInt32("Selected_ElectionID");
            if (electionID == null) return RedirectToAction("election-list", "voter");

            List<CandidateViewModel> candidateList = null;
            try
            {
                candidateList = await _voterService.GetCandidateList(Convert.ToInt32(electionID));
                ViewBag.ElectionName = await _voterService.GetElectionName(Convert.ToInt32(electionID));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in CandidateList(). Error Message: " + ex.Message;
            }
            return View(candidateList);
        }

        [Route("vote-candidate/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VoteCandidate(string id)
        {
            try
            {
                string[] values = id.Split(";");
                await _voterService.VoteCandidate(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), User.Identity.Name);

                await _hubContext.Clients.All.SendAsync("UpdateElectionResultView");

                TempData["successMessage"] = "Successfully voted.";
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in VoteCandidate(). Error Message: " + ex.Message;
            }
            return RedirectToAction("election-list", "voter");
        }


        [Route("your-voting-history")]
        [HttpGet]
        public async Task<IActionResult> YourVotingHistory(int electionID, DateTime? fromDate, DateTime? toDate)
        {
            List<VotedHistoryViewModel> votedHistory = null;
            try
            {
                if (!fromDate.HasValue) fromDate = DateTime.Now.AddMonths(-1);
                if (!toDate.HasValue) toDate = DateTime.Now;
                votedHistory = await _voterService.GetVotedHistory(User.Identity.Name, electionID, fromDate.Value, toDate.Value);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in YourVotingHistory(). Error Message: " + ex.Message;
            }
            return View(votedHistory);
        }

    }
}
