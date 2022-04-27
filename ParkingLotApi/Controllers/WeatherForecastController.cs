using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLotAPI.ParkingLotObjects;
using PLotAPI.ParkingLotServices;

namespace PLotAPI.Controllers
{
    public class ParkingLotAssignmentContorller : ControllerBase
    {
        ParkingLotService ParkingLotService;
        public ParkingLotAssignmentContorller()
        {
            ParkingLotService = new ParkingLotService();
        }

        [HttpPost]
        [Route("/entry")]
        public ContentResult EntryToParkingLot([FromBody] EntryObject LicensePlateAndParkingLotID)
        {
            if (LicensePlateAndParkingLotID.licensePlate == null || LicensePlateAndParkingLotID.parkingLotID < 0)
            {
                return new ContentResult() { Content = "invalid input, one of the argument is missing or not valid", StatusCode = 400 };
            }

            var ticketId = ParkingLotService.GetTicketId(LicensePlateAndParkingLotID);
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

            var ExitObj = ParkingLotService.GetOut(ticketId);
            if (ExitObj != null)
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(ExitObj), StatusCode = 200 };
            }
            else
            {
                return new ContentResult() { Content = "ticketId doesnt exists", StatusCode = 400 };
            }
        }
    }
}