using Newtonsoft.Json;
using Server.Domain;
using System;
using System.IO;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            Stream output;
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add("http://localhost:8881/");
            listener.Start();
            while (listener.IsListening)
            {
                var pathIndex = @"D:";
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;


                var requestPathFile = pathIndex + request.Url.LocalPath;
                var file = File.ReadAllText(requestPathFile);

                var userList = "";

                if (request.Url.LocalPath.Contains("part.html"))
                {
                    var fileJson = File.ReadAllText(@"c:\Users\Andrey\Source\Repos\librandron\Server\Server\users.json");
                    Database data = JsonConvert.DeserializeObject<Database>(fileJson);
                   
                    foreach (var user in data.users)
                    {
                        userList += $@"<li>{user.name}</li>";
                    }
                }

                var resultWithUsers = file.Replace("%participants%",userList);

                byte[] fileAsByte = System.Text.Encoding.UTF8.GetBytes(resultWithUsers);
                response.ContentLength64 = fileAsByte.Length;
                output = response.OutputStream;
                output.Write(fileAsByte, 0, fileAsByte.Length);
                output.Close();

            }

        }

    }
}