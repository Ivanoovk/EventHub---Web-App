
namespace EventHubApp.Data.Common
{
    public static class EntityConstants
    {
        public static class Event
        {

            /// <summary>
            /// Event Title should be able to store text with length up to 100 characters.
            /// </summary>
            public const int TitleMaxLength = 100;

            public const int TitleMinLength = 2;

            /// <summary>
            /// Type must be at least 3 characters.
            /// </summary>
            public const int TypeMinLength = 3;

            /// <summary>
            /// Event Type should be able to store text with length up to 50 characters.
            /// </summary>
            public const int TypeMaxLength = 50;

            /// <summary>
            /// Sponsor name must be at least 2 characters.
            /// </summary>
            public const int SponsorNameMinLength = 2;

            /// <summary>
            /// Event Sponsor should be able to store text with length up to 100 characters.
            /// </summary>
            public const int SponsorNameMaxLength = 100;

            /// <summary>
            /// Event Description must be at least 10 characters.
            /// </summary>
            public const int DescriptionMinLength = 10;

            /// <summary>
            /// Event Description should be able to store text with length up to 1000 characters.
            /// </summary>
            public const int DescriptionMaxLength = 1000;

            /// <summary>
            /// Event Duration should be between 1 and 300 minutes.
            /// </summary>
            public const int DurationMin = 1;

            /// <summary>
            /// Event Duration should be between 1 and 300 minutes.
            /// </summary>
            public const int DurationMax = 300;

            /// <summary>
            /// Maximum allowed length for image URL.
            /// </summary>
            public const int ImageUrlMaxLength = 2048;
        }

        public static class Place
        {
            /// <summary>
            /// Place Name must be at least 2 characters.
            /// </summary>
            public const int NameMinLength = 2;

            /// <summary>
            /// Place Name should be able to store text with length up to 80 characters.
            /// </summary>
            public const int NameMaxLength = 80;

            /// <summary>
            /// Place Location must be at least 2 characters.
            /// </summary>
            public const int LocationMinLength = 2;

            /// <summary>
            /// Place Location should be able to store text with length up to 50 characters.
            /// </summary>
            public const int LocationMaxLength = 50;
        }

        public static class PlaceEvent
        {
            public const int AvailableTicketsDefaultValue = 0;
            public const int ShowtimeMaxLength = 5;
            public const string ShowtimeFormat = "{hh}:{mm}";
        }


    }
}
