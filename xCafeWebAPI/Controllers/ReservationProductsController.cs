using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xCafeWebAPI.Models;

namespace xCafeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationProductsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ReservationProductsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/ReservationProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationProduct>>> GetReservationProducts()
        {
            return await _context.ReservationProducts.ToListAsync();
        }


        // GET: api/ReservationProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationProduct>> GetReservationProduct(int id)
        {
            var reservationProduct = await _context.ReservationProducts.FindAsync(id);

            if (reservationProduct == null)
            {
                return NotFound();
            }

            return reservationProduct;
        }
        //Get Reservation Products by ReservationID (GetRPbyRID)
        // GET: api/ReservationProducts/5
        [HttpGet("{id}")]
        [Route("GetRPbyRID/{id}")]
        public async Task<ActionResult<IEnumerable<ReservationProductJoin>>> GetRPbyRID(int id)
        {
            var reservationProducts = (from resPro in _context.ReservationProducts
                                       join pro in _context.Products on resPro.ProductID equals pro.ProductID
                                       where resPro.ReservationID == id
                                       select new ReservationProductJoin(resPro.ReservationProductID,
                                       resPro.Quantity,
                                       resPro.TotalPrice,
                                       pro.ProductID,
                                       pro.Name,
                                       pro.Price,
                                       pro.ImagePath)).ToListAsync();
            if (reservationProducts == null)
            {
                return NotFound();
            }
            return await reservationProducts;
        }

        [HttpPost]
        [Route("UpdateReservationProduct")]
        public async Task<IActionResult> UpdateReservationProduct(ReservationProduct reservationProduct)
        {
            _context.Entry(reservationProduct).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        /*
        // PUT: api/ReservationProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationProduct(int id, ReservationProduct reservationProduct)
        {
            if (id != reservationProduct.ReservationProductID)
            {
                return BadRequest();
            }

            _context.Entry(reservationProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */

        // POST: api/ReservationProducts
        [HttpPost]
        [Route("PostReservationProduct")]
        public async Task<ActionResult<ReservationProduct>> PostReservationProduct(ReservationProduct reservationProduct)
        {
            _context.ReservationProducts.Add(reservationProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationProduct", new { id = reservationProduct.ReservationProductID }, reservationProduct.ReservationProductID);
        }

        // DELETE: api/ReservationProducts/5
        [HttpDelete("{id}")]
        [Route("DeleteReservationProduct/{id}")]
        public async Task<ActionResult<ReservationProduct>> DeleteReservationProduct(int id)
        {
            var reservationProduct = await _context.ReservationProducts.FindAsync(id);
            if (reservationProduct == null)
            {
                return NotFound();
            }

            _context.ReservationProducts.Remove(reservationProduct);
            await _context.SaveChangesAsync();

            return reservationProduct;
        }

        private bool ReservationProductExists(int id)
        {
            return _context.ReservationProducts.Any(e => e.ReservationProductID == id);
        }
    }
}
