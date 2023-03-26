using System.ComponentModel.DataAnnotations;

namespace ict_technical_WebAPI.Model.Domain
{
    public class product_info
    {
        [Key]
        public int auto_id { get; set; }

        public string product_id { get; set; }

        public string product_name { get; set; }

    }

}
