using CoreKit.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoreKit.Test
{
    class TestCoreKitCaching
    {

        public void Test()
        {

            Console.WriteLine("\n\n\n***** testing CacheKit *****");
            Console.WriteLine();
            var cache = default(CacheKit);

            Console.WriteLine("... no cache");

            cache = new CacheKit();
            cache.Clear();

            cache.Set("one", "one");
            Console.WriteLine(cache.Get<string>("one") == null);

            cache.Set("one", "one", 1);
            Console.WriteLine(cache.Get<string>("one") == "one");

            cache = new CacheKit(new CacheKitConfiguration { });
            cache.Clear();

            cache.Set("one", "one");
            Console.WriteLine(cache.Get<string>("one") == null);

            cache.Set("one", "one", 1);
            Console.WriteLine(cache.Get<string>("one") == "one");

            Thread.Sleep(60001);
            Console.WriteLine(cache.Get<string>("one") == null);

            Console.WriteLine();
            Console.WriteLine("***** testing CacheKit *****");


        }

    }
}
