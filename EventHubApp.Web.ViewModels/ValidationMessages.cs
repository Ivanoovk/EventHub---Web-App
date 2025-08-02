

namespace EventHubApp.Web.ViewModels
{
    public static class ValidationMessages
    {
        public static class Event
        {
            // Error messages
            public const string TitleRequiredMessage = "Title is required.";
            public const string TitleMinLengthMessage = "Title must be at least 2 characters.";
            public const string TitleMaxLengthMessage = "Title cannot exceed 100 characters.";

            public const string TypeRequiredMessage = "Event type is required.";
            public const string TypeMinLengthMessage = "Event type must be at least 3 characters.";
            public const string TypeMaxLengthMessage = "Event type cannot exceed 50 characters.";

            public const string SponsorRequiredMessage = "Sponsor is required.";
            public const string SponsorNameMinLengthMessage = "Sponsor name must be at least 2 characters.";
            public const string SponsorNameMaxLengthMessage = "Sponsor name cannot exceed 100 characters.";

            public const string DescriptionRequiredMessage = "Description is required.";
            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 1000 characters.";

            public const string DurationRequiredMessage = "Duration is required.";
            public const string DurationRangeMessage = "Duration must be between 1 and 300 days.";

            public const string ReleaseDateRequiredMessage = "Release date is required.";

            public const string ImageUrlMaxLengthMessage = "Image URL cannot exceed 2048 characters.";

            public const string ServiceCreateError =
                "Fatal error occurred while adding your event! Please try again later!";
        }

        public static class Place
        {
            public const string NameRequiredMessage = "Place name is required.";
            public const string NameMinLengthMessage = "Place name must be at least 2 characters.";
            public const string NameMaxLengthMessage = "Place name cannot exceed 80 characters.";

            public const string LocationRequiredMessage = "Place location is required.";
            public const string LocationMinLengthMessage = "Place location must be at least 2 characters.";
            public const string LocationMaxLengthMessage = "Place location cannot exceed 50 characters.";
        }
    }
}
