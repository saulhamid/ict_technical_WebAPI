using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ict_technical_WebAPI.Model.Domain
{
    [Table("product_price")]
    public class product_price
    {
        [Key]
        public int auto_id { get; set; }

        public string product_id { get; set; }

        public double? product_current_price { get; set; }

        public DateTime? product_price_upload_date { get; set; }

        public string remarks { get; set; }

    }
}
