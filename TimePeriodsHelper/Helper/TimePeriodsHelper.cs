using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimePeriodsHelper.Helper
{
    public class TimePeriodsHelper
    {
        public TimePeriodsHelper()
        {
            this.Periods = new List<Period>();
        }

        public List<Period> Periods;

        public List<Period> GetIntersections(Period period)
        {
            var intersections = this.Periods.Where(p =>
            !(period.Start > p.End || period.End < p.Start)
            ).ToList();
            return intersections;
        }

        public void AddPeriod(Period period)
        {
            if (period.TotalMinutes > 0)
            {
                //有交集的片段合成一個
                var intersections = this.GetIntersections(period);

                DateTime start = period.Start;
                DateTime end = period.End;

                foreach (var intersection in intersections)
                {
                    if (intersection.Start < start)
                    {
                        start = intersection.Start;
                    }
                    if (intersection.End > end)
                    {
                        end = intersection.End;
                    }
                }

                foreach (var intersection in intersections)
                {
                    this.Periods.Remove(intersection);
                }

                Period newPeriod = new Period(start, end);
                this.Periods.Add(newPeriod);
            }
        }

        public void MinusPeriod(Period period)
        {
            if (period.TotalMinutes > 0)
            {
                var intersections = this.GetIntersections(period);

                List<Period> newPeriods = new List<Period>();
                foreach (var intersection in intersections)
                {
                    if (period.Start > intersection.Start && period.End < intersection.End)
                    {
                        newPeriods.Add(new Period(intersection.Start, period.Start));
                        newPeriods.Add(new Period(period.End, intersection.End));
                    }
                    else if (period.Start < intersection.Start && period.End > intersection.End)
                    {

                    }
                    else
                    {
                        if (period.Start <= intersection.Start && period.End > intersection.Start)
                        {
                            newPeriods.Add(new Period(period.End, intersection.End));
                        }

                        if (period.Start < intersection.End && period.End >= intersection.End)
                        {
                            newPeriods.Add(new Period(intersection.Start, period.Start));
                        }
                    }
                }

                foreach (var intersection in intersections)
                {
                    this.Periods.Remove(intersection);
                }

                this.Periods.AddRange(newPeriods);
            }
        }

        public double TotalMinutes
        {
            get
            {
                double totalMinutes = this.Periods.Sum(p => p.TotalMinutes);
                return totalMinutes;
            }
        }
    }

    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; }

        public DateTime End { get; }

        public double TotalMinutes
        {
            get
            {
                double totalMinutes = (End - Start).TotalMinutes;
                return totalMinutes < 0 ? 0 : totalMinutes;
            }
        }
    }
}
