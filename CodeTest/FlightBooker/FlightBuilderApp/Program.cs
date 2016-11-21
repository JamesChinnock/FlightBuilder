using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker;
using StructureMap;
using FlightBooker = FlightBooker.FlightBuilderWrapper;

namespace FlightBuilderApp
{
    public class FlightRegistry : Registry
    {
        public FlightRegistry()
        {
            this.For<IFlightBuilder>().Use<FlightBuilder>();
            this.For<IFlightBuilderWrapper>().Use<FlightBuilderWrapper>();
            
        }

        public Container GetContainer()
        {
            return new Container(container =>
            {
                container.IncludeRegistry<FlightRegistry>();
            });
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var flightRegistry = new FlightRegistry();
            var container = flightRegistry.GetContainer();
            var builder = container.GetInstance<IFlightBuilderWrapper>();

            Console.WriteLine(string.Format("{0} logically valid (Arrive after departure) flights found", builder.GetLogicallyValidFlights().Count));
            Console.WriteLine(string.Format("{0} flights departings from now on", builder.GetFlightsDepartingAfter(DateTime.Now).Count));
            Console.WriteLine(string.Format("{0} flights with a stop over of less than 2 hours found", builder.StopOverLimit(2).Count));
        }


    }
}
