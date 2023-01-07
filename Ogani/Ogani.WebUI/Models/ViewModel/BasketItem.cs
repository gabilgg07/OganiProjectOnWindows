namespace Ogani.WebUI.Models.ViewModel
{
    public class BasketItem
    {
        //[JsonProperty("productId")]
        public int ProductId { get; set; }
        //[JsonProperty("count")]
        public int Count { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public decimal Price { get; set; }

        public decimal Amount
        {
            get
            {
                return Price * Count;
            }
        }
    }
}

