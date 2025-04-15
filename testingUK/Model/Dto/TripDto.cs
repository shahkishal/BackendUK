using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace testingUK.Model.Dto
{
    public class TripDto
    {
       public string Source { get; set; }

       public string Destination { get; set; }

       public DateOnly From { get; set; }

       public DateOnly To { get; set; }

       public int? Duration { get; set; }

       public string? Description { get; set; }

        public bool IsPublic { get; set; }


        // Navigation Property
        public TravelType TravelType { get; set; }

    }
}
