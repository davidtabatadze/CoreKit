using CoreKit.Caching;
using CoreKit.Connectivity.HTTP;
using CoreKit.Connectivity.SMTP;
using CoreKit.Cryptography.PBKDF2;
using CoreKit.Extension;
using CoreKit.Extension.Class;
using CoreKit.Extension.Collection;
using CoreKit.Extension.String;
using CoreKit.Sync;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace CoreKit.Test
{
    class Program
    {

        class post
        {
            public string Code { get; set; }
            public string Merchant { get; set; }
            public string Mr { get; set; }
        }
        class get
        {
            public string Mr { get; set; }
        }
        class Test
        {

            public string Version { get; set; }
            public string Type { get; set; }

            public string Prop { get; set; }

            public DateTime Prop2 { get; set; }

            public async Task DoSome()
            {
                Console.WriteLine("doing some..");
            }
            public async Task<string> GiveSome(int x)
            {
                Console.WriteLine("giving some..");
                return (x / 123).ToString();
            }

            public void CheckCache(CacheKit cache)
            {
                var x = cache.Get<string>("x");
                var y = cache.Get<string>("y");
                var z = cache.Get<string>("z");
                var o = 0;
            }

            public void HTTP()
            {
                using (var http = new HTTPKit(new HTTPKitConfiguration
                {
                    UsePascalNaming = true,
                    ServiceURL = "http://localhost:5000/",
                    Headers = new Dictionary<string, string> {
                        { "client", "liberty" },
                        { "key", "liberty-l6f84591890d22f5566ffbab23fb012ae79348f8ba572d952b685e31" }//"liberty-l6f845dt890d22f5566ffbab23fb012ae79348f8ba572d952b685e31" }
                    }
                }))
                {

                    //var dddv = JsonConvert.DeserializeObject<Versionables<CategorizationTransactionCloud>>("{"version":"20200323.01","data":[{"code":"dt","contextData":null,"location":null,"terminal":"dt","categoryOrigin":"merchant","categoryCode":110102,"categoryName":"Fuel & Gas","categoryParentCode":110100,"categoryParentName":"Auto & Transport","typeCode":11,"typeName":"Spending","merchant":"Wissol"}]}");


                    // var result = http.Request<Versionables<CategorizationTransactionResponse>>(
                    //    HTTPKitRequestMethod.POST,
                    //    "api/categorize",
                    //    new List<CategorizationTransactionCloud>
                    //    {
                    //        new CategorizationTransactionCloud{
                    //            Code = "dt",
                    //            State = "dt",
                    //            Date = DateTime.Now,
                    //            Currency = "dt",
                    //            Amount = -1,
                    //            MCC = 672,
                    //            Merchant = "wissol",
                    //            Location = "dt",
                    //            Terminal = "dt"
                    //        }
                    //    }
                    //);
                    var x = 0;
                }
            }

            private void do1()
            {
                Console.WriteLine("doo....");
            }
            private void do2(string job)
            {
                Console.WriteLine("done " + job);
            }
            private double do3(int x, double y)
            {
                return x + y;
            }
            private async Task do4(int x, int y)
            {
                await Task.Run(() => { Console.WriteLine(x + y); });
            }

        }

        private class Response
        {

            /// <summary>
            /// 
            /// </summary>
            public class ResponseData
            {

                /// <summary>
                /// 
                /// </summary>
                [JsonPropertyName("messageId")]
                public string MessageId { get; set; }

                /// <summary>
                /// 
                /// </summary>
                [JsonPropertyName("statusId")]
                public int StatusId { get; set; }

            }

            /// <summary>
            /// 
            /// </summary>
            //[JsonPropertyName("data")]
            public List<ResponseData> data { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

        }



        private class SendResponse
        {

            /// <summary>
            /// 
            /// </summary>
            public string RequestID { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public object ErrorResult { get; set; }

        }

        class CLSCLS
        {
            public string Version { get; set; }
        }

        private class SIM
        {
            public List<List<decimal>> Similarities { get; set; }
        }

        private static void olaa()
        {
            var similarities = new List<ulong> { };

            try
            {

                using (var client = new HTTPKit(new HTTPKitConfiguration
                {
                    UsePascalNaming = true,
                    SuccessIs200Only = true,
                    ServiceURL = "http://localhost:5000/",
                    Headers = new Dictionary<string, string>
                {
                    { "key", "tera-t4c67c04a75dt8786f3f773e9393a9ee40e412442e5877e4e6e7b41a02a" }
                }


                }))
                {

                    //var h = client.Request<List<Test>>(HTTPKitRequestMethod.GET, "api/v2/catalog/types/oook");
                    //var m = client.Request<List<Test>>(HTTPKitRequestMethod.GET, "api/v2/catalog/types");

                    var x = client.Request<List<Test>>(HTTPKitRequestMethod.GET, "api/v2/rule/extractions");
                    //var y = client.Request<List<post>>(HTTPKitRequestMethod.POST, "api/v2/categorize",
                    //  new List<post> { new post { Code = "dt", Merchant = "wissol" } });

                    var dddddddd = 0;

                    //var attampts = 2;
                    //client.Timeout = TimeSpan.FromSeconds(2);
                    //client.BaseAddress = new Uri("http://localhost:5000/agent/v2/rule/");

                    //for (int i = 1; i <= attampts; i++)
                    //{
                    //    var request = client.GetAsync("extractions").Result;
                    //    var response = request.Content.ReadAsStringAsync().Result;
                    //    if (request.IsSuccessStatusCode)
                    //    {
                    //        var data = JsonSerializer.Deserialize<Test>(response);
                    //        //similarities = data.Similarities.Select(s => Convert.ToUInt64(s[0])).ToList();
                    //        break;
                    //    }
                    //}

                }

                //using (var client = new HttpClient())
                //{

                //    var attampts = 2;
                //    client.Timeout = TimeSpan.FromSeconds(2);
                //    //client.BaseAddress = new Uri("http://localhost:8080/similarproducts/");
                //    client.BaseAddress = new Uri("http://172.30.111.201/similarproducts/");

                //    for (int i = 1; i <= attampts; i++)
                //    {
                //        var request = client.GetAsync("123456", new CancellationTokenSource(TimeSpan.FromSeconds(1)).Token).Result;
                //        var response = request.Content.ReadAsStringAsync().Result;
                //        if (request.IsSuccessStatusCode)
                //        {
                //            var data = JsonSerializer.Deserialize<SIM>(response);
                //            similarities = data.Similarities.Select(s => Convert.ToUInt64(s[0])).ToList();
                //            break;
                //        }
                //    }

                //}

            }
            catch (Exception exception)
            {
                var ee = 0;
                //_logger.Error(ex.Message, ex);
            }

            if (similarities.Count > 0)
            {
            }
        }

        private static async Task<string> GetCacheData()
        {
            return await Task.Run(() => { return "data"; });
        }
        private static string GetCacheDataSync()
        {
            return "data";
        }

        static async Task Main(string[] args)
        {
            try
            {

                Console.WriteLine("\n\n\n go");

                //
                var cache0 = new CacheKit();
                var cache1 = new CacheKit(new CacheKitConfiguration { });
                var cacheVal = string.Empty;

                var vvv = cache1.Get("xxx", () => GetCacheDataSync(), null);

                cache1.Set("cache", null);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("01 " + cacheVal.IsEmpty());

                cache1.Set("cache", "d", null);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("02 " + cacheVal.IsEmpty());

                cacheVal = await cache1.Get("cache", () => GetCacheData(), null);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("03 " + cacheVal.IsEmpty());

                cache1.Set("cache", null, 1);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("04 " + cacheVal.IsEmpty());

                cache1.Set("cache", "d", 1);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("05 " + cacheVal.HasValue());

                cacheVal = await cache1.Get("cache", () => GetCacheData(), 1);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("06 " + cacheVal.HasValue());

                Console.WriteLine("wait 30 sec");
                Thread.Sleep(30 * 1001);
                Console.WriteLine("07 " + cache1.Get<string>("cache").HasValue());

                Console.WriteLine("wait 30 sec");
                Thread.Sleep(30 * 1001);
                Console.WriteLine("08 " + cache1.Get<string>("cache").IsEmpty());

                cache1.Set("test0", "test0", 1);
                cache1.Remove("test0");
                Console.WriteLine("09 " + cache1.Get<string>("test0").IsEmpty());
                cache1.Set("test1", "test1", 1);
                cache1.Set("test2", "test2", 1);
                cache1.Clear();
                Console.WriteLine("10 " + cache1.Get<string>("test1").IsEmpty());
                Console.WriteLine("11 " + cache1.Get<string>("test2").IsEmpty());

                cache1.Set("cache", "d");
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("12 " + (cacheVal == "d"));

                cache1.Set("cache", "datiko");
                cacheVal = await cache1.Get("cache", () => GetCacheData());
                Console.WriteLine("13 " + (cacheVal != "data"));

                Console.WriteLine("wait 61 sec");
                Thread.Sleep(61 * 1001);
                cacheVal = cache1.Get<string>("cache");
                Console.WriteLine("14 " + (cacheVal == "datiko"));

                cache1 = new CacheKit(new CacheKitConfiguration { DefaultCachingMinutes = 1 });
                cache1.Set("cache", "d");
                Console.WriteLine("16 " + (cache1.Get<string>("cache") == "d"));

                Console.WriteLine("wait 61 sec");
                Thread.Sleep(61 * 1001);
                Console.WriteLine("17 " + cache1.Get<string>("cache").IsEmpty());

                Console.WriteLine("wait 61 sec");
                cacheVal = await cache1.Get("cache", () => GetCacheData(), 0);
                Thread.Sleep(61 * 1001);
                Console.WriteLine("18 " + (cache1.Get<string>("cache") == "data"));


                Console.WriteLine("ok");
                Console.ReadKey();
                //


                using (var http = new HTTPKit(new HTTPKitConfiguration
                {
                    ServiceURL = "https://api.discovery.optio.ai/",
                    UseWebProxy = false,
                    WebProxyURL = "---",
                    IncludeRawResponse = true,
                    Headers = new Dictionary<string, string>
                {
                    { "client", "liberty" },
                    { "key", "liberty-l6f845dt890d22f5566ffbab23fb012ae79348f8ba572d952b685e31" }
                }
                }))
                {
                    var tasks = new List<Task> { };

                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine("Connect");
                        tasks.Add(Task.Run(async () =>
                        {

                            var res = await http.RequestAsync<Test>(
                                HTTPKitRequestMethod.GET,
                                "api/matrix/categories"
                            );

                            if (res.Error == false)
                            {
                                Console.WriteLine("ook =================");
                            }
                            else
                            {
                                Console.WriteLine(res.ErrorText);
                            }

                        }));
                    }

                    var exec = Task.WhenAll(tasks);
                    try
                    {
                        await exec;
                    }
                    catch { }
                    if (exec.Status == TaskStatus.Faulted)
                    {
                        Console.WriteLine(exec.Exception.Message);
                    }
                }



                Console.WriteLine("Done");
                Console.ReadKey();
                return;








                using (var http = new HTTPKit(new HTTPKitConfiguration
                {
                    ServiceURL = "https://api.discovery.optio.ai/",
                    UseWebProxy = false,
                    WebProxyURL = "---",
                    IncludeRawResponse = true,
                    Headers = new Dictionary<string, string>
                {
                    { "client", "liberty" },
                    { "key", "liberty-l6f845dt890d22f5566ffbab23fb012ae79348f8ba572d952b685e31" }
                }
                }))
                {
                    var res = http.Request<Test>(
                        HTTPKitRequestMethod.GET,
                        "api/matrix/categories"
                    //"merchants/chart",
                    //new
                    //{
                    //    DateFrom = DateTime.Now,
                    //    Merchant = "dodolina",
                    //    PageSkip = 666
                    //},
                    //"/20200323.01",
                    //new Dictionary<string, string>
                    //{
                    //{ "client", "liberty" }
                    //}
                    );
                    var x = 0;
                }
                using (var http = new HTTPKit(new HTTPKitConfiguration { ServiceURL = "https://sender.ge/", IncludeRawResponse = true }))
                {
                    var url = string.Format(
                        "api/send.php?apikey={0}&smsno=2&destination={1}&content={2}",
                        "c37152d64f60ac6e563f67882583d7a3",
                        "599438038",
                        "aloooha"
                    );
                    var response = http.Request<Response>(HTTPKitRequestMethod.GET, url);

                    var x = 10;
                }
                return;

                Console.WriteLine("CoreKit test...");

                // extensions
                var rand1 = StringKit.Random(10);
                var rand2 = StringKit.Random(20);
                var rand3 = StringKit.Random(30);
                var str = "j  a     b  C         ";
                string str1 = null;
                var strIsEmpty = str.IsEmpty();
                var strHasValue = str.HasValue();
                var strTrimFull = str.TrimFull();
                var strTrimFullL = str1.TrimFullAndLower();
                var strTrimFullU = str.TrimFullAndUpper();
                var strTrim = str.Trim();
                var strTrimChar = str.Trim('j');
                var col = new List<string> { };
                var colIsEmpty = col.IsEmpty();
                var colHasValue = col.HasValue();
                var pascal = str.ToPascal();
                var dromedary = pascal.ToDromedary();

                var clonestring = "alohaaa";
                var cloneobject = new { foo = "bar" };
                var clonearray = new List<dynamic> { new { foo = "bar" } };
                var clonedstring = clonestring.Clone();
                var clonedobject = cloneobject.Clone();
                var clonedarray = clonearray.Clone();
                clonedstring = "bye";
                clonedobject = new { foo = "nobar" };
                clonedarray = new List<dynamic> { };

                var sha1 = "dat".ToSHA1();
                var sha2 = "dat".ToSHA1();
                var sha3 = "aiwdgwid iuawgd awgdi agid awdiugaw iawidia awdwi aiwdiaiw iaw idgaiwdiawgge g ag iaie ".ToSHA1();
                var sha4 = "aiwdgwid iuawgd awgdi agid awdiugaw+23 iawidia awdwi aiwdiaiw iaw idgaiwdiawgge g ag iaie ".ToSHA1();

                var arraycls = new List<Test> { null };
                var arrayint = new List<int?> { 0, 1, 2, null };
                var arraylon = new List<long> { 0, 0, 999 };
                var arraystr = new List<string> { "aaa", " ", "", null };
                var arrayflt = new List<double> { 0.1, 0.0 };


                var x1 = arraycls.TrimEmpty().HasValue();
                var x2 = arrayint.TrimEmpty().HasValue();
                var x22 = arrayint.TrimEmptyOrLTE0().HasValue();
                var x3 = arraylon.TrimEmptyOrLTE0().HasValue();
                var x4 = arraystr.TrimEmpty().HasValue();
                var x5 = arrayflt.TrimEmptyOrLTE0().HasValue();

                var cls = new Test { };
                cls.InvokeNonPublic("do1", null);
                cls.InvokeNonPublic("do2", new[] { "dodolina" });
                Console.WriteLine(cls.InvokeNonPublic<Test, double>("do3", new object[] { 1, 665.666 }));
                cls.InvokeNonPublicAsync("do4", new object[] { 1, 665 }).Wait();


                var f1 = FileKit.TryReadAllText(".\\txt.txt");
                var f2 = FileKit.TryReadAllText(".\\txt.txt", "olaaa");
                var f3 = FileKit.TryReadAllText(".\\txtx.txt");
                var f4 = FileKit.TryReadAllText(".\\txtx.txt", "olaaa");

                // caching
                var cache = new CacheKit(new CacheKitConfiguration { DefaultCachingMinutes = 1 });
                var val = cache.Get<string>("key");
                cache.Set("key", "ola", 5);
                val = cache.Get<string>("key");
                val = cache.Get<string>("key");
                cache.Set("key", "ola");
                cache.Set("key", "bola");
                val = cache.Get<string>("key");
                cache.Remove("key");
                val = cache.Get<string>("key");

                cache.Set("x", "x");
                cache.Set("y", "y", 0);
                cache.Get("z", () => new Test().GiveSome(999), 0).Wait();

                // cryptography
                using (var pbkdf2 = new PBKDF2Kit())
                {
                    var pass1 = "datiko";
                    var salt1 = pbkdf2.Salt;
                    var hash1 = pbkdf2.GenerateHash(pass1, salt1);

                    var pass2 = "datka";
                    var salt2 = pbkdf2.Salt;
                    var hash2 = pbkdf2.GenerateHash(pass2, salt2);

                    var compare1 = pbkdf2.Compare(pbkdf2.GenerateHash(pass1, salt1), hash1);
                    var compare2 = pbkdf2.Compare(pbkdf2.GenerateHash(pass2, salt2), hash2);
                    var compare3 = pbkdf2.Compare(pbkdf2.GenerateHash("aaa", salt1), hash1);
                    var compare4 = pbkdf2.Compare(pbkdf2.GenerateHash("bbb", salt2), hash2);
                }

                // smtp
                // use Allow less secure apps: ON
                // https://stackoverflow.com/questions/20906077/gmail-error-the-smtp-server-requires-a-secure-connection-or-the-client-was-not#26709761
                using (var smtp = new SMTPKit(new SMTPKitConfiguration
                {
                    EnableSSL = false,
                    Server = "smtp.mailmock.io",
                    Port = 587,
                    Sender = "optio.insight@lb.ge", //"test-mailbox@optio.ai",
                                                    //User = "dec8ea39-5bea-39a9-d9c7-0ff36680222a",
                                                    //Password = "e03a72e3-75aa-5aa3-09b5-008ce0053854"
                }))
                {
                    // smtp.SendAsync("tabatadzedat@gmail.com", "ola", "bola").Wait();
                }

                // sync
                var stest = new Test { };
                SyncKit.Run(() => stest.DoSome());
                var sync1 = SyncKit.Run(() => stest.GiveSome(1735172));

                // http
                using (var http = new HTTPKit(new HTTPKitConfiguration
                {
                    ServiceURL = "https://api.discovery.optio.ai/",
                    UseWebProxy = false,
                    WebProxyURL = "---",
                    Headers = new Dictionary<string, string>
                {
                    { "client", "liberty" },
                    { "key", "liberty-l6f845dt890d22f5566ffbab23fb012ae79348f8ba572d952b685e31" }
                }
                }))
                {
                    var res = http.Request<dynamic>(
                        HTTPKitRequestMethod.GET,
                        "api/matrix/categories",
                        "/20200323.01"
                    );
                    var x = 0;
                }

                Console.WriteLine("All CoreKit tests done...");

                Console.WriteLine("\n\nNow possible to check cache...");
                while (true)
                {
                    Console.ReadLine();
                    new Test().CheckCache(cache);
                }

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n" + ex.Message);
                Console.ReadKey();
            }
        }

    }
}
