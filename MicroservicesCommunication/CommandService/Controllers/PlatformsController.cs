using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[ApiController]
[Route("api/c/[controller]")]
public class PlatformsController : ControllerBase
{
  public PlatformsController()
  {
  }

  [HttpPost]
  public ActionResult TestInboundConnection()
  {
    Console.WriteLine("-- Inbound Post #Command Service");

    return Ok("Inbound test for Platforms Controller");
    
  }
}
