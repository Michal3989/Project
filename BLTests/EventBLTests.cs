using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace BL.Tests
{
    [TestClass()]
    public class EventBLTests
    {
        [TestMethod()]
        public void AddEventTest()
        {
            EventDto newEvent = new EventDto
            {
                IdEventOwner = 1,
                EventTypeCode = 2,
                Date = new DateTime(2019,04,01),
                Name = "sara"
            };

            EventBL.AddEvent(newEvent);
            Assert.Fail();
        }
    }
}