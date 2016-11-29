using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;
using Train.Service.Graph;
using Train.Service.Models;
using Train.Service.Queue;

namespace Train.Service.Tests
{
    [TestClass]
    public class TownGraphTests
    {
        private ITrainQueue _queue;
        private ITrainGraph _graph;

        [TestInitialize]
        public void Setup()
        {
            _queue = new TrainQueue();
            _graph = new TrainGraph(_queue);

            _graph.AddRoute(Track.NewTrack("AB5"));
            _graph.AddRoute(Track.NewTrack("BC4"));
            _graph.AddRoute(Track.NewTrack("CD8"));
            _graph.AddRoute(Track.NewTrack("DC8"));
            _graph.AddRoute(Track.NewTrack("DE6"));
            _graph.AddRoute(Track.NewTrack("AD5"));
            _graph.AddRoute(Track.NewTrack("CE2"));
            _graph.AddRoute(Track.NewTrack("EB3"));
            _graph.AddRoute(Track.NewTrack("AE7"));
        }

        [TestMethod]
        public void TestInput1()
        {
            int? distance = _graph.FindDistance("A", "B", "C");

            distance.Should().Equals(9);
        }

        [TestMethod]
        public void TestInput2()
        {
            int? distance = _graph.FindDistance("A", "D");

            distance.Should().Equals(5);
        }

        [TestMethod]
        public void TestInput3()
        {
            int? distance = _graph.FindDistance("A", "D", "C");

            distance.Should().Equals(13);
        }

        [TestMethod]
        public void TestInput4()
        {
            int? distance = _graph.FindDistance("A", "E", "B", "C", "D");

            distance.Should().Equals(22);
        }

        [TestMethod]
        public void TestInput5()
        {
            int? distance = _graph.FindDistance("A", "E", "D");

            distance.Should().NotHaveValue();
        }

        [TestMethod]
        public void TestInput6()
        {
           IList<Route> routes = _graph.FindRoutes("C", "C").ToList();

            // C-D-C
            // C-E-B-C
            // C-D-E-B-C
            routes.Count.Should().Equals(3);

            routes.Count(a => a.Stops <= 3).Should().Equals(2);
        }

        [TestMethod]
        public void TestInput7()
        {
            IList<Route> routes = _graph.FindRoutes(new Request
            {
                From = "A",
                To = "C",
                MaxStop = 4,
                AllowCircularRoute = true
            }).ToList();

            routes.Count(a => a.Stops == 4).Should().Equals(3);

            var expected = new[]
{
                "A-B-C-D-C",
                "A-D-C-D-C",
                "A-D-E-B-C",
            };

            routes.Count(a => expected.Contains(a.Path)).Should().Equals(3);
        }

        [TestMethod]
        public void TestInput8()
        {
            Route route = _graph.FindShortest("A", "C");

            route.Should().NotBeNull();
            route.Distance.Should().Equals(9);
            route.Path.Should().Equals("A-B-C");
        }

        [TestMethod]
        public void TestInput9()
        {
            Route route = _graph.FindShortest("B", "B");

            route.Should().NotBeNull();
            route.Distance.Should().Equals(9);
            route.Path.Should().Equals("B-C-E-B");
        }

        [TestMethod]
        public void TestInput10()
        {
            IList<Route> routes = _graph.FindRoutes(new Request
            {
                From = "C",
                To = "C",
                AllowCircularRoute = true,
                DistanceLessThan = 30
            }).ToList();

            // Route (distance)
            //--------------
            // C-D-C (16) 
            // C-E-B-C (9)
            // C-E-B-C-D-C (25)
            // C-D-C-E-B-C (25)
            // C-D-E-B-C (21)
            // C-E-B-C-E-B-C (18)
            // C-E-B-C-E-B-C-E-B-C (27)

            var expected = new []
            {
                "C-D-C",
                "C-E-B-C",
                "C-E-B-C-D-C",
                "C-D-C-E-B-C",
                "C-D-E-B-C",
                "C-E-B-C-E-B-C",
                "C-E-B-C-E-B-C-E-B-C"
            };

            routes.Count.Should().Equals(7);
            routes.Count(a => expected.Contains(a.Path)).Should().Equals(7);
        }
    }
}
