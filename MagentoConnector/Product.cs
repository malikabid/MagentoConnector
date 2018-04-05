using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoConnector
{
    class StockItem
    {
       /* [JsonProperty("item_id")]
        public int ItemId { get; set; }

        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("stock_id")]
        public int StockId { get; set; }*/

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("is_in_stock")]
        public bool IsInStock { get; set; }
      
    }

    class ExtensionAttributes
    {

        [JsonProperty("stock_item")]
        public StockItem StockItem { get; set; }

    }

    class CustomAttributes
    {

    }

    public class CustomAttribute
    {

        [JsonProperty("attribute_code")]
        public string AttributeCode { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }

    class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("attribute_set_id")]
        public string AttributeSetId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("type_id")]
        public string TypeId { get; set; }


        [JsonProperty("extension_attributes")]
        public ExtensionAttributes ExtensionAttributes { get; set; }

        [JsonProperty("custom_attributes")]
        public IList<CustomAttribute> CustomAttributes { get; set; }

    }

    class MageProduct
    {
        [JsonProperty("product")]
        public Product Product { get; set; }
    }
}
