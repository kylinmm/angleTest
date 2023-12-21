using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Xml;
using angleTest;

namespace 天气预报
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            GetTXCloudDoc test = new GetTXCloudDoc();
            var a=test.getTXDocWeb().Result;
        }
       
    }
}

