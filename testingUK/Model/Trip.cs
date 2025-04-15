using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using testingUK.Model.Dto;

namespace testingUK.Model
{
    public class Trip
    {
        public Guid Id { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }

        public int? Duration { get; set; }

        public string? Description { get; set; }

        public Guid TravelTypeId { get; set; }

        public string UserId { get; set; }

        public bool IsPublic { get; set; }

        // Navigation Properties

        public TravelType TravelType { get; set; }     

    }
}
