using System;
using System.Collections.Generic;

namespace FlightBooker.Tests
{
    public class FlightBookerTestsBase
    {
        public List<Flight> ListOfValidFlights
        {
            get
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment {DepartureDate = DateTime.Now.AddHours(1), ArrivalDate = DateTime.Now.AddHours(2)},
                            new Segment {DepartureDate = DateTime.Now.AddHours(3), ArrivalDate = DateTime.Now.AddHours(4)},
                            new Segment {DepartureDate = DateTime.Now.AddHours(5), ArrivalDate = DateTime.Now.AddHours(6), }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment {DepartureDate = DateTime.Now.AddHours(1), ArrivalDate = DateTime.Now.AddHours(2)},
                            new Segment {DepartureDate = DateTime.Now.AddHours(5), ArrivalDate = DateTime.Now.AddHours(6), },
                            new Segment { DepartureDate = DateTime.Now.AddHours(7), ArrivalDate = DateTime.Now.AddHours(8),}
                        }
                    }
                };
            }
        }

        public List<Flight> OneInvalidFlight_and_OneValid
        {
            get
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment {ArrivalDate = DateTime.Now.AddHours(2), DepartureDate = DateTime.Now.AddHours(5)}
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { ArrivalDate = DateTime.Now.AddHours(10), DepartureDate = DateTime.Now.AddHours(5)}
                        }
                    }
                };
            }
        }

        public List<Flight> IncEarlierFlight
        {
            get
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment {ArrivalDate = DateTime.Now.AddHours(10), DepartureDate = DateTime.Now.AddHours(5)}
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { ArrivalDate = DateTime.Now.AddHours(-5), DepartureDate = DateTime.Now.AddHours(-10)}
                        }
                    }
                };
            }
        }
    }
}