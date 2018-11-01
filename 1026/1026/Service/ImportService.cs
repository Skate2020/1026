using _1026.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Service
{
    class ImportService
    {
        public Repository.OpenDataRepository _repository;

        public ImportService()
        {
            _repository = new Repository.OpenDataRepository();
        }

        public List<OpenData> FindOpenData()
        {
            List<OpenData> result = new List<OpenData>();

            string baseDir = Directory.GetCurrentDirectory();


            var xml = XElement.Load(@"C:\Users\user\Desktop\軟體工程\1026\1026\1026\Data\datagovtw_dataset_20181101.xml");


            //XNamespace gml = @"http://www.opengis.net/gml/3.2";
            //XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";
            var nodes = xml.Descendants("node").ToList();

            result = nodes
                .Where(x => !x.IsEmpty).ToList()
                .Select(node =>
                {
                    OpenData item = new OpenData();
                    item.id = int.Parse(getValue(node, "id"));
                    item.資料集名稱 = getValue(node, "資料集名稱");
                    item.服務分類 = getValue(node, "服務分類");
                    item.主要欄位說明 = getValue(node, "主要欄位說明");
                    return item;
                }).ToList();
            return result;
        }

        public List<OpenData> FindOpenDataFromDb(string name)
        {
            return _repository.SelectAll(name);
        }

        public void ImportToDb(List<OpenData> openDatas)
        {
            //Repository.OpenDataReository Repository = new Repository.OpenDataReository();
            //openDatas.ForEach(item =>
            //{
            //    Repository.Insert(item);
            //});
            openDatas.ForEach(item =>
            {
                _repository.Insert(item);
            });
        }

        private string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }
    }
}