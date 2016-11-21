using System.Collections.Generic;

namespace FlightBooker
{
    public interface IFlightBuilder
    {
        IList<Flight> GetFlights();
    }
}