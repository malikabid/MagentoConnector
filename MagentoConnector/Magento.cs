using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace MagentoConnector
{
    class Magento
    {

        private RestClient Client { get; set; }
        private string Token { get; set; }

        public Magento(string magentoUrl)
        {
            Client = new RestClient(magentoUrl);

        }


        public Magento(string magentUrl, string token)
        {
            Client = new RestClient(magentUrl);

        }

        public string getAdminToken(string userName, string passWord)
        {
            var request = CreateRequest("/rest/V1/integration/admin/token", Method.POST);
            var user = new Credentials();
            user.username = userName;
            user.password = passWord;

            string json = JsonConvert.SerializeObject(user, Formatting.Indented);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = Client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            return response.ErrorMessage;
        }

        private RestRequest CreateRequest(string endPoint, Method method)
        {
            var request = new RestRequest(endPoint, method);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        private RestRequest CreateRequest(string endPoint, Method method, string token)
        {
            var request = new RestRequest(endPoint, method);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/json");

            return request;
        }


        public string GetProduct(string sku, string token)
        {
            var request = CreateRequest("rest/V1/products/" + sku,Method.GET,token);
            var response = Client.Execute(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ToString();
            }

            return response.ErrorMessage;
        }

        public string UpdatePrice(string sku, string priceValue, string token)
        {
            var mageProduct = new MageProduct();
            var product = new Product();
            product.Sku = sku;
            product.Price = priceValue;

            mageProduct.Product = product;

            string json = JsonConvert.SerializeObject(mageProduct, Formatting.Indented);

            var request = CreateRequest("rest/V1/products/" + sku, Method.PUT, token);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return response.Content.ToString();
                return "Price Update Successfull";
            }

            return response.ErrorMessage;
        }


        public string UpdateStock(string sku, string qtyValue, string token)
        {
            var extensionAttributes = new ExtensionAttributes();
            var stockItem = new StockItem();

            stockItem.Qty = Convert.ToInt32(qtyValue);
            stockItem.IsInStock = Convert.ToInt32(qtyValue) > 0 ? true : false;

            extensionAttributes.StockItem = stockItem;

            string json = JsonConvert.SerializeObject(extensionAttributes, Formatting.Indented);

            var request = CreateRequest("rest/V1/products/" + sku + "/stockItems/1", Method.PUT, token);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return response.Content.ToString();
                return "Stock Update Successfull";
            }

            return response.ErrorMessage;
        }

        public string UpdateStockAndPrice(string sku, string qtyValue, string priceValue, string token)
        {

            var mageProduct = new MageProduct();
            var product = new Product();

            var extensionAttributes = new ExtensionAttributes();
            var stockItem = new StockItem();

            stockItem.Qty = Convert.ToInt32(qtyValue);
            stockItem.IsInStock = Convert.ToInt32(qtyValue) > 0 ? true : false;

            extensionAttributes.StockItem = stockItem;

            product.Sku = sku;
            product.Price = priceValue;
            product.ExtensionAttributes = extensionAttributes;

            mageProduct.Product = product;

            string json = JsonConvert.SerializeObject(mageProduct, Formatting.Indented);

            var request = CreateRequest("rest/V1/products/" + sku, Method.PUT, token);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return response.Content.ToString();
                return "Stock and Price Update Successfull";
            }

            return response.ErrorMessage;

        }

        public string CreateProduct(string sku, string name, string weightValue, string priceValue, string qtyValue,string suitableCar, string suitableYear, string token)
        {


            var mageProduct = new MageProduct();
            var product = new Product();

            var suitable_car = new CustomAttribute();
            suitable_car.AttributeCode = "suitable_car";
            suitable_car.Value = "ALTIMA";

            var suitable_year = new CustomAttribute();
            suitable_year.AttributeCode = "suitable_year";
            suitable_year.Value = "2010-2015";

            List<CustomAttribute> CustomAttributes = new List<CustomAttribute>();
            CustomAttributes.Add(suitable_car);
            CustomAttributes.Add(suitable_year);



            var extensionAttributes = new ExtensionAttributes();
            var stockItem = new StockItem();

            stockItem.Qty = Convert.ToInt32(qtyValue);
            stockItem.IsInStock = Convert.ToInt32(qtyValue) > 0 ? true : false;

            extensionAttributes.StockItem = stockItem;

            product.Sku = sku;
            product.Name = name;
            product.Weight = weightValue;
            product.Status = "1";
            product.TypeId = "simple";
            product.Visibility = "4";
            product.AttributeSetId = "4";

            product.Price = priceValue;
            product.ExtensionAttributes = extensionAttributes;

            product.CustomAttributes = CustomAttributes;

            mageProduct.Product = product;

            string json = JsonConvert.SerializeObject(mageProduct, Formatting.Indented);

            var request = CreateRequest("rest/V1/products", Method.POST, token);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return response.Content.ToString();
                return "Product Creation Successfull";
            }

            return response.ErrorMessage;
        }
    }
}
