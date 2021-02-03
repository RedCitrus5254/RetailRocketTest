using RetailRocketTest.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace RetailRocketTest
{
    public class YmlParser
    {

        private IEnumerable<Product> ParseFile(string file, Shop shop)
        {
            XDocument xDocument = XDocument.Parse(file);

            XElement catalog = xDocument.Element("yml_catalog");
            XElement shopEl = catalog.Element("shop");
            XElement offers = shopEl.Element("offers");

            List<Product> products = new List<Product>();

            foreach(var o in offers.Elements("offer"))
            {
                string name = o.Element("name").Value;
                products.Add(new Product() { Name = name, Shop = shop });
            }

            return products;
        }


        private string DownloadFile(string address)
        {
            WebClient client = new WebClient();

            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            try
            {
                string ymlFile = client.DownloadString(address);
                return ymlFile;
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Product> GetProducts(string address, Shop shop)
        {
            string ymlStr = DownloadFile(address);

            IEnumerable<Product> products = null;

            if (!string.IsNullOrWhiteSpace(ymlStr))
            {
                products = ParseFile(ymlStr, shop);
            }

            return products;
        }
    }
}
