

using System.ComponentModel.DataAnnotations;

using static EventHubApp.GCommon.ApplicationConstants;
using static EventHubApp.Web.ViewModels.ValidationMessages.Event;
using static EventHubApp.Data.Common.EntityConstants.Event;

namespace EventHubApp.Web.ViewModels.Event
{
    public class EventFormInputModel
    {
        
        public string Id { get; set; }
            = string.Empty;

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = TypeRequiredMessage)]
        [MinLength(TypeMinLength, ErrorMessage = TypeMinLengthMessage)]
        [MaxLength(TypeMaxLength, ErrorMessage = TypeMaxLengthMessage)]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; } = null!;

        [Required(ErrorMessage = DurationRequiredMessage)]
        [Range(DurationMin, DurationMax, ErrorMessage = DurationRangeMessage)]
        public int Duration { get; set; }

        [Required(ErrorMessage = SponsorRequiredMessage)]
        [MinLength(SponsorNameMinLength, ErrorMessage = SponsorNameMinLengthMessage)]
        [MaxLength(SponsorNameMaxLength, ErrorMessage = SponsorNameMaxLengthMessage)]
        public string Sponsor { get; set; } = null!;

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthMessage)]
        public string? ImageUrl { get; set; }
            = $"/images/{NoImageUrl}";
    }
}

