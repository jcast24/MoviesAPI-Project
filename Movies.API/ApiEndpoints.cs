namespace Movies.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/Movies";

        public const string Create = Base;
        
        // :guid is a constraint on the route. only accepts guid
        public const string Get = $"{Base}/{{idOrSlug}}";

        public const string GetAll = Base;

        public const string Update = $"{Base}/{{id:guid}}";

        public const string Delete = $"{Base}/{{id:guid}}";
    }
}