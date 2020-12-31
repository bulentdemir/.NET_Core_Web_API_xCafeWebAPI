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
    public class ReservationsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ReservationsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        [Route("GetReservations")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }
        [HttpGet("{id}")]
        [Route("GetReservationsByUserID/{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByUserID(string id)
        {
            return await _context.Reservations.Where(x => x.UserID == id).ToListAsync();
        }

        [HttpGet("{id}")]
        [Route("GetReservationUser/{id}")]
        public async Task<ActionResult<IEnumerable<ReservationUser>>> GetReservationUser(int id)
        {
            var result = (from reservation in _context.Reservations
                          join user in _context.ApplicationUsers on reservation.UserID equals user.Id
                          select new ReservationUser(reservation.ReservationID,
                              reservation.ReservationTime,
                              reservation.IsFinished,
                              reservation.IsCanceled,
                              reservation.ReservationPrice,
                              user.Id,
                              user.FullName,
                              reservation.TableID)).OrderByDescending(x => x.ReservationID);

            Task<List<ReservationUser>> reservationUserList;
            switch (id)
            {
                case 1:
                    reservationUserList = result.Where(x => x.IsFinished == true).ToListAsync();
                    break;
                case 2:
                    reservationUserList = result.Where(x => x.IsCanceled == true).ToListAsync();
                    break;
                case 3:
                    reservationUserList = result.ToListAsync();
                    break;
                case 4:
                    reservationUserList = result.Where(x => x.IsCanceled == false && x.IsFinished == false).ToListAsync();
                    break;
                default:
                    return NotFound();
            }
            return await reservationUserList;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        [Route("GetReservation/{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpGet]
        [Route("GetReservationsByDate")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByDate()
        {
            var reservations = _context.Reservations.Where(x => x.ReservationTime.Date == DateTime.Today && x.IsFinished == true).ToListAsync();

            return await reservations;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        [Route("GetSingleReservation/{id}")]
        public async Task<ActionResult<ReservationUser>> GetSingleReservation(int id)
        {
            var singleReservation = await (from res in _context.Reservations
                                           join user in _context.ApplicationUsers on res.UserID equals user.Id
                                           where res.ReservationID == id
                                           select new ReservationUser(res.ReservationID,
                                               res.ReservationTime,
                                               res.IsFinished,
                                               res.IsCanceled,
                                               res.ReservationPrice,
                                               user.Id,
                                               user.FullName,
                                               res.TableID)).FirstOrDefaultAsync();
            if (singleReservation == null)
            {
                return NotFound();
            }

            return singleReservation;
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        [Route("PutReservation/{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        [HttpPost]
        [Route("FinishReservation")]
        public async Task<IActionResult> FinishReservation(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;

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

        // POST: api/Reservations
        [HttpPost]
        [Route("PostReservation")]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.ReservationID }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
