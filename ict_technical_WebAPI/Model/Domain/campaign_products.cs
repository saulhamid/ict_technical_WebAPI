using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ict_technical_WebAPI.Model.Domain
{
    [Table("campaign_products")]
    public class campaign_products
    {
        [Key]
        public int auto_id { get; set; }

        public int campaign_id { get; set; }

        public string product_id { get; set; }

    }

}
