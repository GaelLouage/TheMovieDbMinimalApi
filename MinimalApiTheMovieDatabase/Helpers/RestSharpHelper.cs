using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiTheMovieDatabase.Helpers
{
    public static class RestSharpHelper<T> 
    {
        public static IResult GetById(int id, string baseUrl, string apiType, string apiKey)
        {
            var result = new ResultDto<T>();
            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{apiType}/{id}", Method.Get);
            request.AddParameter("api_key", apiKey);

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                result.Result = JsonConvert.DeserializeObject<T>(response.Content);
            }
            if (result.Result is null)
            {
                result.Errors.Add($"{apiType} with id {id} not found!");
            }
            return Results.Ok(result);
        }

        public static async Task<IResult> GetMovieList(string baseUrl, string apiType, int id, string apiKey)
        {
            var result = new ResultDto<List<MovieEntity>>();
            result.Result = new List<MovieEntity>();

            var client = new RestClient(baseUrl);
            var request = new RestRequest(apiType, Method.Get);
            request.AddParameter("api_key", apiKey);
            request.AddParameter("sort_by", "popularity.desc");
            request.AddParameter("page", id);

            var response = await client.ExecuteAsync<MovieSearchResultEntity>(request);
            if (response.IsSuccessful)
            {
                foreach (var data in response.Data.Results)
                {
                    result.Result.Add(data);
                }
            }
            else
            {
                result.Errors.Add(response.ErrorMessage);
            }

            return Results.Ok(result);
        }
        public static async Task<IResult> PostMovieList(string baseUrl, string apiType, PageEntity page, string apiKey)
        {
            var result = new ResultDto<List<MovieEntity>>();
            result.Result = new List<MovieEntity>();
        
            foreach (var item in page.Pages)
            {
                var client = new RestClient(baseUrl);
                var request = new RestRequest(apiType, Method.Get);
                request.AddParameter("api_key", apiKey);
                request.AddParameter("sort_by", "popularity.desc");
                request.AddParameter("page", item);
                var response = await client.ExecuteAsync<MovieSearchResultEntity>(request);
                if (response.IsSuccessful)
                {
                    foreach (var data in response.Data.Results)
                    {
                        result.Result.Add(data);
                    }
                }
                else
                {
                    result.Errors.Add(response.ErrorMessage);
                }
            }
         
            return Results.Ok(result);
        }
        public static async Task<IResult> GetMovieByName(string baseUrl, string apiType, string movieName, string apiKey)
        {
            var result = new ResultDto<MovieEntity>();
            result.Result = new MovieEntity();

            foreach (var item in new List<int> {1,2,3,4,5,6,7,9,10})
            {
                var client = new RestClient(baseUrl);
                var request = new RestRequest(apiType, Method.Get);
                request.AddParameter("api_key", apiKey);
                request.AddParameter("sort_by", "popularity.desc");
                request.AddParameter("page", item);
                var response = await client.ExecuteAsync<MovieSearchResultEntity>(request);
                if (response.IsSuccessful)
                {
                 
                    var resultMovie = new ResultDto<T>();
                    var clientMovie = new RestClient(baseUrl);
                    var movie = response.Data.Results.FirstOrDefault(x => x.Title.ToLower().Contains(movieName));
                    if(movie is null)
                    {
                        result.Errors.Add($"Movie {movieName} not found!");
                        return Results.Ok(result);
                    }
                    var requestMovie = new RestRequest($"movie/{movie.Id}", Method.Get);
                    requestMovie.AddParameter("api_key", apiKey);
                    var responseMovie = client.Execute(requestMovie);
                    if (responseMovie.IsSuccessful)
                    {

                        result.Result = JsonConvert.DeserializeObject<MovieEntity>(responseMovie.Content);

                        return Results.Ok(result);
                    }
                }
                result.Errors.Add(response.ErrorMessage); 
            }
            return Results.Ok(result);
        }
    }
}
