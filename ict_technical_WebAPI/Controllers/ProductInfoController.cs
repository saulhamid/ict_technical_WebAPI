using ict_technical_WebAPI.Model;
using ict_technical_WebAPI.Model.Domain;
using ict_technical_WebAPI.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace ict_technical_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInfoController : ControllerBase
    {

        private readonly ict_technicalContext _context;

        public ProductInfoController(ict_technicalContext context)
        {
            _context = context;
        }

        // GET: api/product_infos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product_info>>> Getproduct_infos()
        {
            return await _context.product_info.ToListAsync();
        }
        [Route("Getproduct_infosWithUpDatePrice")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> Getproduct_infosWithUpDatePrice()
        {
            List<product_info> productdata = await _context.product_info.ToListAsync();
            List<product_price> product_pricedata = await _context.product_price.ToListAsync();
            List<campaign_info> campaign_infodata = await _context.campaign_info.ToListAsync();
            List<campaign_products> campaign_productsdata = await _context.campaign_product.ToListAsync();
            //var maxdateprice = from  pr in product_pricedata 
            //                   group pr by pr.product_id into prgrp
            //                   select new { product_id = prgrp.Key, product_price_upload_date =prgrp.Max(e=>e.product_price_upload_date) };

            var data = from product in productdata
                       join pr in product_pricedata on product.product_id equals pr.product_id
                       join mp in 
                       (
                       from pr in product_pricedata
                       group pr by pr.product_id into prgrp
                       select new { product_id = prgrp.Key, product_price_upload_date = prgrp.Max(e => e.product_price_upload_date) }
                       ) 
                       on new { pr.product_price_upload_date, pr.product_id } equals new { mp.product_price_upload_date, mp.product_id }
                       join cc in 
                       (
                       from cp in campaign_productsdata
                       join c in campaign_infodata on cp.campaign_id equals c.campaign_id
                       where c.campaign_active_status == 1 && (c.campaign_start_date <= DateTime.Now.Date && c.campaign_end_date >= DateTime.Now.Date)
                       select new {cp.product_id,c.campaign_discount }
                       ) on pr.product_id equals cc.product_id into cpgrp
                       from cp in cpgrp.DefaultIfEmpty()
                      
                       select new { product.product_id, product.product_name, DicountPrice= (pr.product_current_price),cp};


            return data.ToList();
        }
        [Route("Postproduct_infowithPrice")]
        [HttpPost]
        public async Task<ActionResult<product_info>> Postproduct_infowithPrice(ProductInfoVM pvm)
        {
            int auto_id= _context.product_info.Max(e => e.auto_id)+1;
            string product_id = _context.product_info.Max(e => (Convert.ToInt32(e.product_id) + 1)).ToString().PadLeft(5,'0');
            product_info product_Info =new product_info(){
               
                product_name =pvm.ProductName,
                product_id= product_id
            };
            _context.product_info.Add(product_Info);
             auto_id = _context.product_price.Max(e => e.auto_id) + 1;
            product_price product_Price = new product_price()
            {
                product_id = product_id,
                product_current_price=pvm.CurrentPrice,
                product_price_upload_date=DateTime.Now,
            };
            _context.product_price.Add(product_Price);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error save data");
            }
            return Ok(pvm);
        }
        private bool product_infoExists(int id)
        {
            return _context.product_info.Any(e => e.auto_id == id);
        }
    }
}
