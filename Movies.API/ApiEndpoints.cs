namespace Movies.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/Movies";

        public const string Create = Base;
        
        // :guid is a constraint on the route. only accepts guid
        public const string Get = $"{Base}/{{id:guid}}";

        public const string GetAll = Base;
    }
}