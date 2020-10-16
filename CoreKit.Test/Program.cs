using System;
using System.Collections.Generic;
using CoreKit.Caching;
using CoreKit.Extension.Class;
using CoreKit.Extension.String;
using CoreKit.Extension.Collection;
using CoreKit.Connectivity.SMTP;
using CoreKit.Connectivity.HTTP;
using CoreKit.Sync;
using System.Net.Mail;
using System.Net;
using CoreKit.Cryptography.PBKDF2;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace CoreKit.Test
{
    class Program
    {

        class Test
        {
            public string Prop { get; set; }

            public async Task DoSome()
            {
                Console.WriteLine("doing some..");
            }
            public async Task<string> GiveSome(int x)
            {
                Console.WriteLine("giving some..");
                return (x / 123).ToString();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("CoreKit test...");

            // extensions
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

            // caching
            var cache = new CacheKit(new CacheKitConfiguration { DefaultCachingMinutes = 1 });
            var val = cache.Get<string>("key");
            cache.Set("key", "ola");
            val = cache.Get<string>("key");
            val = cache.Get<string>("key");
            cache.Set("key", "ola");
            cache.Set("key", "bola");
            val = cache.Get<string>("key");
            cache.Remove("key");
            val = cache.Get<string>("key");

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
                    { "client", "aaa" },
                    { "key", "liberty-l6f845dt890d22f5566ffbab23fb012ae79348f8ba572d952b685e31" }
                }
            }))
            {
                var res = http.Request<dynamic>(
                    HTTPKitRequestMethod.GET,
                    "api/matrix/categories",
                    "/20200323.01",
                    new Dictionary<string, string>
                    {
                        { "client", "liberty" }
                    }
                );
                var x = 0;
            }

            Console.WriteLine("all CoreKit tests done...");
            Console.ReadKey();
        }
    }
}
