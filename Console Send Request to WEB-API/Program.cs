using System;
using System.IO;
using System.Web;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Console_Send_Request_to_WEB_API
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static MultipartFormDataContent dataContent = new MultipartFormDataContent();
        static string folder = @"C:\Users\faranam\Desktop\Files";

        public static void Post(MultipartFormDataContent httpContent)
        {
            var message = client.PostAsync($"post", httpContent);
            Console.WriteLine(message.Result.ToString());
        }

        public static void GetFiles()
        {
            if (Directory.Exists(folder))
            {
                ByteArrayContent byteContent;
                var files = Directory.EnumerateFiles(folder);

                foreach (var item in files)
                {

                    var file = File.ReadAllBytes(item);
                    byteContent = new ByteArrayContent(file);
                    byteContent.Headers.Add("checksum", item.Split('\\').Last());
                    dataContent.Add(byteContent);
                    HttpPostedFile[] postedFiles = new HttpPostedFile[3];

                }
                Post(dataContent);
            }
            else
                Console.WriteLine("Faild!");
        }

        static void Main(string[] args)
        {

            client.BaseAddress = new Uri("https://localhost:44392/api/values/");

            //Post("itsWorking!!");
            GetFiles();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
