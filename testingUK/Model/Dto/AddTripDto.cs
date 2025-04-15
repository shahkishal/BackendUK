using System.ComponentModel.DataAnnotations;

namespace testingUK.Model.Dto
{
    public class AddTripDto
    {
        [Required]
        public string Source { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateOnly From { get; set; }

        [Required]
        public DateOnly To { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1")]
        public int? Duration { get; set; }

       
        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        public Guid TravelTypeId { get; set; }
    }
}
