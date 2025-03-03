using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("dashboard")]
        [HttpGet]
        public async Task<IActionResult> Dashboard(DateTime? fromDate, DateTime? toDate)
        {
            DashboardViewModel viewModel = null;
            try
            {
                if (!fromDate.HasValue) fromDate = DateTime.Now.AddMonths(-1);
                if (!toDate.HasValue) toDate = DateTime.Now;
                viewModel = await _adminService.GetDashboardCounts(fromDate.Value, toDate.Value);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in Dashboard(). Error Message: " + ex.Message;
            }
            return View(viewModel);
        }



        [Route("election-list")]
        [HttpGet]
        public async Task<IActionResult> ElectionList(DateTime? fromDate, DateTime? toDate, int? statusId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            List<ElectionViewModel> electionList = null;
            try
            {
                if (!fromDate.HasValue) fromDate = DateTime.Now.AddMonths(-1);
                if (!toDate.HasValue) toDate = DateTime.Now;
                ViewBag.StatusList =  _adminService.GetAllElectionStatuses();
                electionList = await _adminService.GetElectionList(fromDate.Value, toDate.Value, statusId ?? 0);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in ElectionList(). Error Message: " + ex.Message;
            }
            return View(electionList);
        }

        [Route("election/create-new-election/")]
        [HttpGet]
        public IActionResult CreateNewElection()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");
            return View();
        }

        [Route("election/create-new-election/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewElection(ElectionViewModel electionViewModel)
        {
            try
            {
                await _adminService.CreateNewElection(electionViewModel);
                TempData["successMessage"] = "Successfully Created";
                return RedirectToAction("election-list", "admin");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in CreateNewElection(). Error Message: " + ex.Message;
            }
            return View();
        }

        [Route("election/edit/{electionId}")]
        [HttpGet]
        public async Task<IActionResult> EditElection(int electionId)
        {
            ElectionViewModel electionViewModel = null;

            try
            {
                electionViewModel = await _adminService.GetElectionByID(electionId);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in EditElection(). Error Message: " + ex.Message;
            }
            return View(electionViewModel);
        }

        [Route("election/edit/{electionId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditElection(ElectionViewModel electionViewModel)
        {
            try
            {
                await _adminService.UpdateElection(electionViewModel);
                TempData["successMessage"] = "Successfully Saved";
                return RedirectToAction("election-list", "admin");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in EditElection(). Error Message: " + ex.Message;
            }
            return View();
        }


        [Route("election/delete/{electionId}")]
        [HttpGet]
        public async Task<IActionResult> DeleteElection(int electionId)
        {
            ElectionViewModel electionViewModel = null;

            try
            {
                electionViewModel = await _adminService.GetElectionByID(electionId);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in DeleteElection(). Error Message: " + ex.Message;
            }
            return View(electionViewModel);
        }

        [Route("election/delete/{electionId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteElection(ElectionViewModel electionViewModel)
        {
            try
            {
                await _adminService.DeleteElection(electionViewModel);
                TempData["successMessage"] = "Successfully Deleted";
                return RedirectToAction("election-list", "admin");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in DeleteElection(). Error Message: " + ex.Message;
            }
            return View();
        }

        [Route("candidate-list")]
        [HttpGet]
        public async Task<IActionResult> CandidateList(DateTime? fromDate, DateTime? toDate, int? statusId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            List<CandidateViewModel> candidateList = null;
            try
            {
                if (!fromDate.HasValue) fromDate = DateTime.Now.AddMonths(-1);
                if (!toDate.HasValue) toDate = DateTime.Now;
                ViewBag.StatusList = _adminService.GetAllElectionStatuses();
                candidateList = await _adminService.GetCandidateList(fromDate.Value, toDate.Value, statusId ?? 0);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in CandidateList(). Error Message: " + ex.Message;
            }
            return View(candidateList);
        }

        [Route("candidate/create-new-candidate/")]
        [HttpGet]
        public async Task<IActionResult> CreateNewCandidate()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");
            try
            {
                ViewBag.ElectionList = await _adminService.GetUpComingElections();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in CreateNewCandidate(). Error Message: " + ex.Message;
            }
            return View();
        }

        [Route("candidate/create-new-candidate/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewCandidate(CandidateViewModel candidateViewModel)
        {
            try
            {
                await _adminService.CreateNewCandidate(candidateViewModel);
                TempData["successMessage"] = "Successfully Created";
                return RedirectToAction("candidate-list", "admin");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in CreateNewCandidate(). Error Message: " + ex.Message;
            }
            return View();
        }


        [Route("candidate/edit/{candidateId}")]
        [HttpGet]
        public async Task<IActionResult> EditCandidate(int candidateId)
        {
            CandidateViewModel candidateViewModel = null;

            try
            {
                candidateViewModel = await _adminService.GetCandidateByID(candidateId);
                ViewBag.ElectionList = await _adminService.GetUpComingElections();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in EditCandidate(). Error Message: " + ex.Message;
            }
            return View(candidateViewModel);
        }

        [Route("candidate/edit/{candidateId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidate(CandidateViewModel candidateViewModel)
        {
            try
            {
                await _adminService.UpdateCandidate(candidateViewModel);
                TempData["successMessage"] = "Successfully Saved";
                return RedirectToAction("candidate-list", "admin");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in EditCandidate(). Error Message: " + ex.Message;
            }
            return View();
        }



        [Route("candidate/delete/{candidateId}")]
        [HttpGet]
        public async Task<IActionResult> DeleteCandidate(int candidateId)
        {
            CandidateViewModel candidateViewModel = null;

            try
            {
                candidateViewModel = await _adminService.GetCandidateByID(candidateId);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in DeleteCandidate(). Error Message: " + ex.Message;
            }
            return View(candidateViewModel);
        }

        [Route("candidate/delete/{candidateId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCandidate(CandidateViewModel candidateViewModel)
        {
            try
            {
                await _adminService.DeleteCandidate(candidateViewModel);
                TempData["successMessage"] = "Successfully Deleted";
                return RedirectToAction("candidate-list", "admin");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error has occured in DeleteCandidate(). Error Message: " + ex.Message;
            }
            return View();
        }

    }
}
