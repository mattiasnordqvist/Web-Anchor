using System;
using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.Attributes.URL;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await WebAnchor.Api.For<ITypicode>("https://jsonplaceholder.typicode.com/").GetPost2(2);
            Console.WriteLine("hello");
        }
    }


    [BaseLocation("")]
    public interface ITypicode : IDisposable
    {
        [Get("/posts/{id}")]
        Task<Post> GetPost(int id);

        [Get("/posts/{id}")]
        Task GetPost2(int id);
    }

    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    [BaseLocation("test")]
    public interface IMyApi2 : IDisposable
    {
        [Get("/customer/{id}")]
        Task<HttpResponseMessage> GetCustomer(int id);

        [Post("/customer")]
        Task<HttpResponseMessage> CreateCustomer();
    }
}
