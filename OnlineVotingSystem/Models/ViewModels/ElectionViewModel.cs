using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Models.ViewModels
{
    public class ElectionViewModel
    {
        [Display(Name = "Election ID")]
        public int ID { get; set; }


        [Required(ErrorMessage = "Election Name is required")]
        [Display(Name = "Election Name")]
        public string Name { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start Date Time is required")]
        [Display(Name = "Start Date Time")]
        public DateTime StartDateTime { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End Date Time is required")]
        [Display(Name = "End Date Time")]
        public DateTime EndDateTime { get; set; }

        public bool IsVoted { get; set; }
        public bool HasVoter { get; set; }
    }
}
