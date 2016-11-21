using System;
using System.Collections.Generic;

namespace FlightBooker
{
    public interface IFlightBuilderWrapper
    {
        List<Flight> GetFlightsDepartingAfter(DateTime departureDatetime);
        List<Flight> GetLogicallyValidFlights();
        List<Flight> StopOverLimit(double hours);
    }
}