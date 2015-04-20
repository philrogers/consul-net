consul-net
==========

A full service .net api for the consul.io api. See the readme below for examples on how to use it.


Changes made to enhance the original version.

In Particular.
Changes to all the
var response = await Execute(request)
have been changed to 
var response = await Execute(request).ConfigureAwait(false);

This was done to stop the calls hanging from a winform. If someone thinks this is a bad idea then this should be reviewed. Various documents 
around the web dont give a definative answer however this did fix the problem of the hang.

It is now possible to connect to the client


The Initial Connection at runtime will be remembered as the default settings in properties

Connection.DefaultDataCenter   
Connection.DefaultAgent             

if you create an instance without any connection details the default values will be used. The code supports the ability to read these settings from 
your app.config file in your application. Set the defaults to your test consul server.

see example.app.config in the consul-net\src\Consul.Net.TestSuite. Paste it into your applications app.config

If this default information is not set in your app.config you will get a runtime error thats a pain to debug.


To use these default settings

Vb Code

Dim Connection = New Consul.Net.ConsulClient()
Console.WriteLine ("Current :" + Connection.CurrentDataCenter + " " + Connection.CurrentAgent)
Console.WriteLine ("Defaults :" + Connection.DefaultDataCenter + " " + Connection.DefaultAgent)

Vb Code
to Specify custom connection setting
Dim Connection = New Consul.Net.ConsulClient("10.10.10.1:8500","MYDC")
Console.WriteLine ("Current :DC=" + Connection.CurrentDataCenter + " " + Connection.CurrentAgent)
Console.WriteLine ("Defaults :DC=" + Connection.DefaultDataCenter + " " + Connection.DefaultAgent)

C#
Consul.Net.ConsulClient consulconnection = new Consul.Net.ConsulClient();
Console.WriteLine("Current :DC=" + consulconnection.CurrentDataCenter + " Agent=" + consulconnection.CurrentAgent);
Console.WriteLine("Defaults :DC=" + consulconnection.DefaultDataCenter + " Agent=" + consulconnection.DefaultAgent);

C#
to Specify custom connection setting
Consul.Net.ConsulClient consulconnection = new Consul.Net.ConsulClient("10.10.10.1:8500","MYDC");
Console.WriteLine("Current :DC=" + consulconnection.CurrentDataCenter + " Agent=" + consulconnection.CurrentAgent);
Console.WriteLine("Defaults :DC=" + consulconnection.DefaultDataCenter + " Agent=" + consulconnection.DefaultAgent);


Coding Examples


To Write a KeyValue
In VB

Dim kv As New Consul.Net.KeyValue
Dim strDataCentre as string = "mydc"
Dim strAppPath as string = "myapplications/mykey"

kv.Key = strAppPath.ToLower     
kv.Value = "Hello World"

Dim ocli= New Consul.Net.ConsulClient("10.10.10.1:8500",strDataCentre)         
           
ocli.KeyValuePut(kv, 0, Nothing, Nothing, strDataCentreName)


C#

 var kv = new KeyValue();
 kv.Key = "myapplications/mykey";
 kv.Value = "something";
 client.KeyValuePut(kv,0,null,null,"mydc");
 or
 client.KeyValuePut(kv);  // to use current dc that is set


