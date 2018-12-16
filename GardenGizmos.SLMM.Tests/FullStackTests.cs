using GardenGizmos.SLMM.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;

namespace GardenGizmos.SLMM.Tests
{
    [TestClass]
    public class FullStackTests
    {
        private HttpClient _testClient;
        private string _baseUrl = "http://localhost:5000/api/mowingmachine";

        [TestMethod]
        public void TestWalkthrough()
        {
            var builder = WebHost.CreateDefaultBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _testClient = testServer.CreateClient();

            // Check starting position
            VerifyPosition(0, 0, 0, "North");           // Initially after 0s we're at the start position

            // Check that moves take time
            MoveForwards();
            VerifyPosition(0, 0, 0, "North");           // After no wait we've not moved
            VerifyPosition(3, 0, 0, "North");           // After 3s we've still not moved
            VerifyPosition(2, 1, 0, "North");           // After another 2s we've moved lengthwise

            // Check we can stack a sequence of moves
            MoveForwards();
            TurnClockwise();
            MoveForwards();
            TurnAntiClockwise();
            MoveForwards();
            VerifyPosition(5, 2, 0, "North");           // Move forwards
            VerifyPosition(3, 2, 0, "East");            // Turn clockwise
            VerifyPosition(5, 2, 1, "East");            // Move forwards
            VerifyPosition(3, 2, 1, "North");           // Turn anticlockwise
            VerifyPosition(5, 3, 1, "North");           // Move forwards

            // Check we can rotate anticlockwise corrrectly
            TurnAntiClockwise(); VerifyPosition(3, 3, 1, "West");
            TurnAntiClockwise(); VerifyPosition(3, 3, 1, "South");
            TurnAntiClockwise(); VerifyPosition(3, 3, 1, "East");
            TurnAntiClockwise(); VerifyPosition(3, 3, 1, "North");

            // Check we can rotate clockwise correctly
            TurnClockwise(); VerifyPosition(3, 3, 1, "East");
            TurnClockwise(); VerifyPosition(3, 3, 1, "South");
            TurnClockwise(); VerifyPosition(3, 3, 1, "West");
            TurnClockwise(); VerifyPosition(3, 3, 1, "North");

            // Check we can move North and not leave boundaries
            MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards();
            VerifyPosition(30, 4, 1, "North");

            // Check we can move East and not leave boundaries
            TurnClockwise();
            MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards();
            VerifyPosition(33, 4, 4, "East");

            // Check we can move South and not leave boundaries
            TurnClockwise();
            MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards();
            VerifyPosition(33, 0, 4, "South");

            // Check we can move West and not leave boundaries
            TurnClockwise();
            MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards(); MoveForwards();
            VerifyPosition(33, 0, 0, "West");
        }

        private void VerifyPosition(int seconds, int length, int width, string orientation)
        {
            Wait(seconds);

            var result = _testClient.GetStringAsync($"{_baseUrl}/position").Result;
            var machine = JsonConvert.DeserializeObject<MowingMachine>(result);

            Assert.AreEqual(length, machine.Position.Length);
            Assert.AreEqual(width, machine.Position.Width);
            Assert.AreEqual(orientation, machine.Orientation);
        }

        private void MoveForwards()
        {
            var result = _testClient.GetStringAsync($"{_baseUrl}/move-forwards").Result;
        }

        private void TurnClockwise()
        {
            var result = _testClient.GetStringAsync($"{_baseUrl}/turn-clockwise").Result;
        }

        private void TurnAntiClockwise()
        {
            var result = _testClient.GetStringAsync($"{_baseUrl}/turn-anticlockwise").Result;
        }

        private void Wait(int seconds)
        {
            while (seconds > 0)
            {
                TestTimer.ClockTick();
                Thread.Sleep(1);
                seconds--;
            }
        }
    }
}
