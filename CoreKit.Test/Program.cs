﻿using System;
using System.Collections.Generic;
using CoreKit.Caching;
using CoreKit.Extension.Class;
using CoreKit.Extension.String;
using CoreKit.Extension.Collection;
using CoreKit.Connectivity.SMTP;
using System.Net.Mail;
using System.Net;

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

            var clonestring = "alohaaa";
            var cloneobject = new { foo = "bar" };
            var clonearray = new List<dynamic> { new { foo = "bar" } };
            var clonedstring = clonestring.Clone();
            var clonedobject = cloneobject.Clone();
            var clonedarray = clonearray.Clone();
            clonedstring = "bye";
            clonedobject = new { foo = "nobar" };
            clonedarray = new List<dynamic> { };

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

            // smtp
            // use Allow less secure apps: ON
            // https://stackoverflow.com/questions/20906077/gmail-error-the-smtp-server-requires-a-secure-connection-or-the-client-was-not#26709761
            using (var smtp = new SMTPKit(new SMTPKitConfiguration { EnableSSL = true, Server = "smtp.gmail.com", Port = 587, User = "tabatadzedat@gmail.com", Password = "xxx" }))
            {
                smtp.SendAsync("tabatadzedat@gmail.com", "ola", "bola").Wait();
            }

            Console.WriteLine("all CoreKit tests done...");
            Console.ReadKey();
        }
    }
}
