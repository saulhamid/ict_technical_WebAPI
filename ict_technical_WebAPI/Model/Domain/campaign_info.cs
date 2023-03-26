using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ict_technical_WebAPI.Model.Domain
{
    [Table("campaign_info")]
    public class campaign_info
    {
        [Key]
        public int campaign_id { get; set; }

        public string campaign_name { get; set; }

        public int? campaign_discount { get; set; }

        public DateTime? campaign_start_date { get; set; }

        public DateTime? campaign_end_date { get; set; }

        public int? campaign_active_status { get; set; }

    }
}
