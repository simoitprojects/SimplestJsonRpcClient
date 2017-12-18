## SimplestJsonRpcClient -- Very simple JSON RPC C# client library

This library isn't meant for production use but more for POC projects 
It's very easy to use and debug 

In SimplestJsonRpcClient_test folder you will find a working console client


Usage examples: 
Simple
```C#
using SimoItProjects;

private async Task Test()
{
        SimplestJsonRpcClient client;
        // url - your JSON RPC server url
        // user, password - credentials (optional)
        client = new SimplestJsonRpcClient("http://127.0.0.1:5000", "user", "password");
        
        // PostRaw - this method takes a JSON string as argument and returns a raw JSON string response from server
        string response1 = await client.PostRaw("{ \"method\":\"getinfo\",\"params\":[]}");
        Console.WriteLine(response1);
        
        // Post - this method takes a JSON string as argument and returns parsed object from server response 
        dynamic response2 = await client.Post("{ \"method\":\"getinfo\",\"params\":[]}");
        Console.WriteLine(response2.result[0].info);     
}
```

Advanced(with debugging): 
```C#
using SimoItProjects;

private async Task Test()
{
    //Wrap all in try to handle exceptions
    try
    {
        SimplestJsonRpcClient client;
        client = new SimplestJsonRpcClient("http://127.0.0.1:5000", "user", "password");
        
        //This will enable debug log for all request and responses 
        // 2nd argument defines path that will contain log files like SimplestJsonRpcClient.log
        client.Debug(true,@"C:\Test");
        
        string response1 = await client.PostRaw("{ \"method\":\"getinfo\",\"params\":[]}");
        Console.WriteLine(response1);

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
     
}
```
