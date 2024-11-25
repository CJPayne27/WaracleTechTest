using HotelWaracleBookingApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvailabilityController : ControllerBase
{
    private readonly HotelWaracleDbContext _context;
    private readonly ILogger<AvailabilityController> _logger;

    public AvailabilityController(HotelWaracleDbContext context, ILogger<AvailabilityController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


}