using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace FlightBooker.Tests
{
    [TestClass]
    public class FlightBookerTests : FlightBookerTestsBase
    {
        private IFlightBuilder _flightBuilder;
        private FlightBuilderWrapper _flightBooker;
 
        [TestInitialize]
        public void Init()
        {
            _flightBuilder = MockRepository.GenerateMock<IFlightBuilder>();
            
            _flightBooker = new FlightBuilderWrapper(_flightBuilder);
        }

        [TestMethod]
        public void InitializeNewInstance()
        {
            _flightBooker = new FlightBuilderWrapper(_flightBuilder);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeNewInstanceWithNullFlightBuilderThrowsException()
        {
            _flightBooker = new FlightBuilderWrapper(null);
        }

        [TestMethod]
        public void GetFlightsDepartingAfter_Method_ReturnsCorrectNumberOfFlights()
        {
            _flightBuilder.Expect(x => x.GetFlights()).Return(IncEarlierFlight);

            var departureDatetime = DateTime.Now;
            var flights = _flightBooker.GetFlightsDepartingAfter(departureDatetime);
            Assert.AreEqual(1, flights.Count);

            var departure = flights.FirstOrDefault().Segments.FirstOrDefault().DepartureDate;
            Assert.IsTrue(departure > departureDatetime);
        }

        [TestMethod]
        public void GetLogicallyValidFlights_Method_ReturnsCorrectNumberOfFlights()
        {
            _flightBuilder.Expect(x => x.GetFlights()).Return(OneInvalidFlight_and_OneValid);

            var flights = _flightBooker.GetLogicallyValidFlights();
            Assert.AreEqual(1, flights.Count);

            var departure = flights.FirstOrDefault().Segments.FirstOrDefault().DepartureDate;
            var arrival = flights.FirstOrDefault().Segments.FirstOrDefault().ArrivalDate;
            Assert.IsTrue(arrival > departure);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void StopOverLimit_NegativeHours()
        {
            _flightBooker.StopOverLimit(-1);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void StopOverLimit_ZeroHours()
        {
            _flightBooker.StopOverLimit(0);
        }

        [TestMethod]
        public void StopOverLimit_LessThanTwoHours()
        {
            _flightBuilder.Expect(x => x.GetFlights()).Return(ListOfValidFlights);
            var res = _flightBooker.StopOverLimit(2);
        }

    }

    
}
