using SimoItProjects;
using System;
using System.Threading.Tasks;

namespace SimplestJsonRpcClient_test
{
    class Program
    {

        private async Task Test()
        {
            try
            {
                SimplestJsonRpcClient client;
                client = new SimplestJsonRpcClient("http://127.0.0.1:5000", "user", "password");
                string response1 = await client.PostRaw("{ \"method\":\"getinfo\",\"params\":[]}");
                Console.WriteLine(response1);

                dynamic response2 = await client.Post("{ \"method\":\"getinfo\",\"params\":[]}");
                Console.WriteLine(response2.result[0].info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
             
        }

        static void Main(string[] args)
        {
            var task = Task.Run(async () =>
            {
                await new Program().Test();
            });
            task.Wait();
        }
    }
}
