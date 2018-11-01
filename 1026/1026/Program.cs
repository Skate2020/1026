using _1026.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace _1026
{
    class Program
    {
        static void Main(string[] args)
        {
            Service.ImportService importService = new Service.ImportService();
            var nodes = importService.FindOpenDataFromDb("勞工保險基金國內投資股票類別");//importService.FindOpenData();
            //importService.ImportToDb(nodes);
            showOpenData(nodes);
            Console.ReadKey();

        }
        public void Configure()
        {
            string baseDir = Directory.GetCurrentDirectory();

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(baseDir, "App_Data"));
        }

        private static void showOpenData(List<OpenData> nodes)
        {

            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            nodes.GroupBy(node => node.資料集名稱).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"資料集名稱:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                });
        }
    }
}