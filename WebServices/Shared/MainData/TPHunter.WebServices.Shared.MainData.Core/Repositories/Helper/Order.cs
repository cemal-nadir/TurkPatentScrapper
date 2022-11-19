namespace TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper
{
    /// <summary>
    /// Tablo verilerinde kolon adı, yön bazlı sıralama yapılmasını sağlayan model
    /// </summary>
    public class Order
    {
        public string OrderColumnName { get; set; }
        public OrderDirection OrderColumnDirection { get; set; }
        /// <summary>
        /// Varsayılan Order Değeri Atamasını Yapar (CreateTime'a göre büyükten küçüğe sıralama)
        /// </summary>
        /// <returns></returns>
        public Order()
        {
            OrderColumnName = nameof(EntityBase.CreateTime);
            OrderColumnDirection = OrderDirection.Desc;
        }
    }
}
