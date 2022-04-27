using PLotAPI.ParkingLotObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PLotAPI.ParkingLotServices
{
    public class ParkingLotService
    {
        public static void NullCheck(object argument, string name)
        {
            if (argument == null)
                throw new ArgumentNullException(name);
        }
        public ExitObject GetParkingCost(int ticketId)
        {
            var currentCar = new ExitObject();

            var singelton = ParkingLotSingleton.GetInstance();

            var tryToFind = singelton.TryGet(ticketId, out var parkedCarStatus);
            if (tryToFind)
            {
                singelton.Remove(ticketId, out parkedCarStatus);
                currentCar.parkingLotID = parkedCarStatus.parkingLotID;
                currentCar.licensePlate = parkedCarStatus.licensePlate;

                currentCar.TotalParkTime = CalculateTime(parkedCarStatus.StartParkingDateTime);
                currentCar.TotalPrice = CalculatePrice(currentCar.TotalParkTime);
                return currentCar;
            }
            else
            {
                return null;
            }
        }
        public int CalculatePrice(int totalTimeMinutes)
        {
            return (((totalTimeMinutes / 15) * 10));
        }
        public int CalculateTime(DateTime StartTime)
        {
            var timePassed = DateTime.UtcNow.Subtract(StartTime);
            var minutes = timePassed.TotalMinutes;
            return (int)minutes;
        }


        public int GetTicketId(EntryObject licensePlateAndParkingLotID)
        {
            var singelton = ParkingLotSingleton.GetInstance();

            var currentCar = new ParkingStatus();
            int ticketId = PickValidId();
            currentCar.parkingLotID = licensePlateAndParkingLotID.parkingLotID;
            currentCar.licensePlate = licensePlateAndParkingLotID.licensePlate;
            currentCar.StartParkingDateTime = DateTime.UtcNow;
            singelton.Add(ticketId, currentCar);
            return ticketId;
        }

        private int PickValidId()
        {
            var singelton = ParkingLotSingleton.GetInstance();

            Random r = new Random();
            int CurrentId = r.Next();
            while (singelton.ValidId(CurrentId))
            {
                CurrentId = r.Next();
            }
            return CurrentId;
        }

    }
}
