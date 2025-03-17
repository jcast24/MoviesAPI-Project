namespace Movies.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class V1
    {
        private const string VersionBase = $"{ApiBase}/v1";
        public static class Movies
        {
            private const string Base = $"{VersionBase}/Movies";

            public const string Create = Base;

            // :guid is a constraint on the route. only accepts guid
            public const string Get = $"{Base}/{{idOrSlug}}";

            public const string GetAll = Base;

            public const string Update = $"{Base}/{{id:guid}}";

            public const string Delete = $"{Base}/{{id:guid}}";

            // Ratings endpoint
            public const string Rate = $"{Base}/{{id:guid}}/ratings";
            public const string DeleteRating = $"{Base}/{{id:guid}}/ratings";
        }

        // Get all ratings
        public static class Ratings
        {
            private const string Base = $"{VersionBase}/ratings";

            // the /me is pretty common
            public const string GetUserRatings = $"{Base}/me";
        }
    }
}