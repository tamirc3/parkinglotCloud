using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLotAPI.ParkingLotObjects;
using PLotAPI.ParkingLotServices;

namespace PLotAPI.Controllers
{
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService _parkingLotService;
        public ParkingLotController()
        {
            _parkingLotService = new ParkingLotService();
        }

        [HttpGet]
        [Route("/health")]
        public ContentResult EntryToParkingLot()
        {
            return new ContentResult() { Content = $"app is up and running ,current datetime:{DateTime.Now.ToString(CultureInfo.InvariantCulture)}", StatusCode = 200 };
        }

        [HttpPost]
        [Route("/entry")]
        public ContentResult EntryToParkingLot([FromBody] EntryObject licensePlateAndParkingLotId)
        {
            if (licensePlateAndParkingLotId.licensePlate == null || licensePlateAndParkingLotId.parkingLotID < 0)
            {
                return new ContentResult() { Content = "invalid input, one of the argument is missing or not valid", StatusCode = 400 };
            }

            var ticketId = _parkingLotService.GetTicketId(licensePlateAndParkingLotId);
            Response.StatusCode = 200;

            return new ContentResult() { Content = ticketId.ToString(), StatusCode = 200 };
        }

        [HttpPost]
        [Route("/exit")]
        public ContentResult ExitFromParkingLot([FromBody] int ticketId)
        {
            if (ticketId < 0)
            {
                return new ContentResult() { Content = "invalid input, ticket id cannot be negative", StatusCode = 400 };
            }

            var exitObj = _parkingLotService.GetParkingCost(ticketId);
            if (exitObj != null)
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(exitObj), StatusCode = 200 };
            }

            return new ContentResult() { Content = $"ticketId {ticketId} doesn't exists", StatusCode = 400 };
        }
    }
}