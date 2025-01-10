using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using TimePeriodsHelper.Helper;

namespace TimePeriodsHelper
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DateTime start = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end = new DateTime(2025, 1, 9, 17, 0, 0);

            Period period1 = new Period(start, end);

            Assert.AreEqual(start, period1.Start);
            Assert.AreEqual(end, period1.End);
            Assert.AreEqual(60 * 9, period1.TotalMinutes);

            Period period2 = new Period(end, start);
            Assert.AreEqual(0, period2.TotalMinutes);
        }

        /// <summary>
        /// 加沒有交集的片段會變兩個
        /// </summary>
        [TestMethod]
        public void TestMethodAdd1()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 12, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            DateTime start2 = new DateTime(2025, 1, 9, 13, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 17, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.AddPeriod(period2);

            Assert.AreEqual(2, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 8, timePeriodsHelper.TotalMinutes);
        }

        /// <summary>
        /// 加有交集的片段會合成一個
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 12, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            DateTime start2 = new DateTime(2025, 1, 9, 13, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 17, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.AddPeriod(period2);

            DateTime start3 = new DateTime(2025, 1, 9, 12, 0, 0);
            DateTime end3 = new DateTime(2025, 1, 9, 13, 0, 0);
            Period period3 = new Period(start3, end3);
            timePeriodsHelper.AddPeriod(period3);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 9, timePeriodsHelper.TotalMinutes);
        }

        #region 減法
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestMethodMinute1()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 17, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 9, timePeriodsHelper.TotalMinutes);

            DateTime start2 = new DateTime(2025, 1, 9, 12, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 13, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.MinusPeriod(period2);

            Assert.AreEqual(2, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 8, timePeriodsHelper.TotalMinutes);
        }

        [TestMethod]
        public void TestMethodMinute2()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 12, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 13, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 1, timePeriodsHelper.TotalMinutes);

            DateTime start2 = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 17, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.MinusPeriod(period2);

            Assert.AreEqual(0, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(0, timePeriodsHelper.TotalMinutes);
        }

        [TestMethod]
        public void TestMethodMinute3()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 13, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 5, timePeriodsHelper.TotalMinutes);

            DateTime start2 = new DateTime(2025, 1, 9, 12, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 13, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.MinusPeriod(period2);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 4, timePeriodsHelper.TotalMinutes);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestMethodMinute4()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 12, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 17, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 5, timePeriodsHelper.TotalMinutes);

            DateTime start2 = new DateTime(2025, 1, 9, 12, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 13, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.MinusPeriod(period2);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 4, timePeriodsHelper.TotalMinutes);
        }

        /// <summary>
        /// 減無關的片段
        /// </summary>
        [TestMethod]
        public void TestMethodMinute5()
        {
            Helper.TimePeriodsHelper timePeriodsHelper = new Helper.TimePeriodsHelper();

            DateTime start1 = new DateTime(2025, 1, 9, 8, 0, 0);
            DateTime end1 = new DateTime(2025, 1, 9, 12, 0, 0);
            Period period1 = new Period(start1, end1);
            timePeriodsHelper.AddPeriod(period1);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 4, timePeriodsHelper.TotalMinutes);

            DateTime start2 = new DateTime(2025, 1, 9, 13, 0, 0);
            DateTime end2 = new DateTime(2025, 1, 9, 17, 0, 0);
            Period period2 = new Period(start2, end2);
            timePeriodsHelper.MinusPeriod(period2);

            Assert.AreEqual(1, timePeriodsHelper.Periods.Count);
            Assert.AreEqual(60 * 4, timePeriodsHelper.TotalMinutes);
        }

        #endregion
    }
}
