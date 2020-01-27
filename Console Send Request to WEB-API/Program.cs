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
        #region Fields
        /// <summary>
        /// HttpClient For Sending Request
        /// </summary>
        static HttpClient client = new HttpClient();

        /// <summary>
        /// MultipartFormDataContent For Upload Multiple Files to the Web-API
        /// </summary>
        static MultipartFormDataContent dataContent = new MultipartFormDataContent();

        /// <summary>
        /// Path of Input Files Main Directory
        /// </summary>
        static string folder =
            @"C:\Users\faranam\Desktop\Files";

        #endregion

        #region Methods
        /// <summary>
        /// Post Function that Sending Values to the Web-API
        /// </summary>
        /// <param name="httpContent"></param>
        public static void Post(MultipartFormDataContent httpContent)
        {
            var message = 
                client.PostAsync($"post", httpContent);

            Console.WriteLine(message.Result.ToString());
        }

        /// <summary>
        /// GetFiles Function For Getting Files and Place them at MultipartFormDataContent 
        /// </summary>
        public static void GetFiles()
        {
            if (Directory.Exists(folder))
            {
                ByteArrayContent byteContent;

                var files =
                    Directory.EnumerateFiles(folder);

                foreach (var item in files)
                {
                    var file = 
                        File.ReadAllBytes(item);

                    byteContent = 
                        new ByteArrayContent(file);

                    byteContent.Headers.Add
                        ("checksum", item.Split('\\').Last());

                    dataContent.Add(byteContent);
                }
                Post(dataContent);
            }
            else
                Console.WriteLine("Failed!");
        }
        #endregion

        #region Main
        /// <summary>
        /// Main Funtion, Main Function Will Run After Program Starts
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ///URI
            client.BaseAddress =
                new Uri("https://localhost:44392/api/values/");

            GetFiles();

            Console.WriteLine("Finish");
            Console.ReadKey();
        }
        #endregion
    }
}
