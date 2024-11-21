using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count-1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            }; 

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };
            
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendsInPast()
        {
            DateTime startDate = new DateTime(2021, 5, 1);
            int count = 3;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 20), new DateTime(2021, 4, 22)) 
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 5, 3), result);
        }

        [TestMethod]
        public void TestWeekendsOverlappingNow()
        {
            DateTime startDate = new DateTime(2021, 5, 1);
            int count = 3;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 30), new DateTime(2021, 5, 2)) 
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 5, 5), result);
        }

        [TestMethod]
        public void TestWeekendsInFuture()
        {
            DateTime startDate = new DateTime(2021, 5, 1);
            int count = 3;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 5, 3), new DateTime(2021, 5, 4)) 
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 5, 5), result);
        }

        [TestMethod]
        public void TestNoWorkDaysDueToContinuousWeekends()
        {
            DateTime startDate = new DateTime(2021, 5, 1);
            int count = 3;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 5, 1), new DateTime(2021, 5, 10)) 
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 5, 13), result); 
        }

        [TestMethod]
        public void TestEmptyWeekends()
        {
            DateTime startDate = new DateTime(2021, 5, 1);
            int count = 5;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, new WeekEnd[0]);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestSingleDayWeekend()
        {
            DateTime startDate = new DateTime(2021, 5, 1);
            int count = 3;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 5, 2), new DateTime(2021, 5, 2)) 
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 5, 4), result);
        }
    }
}