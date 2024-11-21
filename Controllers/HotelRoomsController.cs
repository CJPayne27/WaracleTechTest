﻿using HotelWaracleBookingApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly HotelWaracleDbContext _context;
        private readonly ILogger<HotelRoomsController> _logger;

        public HotelRoomsController(HotelWaracleDbContext context, ILogger<HotelRoomsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<HotelRoomsController>));
        }

        /// <summary>
        /// Retrieves all hotel rooms
        /// </summary>
        /// <returns>List of all valid hotel rooms.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllHotelRooms()
        {
            _logger.LogInformation("HotelRooms: Received request to get all hotel rooms");
            
            try
            {
                var hotelRooms = await _context.HotelRooms.ToListAsync();

                _logger.LogInformation("HotelRooms: Processing request to return all hotel rooms");

                return Ok(hotelRooms);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "HotelRooms: An error occurred while processing the GetAllHotelRooms request");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves all hotel rooms by hotel ID
        /// </summary>
        /// <param name="hotelId">The Id of the Hotel</param>
        /// <returns>List of valid hotel rooms for that specific hotel, or a 404 if no hotel rooms found.</returns>
        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelRoomsByHotelId(string hotelId)
        {
            _logger.LogInformation("HotelRooms: Received request to get hotel rooms for HotelId: {HotelId}", hotelId);

            if (string.IsNullOrWhiteSpace(hotelId))
            {
                _logger.LogWarning("HotelRooms: Invalid HotelId: {HotelId} provided", hotelId);

                return BadRequest("Hotel ID cannot be null or empty.");
            }

            try
            {
                var hotelRooms = await _context.HotelRooms.Where(r => r.HotelId == hotelId).ToListAsync();

                if (hotelRooms.Count == 0)
                {
                    _logger.LogInformation("HotelRooms: No rooms found for HotelId: {HotelId}", hotelId);

                    return NotFound($"No rooms found for hotel ID: {hotelId}");
                }

                _logger.LogInformation("HotelRooms: Processing request to return hotel rooms for HotelId: {HotelId}", hotelId);

                return Ok(hotelRooms);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "HotelRooms: An error occurred while processing the GetHotelRoomByHotelId request");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
