using GardenGizmos.SLMM.Navigation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GardenGizmos.SLMM.Controllers
{
    [Route("api/mowingmachine")]
    [ApiController]
    public class MowingMachineController : ControllerBase
    {
        private Navigator _navigator;

        public MowingMachineController(Navigator navigator)
        {
            _navigator = navigator;
        }

        [HttpGet("position")]
        public ActionResult Position()
        {
            return new JsonResult(_navigator.MachinePosition());
        }

        [HttpGet("turn-clockwise")]
        public ActionResult TurnClockwise()
        {
            _navigator.AddNavigationCommand(new TurnClockwise());
            return Ok();
        }

        [HttpGet("turn-anticlockwise")]
        public ActionResult TurnAntiClockwise()
        {
            _navigator.AddNavigationCommand(new TurnAntiClockwise());
            return Ok();
        }

        [HttpGet("move-forwards")]
        public ActionResult MoveForwards()
        {
            _navigator.AddNavigationCommand(new MoveForwards());
            return Ok();
        }
    }
}
