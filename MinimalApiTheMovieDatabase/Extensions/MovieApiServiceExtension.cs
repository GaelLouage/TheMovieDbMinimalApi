using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using Microsoft.Extensions.Configuration;
using MinimalApiTheMovieDatabase.Helpers;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.WebRequestMethods;

namespace MinimalApiTheMovieDatabase.Extensions
{
    public static class MovieApiServiceExtension
    {
   
        private static string _apikey;
        private const string BASEURL  = "https://api.themoviedb.org/3";
        public static void MovieApi(this WebApplication app, IConfiguration configueration)
        {
            app.GetMovie();
            app.GetMovieList();
            app.PostMovieList();
            app.GetMovieByName();
            _apikey = ReadFile.GetApiKey(configueration).Key;
        }
     
        public static RouteHandlerBuilder GetMovie(this WebApplication app) =>
            app.MapGet("/Movie/{id}", (int id) => RestSharpHelper<MovieEntity>.GetById(id, BASEURL, "movie", _apikey));

        public static RouteHandlerBuilder GetMovieList(this WebApplication app)  =>
             app.MapGet("/Movies",  (int id) => RestSharpHelper<MovieSearchResultEntity>.GetMovieList(BASEURL, "discover/movie", id, _apikey));

        public static RouteHandlerBuilder PostMovieList(this WebApplication app) =>
          app.MapPost("/Movies", (PageEntity page) => RestSharpHelper<MovieSearchResultEntity>.PostMovieList(BASEURL, "discover/movie", page, _apikey));

        public static RouteHandlerBuilder GetMovieByName(this WebApplication app) =>
                app.MapGet("/MoviesByName/{name}", (string movieName) => RestSharpHelper<MovieSearchResultEntity>.GetMovieByName(BASEURL, "discover/movie", movieName, _apikey));


    }
}
