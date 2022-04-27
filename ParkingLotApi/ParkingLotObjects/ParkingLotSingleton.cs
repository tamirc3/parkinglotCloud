using System.Collections.Concurrent;

namespace PLotAPI.ParkingLotObjects
{
    // This ParkingLotSingleton implementation is called "double check lock". It is safe
    // in multithreaded environment and provides lazy initialization for the
    // ParkingLotSingleton object.
    class ParkingLotSingleton
    {
        private ParkingLotSingleton() { }

        private static ParkingLotSingleton _instance;

        // We now have a lock object that will be used to synchronize threads
        // during first access to the ParkingLotSingleton.
        private static readonly object _lock = new object();

        public static ParkingLotSingleton GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ParkingLotSingleton();
                    }
                }
            }
            return _instance;
        }

        // We'll use this property to prove that our ParkingLotSingleton rWAeally works.
        public static ConcurrentDictionary<int,ParkingStatus> Dictionary = new ConcurrentDictionary<int,ParkingStatus>();

        public bool ValidId(int CurrentId)
        {
            return Dictionary.ContainsKey(CurrentId);
        }

        public bool TryGet(int ticketId, out ParkingStatus parkedCarStatusl)
        {
            return Dictionary.TryGetValue(ticketId, out parkedCarStatusl);
        }
        public bool Remove(int ticketId, out ParkingStatus parkedCarStatusl)
        {
            return Dictionary.TryRemove(ticketId, out parkedCarStatusl);
        }
        public bool Add(int ticketId, ParkingStatus currentCar)
        {
            return Dictionary.TryAdd(ticketId, currentCar);
        }

    }   
}
