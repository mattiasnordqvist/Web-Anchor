using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAnchor;
using WebAnchor.ResponseParser;

namespace WebAnchorHttpConsoleTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10000; i++)
            {
                IsElasticSearchRunning();
                Console.Out.WriteLine(i);
            }
            Console.In.ReadLine();
        }

        private static bool IsElasticSearchRunning()
        {
            try
            {
                using (var api = Api.For<IElasticApi>("http://localhost:9200"))
                {
                    var posts = api.GetElasticSearchStatusReport().Result;
                    Console.Out.WriteLine(posts.Status);
                }
            }
            catch (ApiException)
            {
                return false;
            }
            return true;
        }
    }
}
