using PLotAPI.ParkingLotObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PLotAPI.ParkingLotServices
{
    public class ParkingLotService
    {
        public ParkingLotService()
        {

        }
        
        public static void NullCheck(object argument, string name)
        {
            if (argument == null)
                throw new ArgumentNullException(name);
        }
        public ExitObject GetOut(int ticketId)
        {
            var currentCar = new ExitObject();
            ParkingStatus parkedCarStatusl;

            var singelton = ParkingLotSingleton.GetInstance();

            var tryToFind = singelton.TryGet(ticketId, out parkedCarStatusl);
            if (tryToFind)
            {
                singelton.Remove(ticketId, out parkedCarStatusl);
                currentCar.parkingLotID = parkedCarStatusl.parkingLotID;
                currentCar.licensePlate = parkedCarStatusl.licensePlate;

                currentCar.TotalParkTime = CalculateTime(parkedCarStatusl.StartParkingDateTime);
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
