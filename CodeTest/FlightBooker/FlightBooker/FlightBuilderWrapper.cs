using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooker
{
    public class FlightBuilderWrapper : IFlightBuilderWrapper
    {
        private readonly IFlightBuilder _flightBuilder;

        public FlightBuilderWrapper(IFlightBuilder _flightBuilder)
        {
            if (_flightBuilder == null) throw new ArgumentNullException(nameof(_flightBuilder));
            this._flightBuilder = _flightBuilder;
        }

        public List<Flight> GetFlightsDepartingAfter(DateTime departureDatetime)
        {
            var flights = _flightBuilder.GetFlights();
            var flightResults = new List<Flight>();

            foreach (var flight in flights)
            {
                bool valid = true;
                foreach (var segment in flight.Segments)
                {
                    if (segment.DepartureDate < departureDatetime)
                    {
                        valid = false;
                    }
                }


                if (valid)
                {
                    flightResults.Add(flight);
                }
            }

            return flightResults;
        }

        public List<Flight> GetLogicallyValidFlights()
        {
            var flights = _flightBuilder.GetFlights();
            var flightResults = new List<Flight>();

            foreach (var flight in flights)
            {
                bool valid = true;

                foreach (var segment in flight.Segments)
                {
                    if (segment.ArrivalDate < segment.DepartureDate)
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    flightResults.Add(flight);
                }
            }

            return flightResults.ToList();
        }

        public List<Flight> StopOverLimit(double hours)
        {
            if (hours <= 0)
            {
                throw new ApplicationException("Invalid stopover time, must be greater than zero hours");
            }

            var flights = this.GetLogicallyValidFlights();
            
            List<Flight> flightResults = new List<Flight>();

            foreach (var flight in flights)
            {
                var orderedSegments = flight.Segments.OrderBy(o => o.DepartureDate);
                var stopoverTime = new List<double>();

                if (orderedSegments.Count() == 1)
                {
                    flightResults.Add(flight);
                }

                for (int i = 1; i < orderedSegments.Count(); i++)
                {
                    var prevSeg = orderedSegments.ElementAt(i - 1);
                    var seg = orderedSegments.ElementAt(i);

                    var hourDifference = (seg.DepartureDate - prevSeg.ArrivalDate).TotalHours;

                    stopoverTime.Add(hourDifference);

                }

                if (!stopoverTime.Any(x => x > hours))
                {
                    flightResults.Add(flight);
                }
            }

            return flightResults;
        }


    }
}