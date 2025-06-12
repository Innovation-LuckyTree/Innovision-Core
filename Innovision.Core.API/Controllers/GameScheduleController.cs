using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class GameScheduleController : ApiBaseController
{
    private readonly ILogger<GameScheduleController> _logger;

    public GameScheduleController(ILogger<GameScheduleController> logger)
    {
        _logger = logger;
    }

}