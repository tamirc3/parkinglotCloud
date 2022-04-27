using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLotAPI.ParkingLotObjects;
using PLotAPI.ParkingLotServices;

namespace PLotAPI.Controllers
{
    public class ParkingLotController : ControllerBase
    {
        ParkingLotService ParkingLotService;
        public ParkingLotController()
        {
            ParkingLotService = new ParkingLotService();
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

            var ticketId = ParkingLotService.GetTicketId(licensePlateAndParkingLotId);
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

            var exitObj = ParkingLotService.GetOut(ticketId);
            if (exitObj != null)
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(exitObj), StatusCode = 200 };
            }
            else
            {
                return new ContentResult() { Content = $"ticketId {ticketId} doesn't exists", StatusCode = 400 };
            }
        }
    }
}