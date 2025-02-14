using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Models.ViewModels
{
    public class CandidateViewModel
    {
        [Display(Name = "Candidate ID")]
        public int ID { get; set; }


        [Required(ErrorMessage = "Candidate Name is required")]
        [Display(Name = "Candidate Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Election is required")]
        [Display(Name = "Election Name")]
        public int ElectionID { get; set; }

        public string ElectionName { get; set; }
        public bool HasVoter { get; set; }
    }
}
