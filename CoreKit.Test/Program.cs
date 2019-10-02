using System;
using System.Collections.Generic;
using CoreKit.Extensions.String;
using CoreKit.Extensions.Collection;
using CoreKit.Caching;

namespace CoreKit.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CoreKit test...");

            // extensions
            var str = "a     b  c         ";
            var strIsEmpty = str.IsEmpty();
            var strHasValue = str.HasValue();
            var strTrimFull = str.TrimFull();
            var col = new List<string> { };
            var colIsEmpty = col.IsEmpty();
            var colHasValue = col.HasValue();

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


            Console.WriteLine("all CoreKit tests done...");
            Console.ReadKey();
        }
    }
}
