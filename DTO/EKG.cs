using System;
using System.Collections.Generic;

namespace DTO
{
    public class EKG
    {
        public List<double> EKGsamples { get; set; }
        public double MeasurementPeriod { get; set; }
        public string CPR { get; set; }

        public DateTime MeasurementTime{ get; set; }

        public EKG(List<double> ekgSamples, double measurementPeriod, string cpr, DateTime measurementTime)
        {
            EKGsamples = ekgSamples;
            MeasurementPeriod = measurementPeriod;
            CPR = cpr;
            MeasurementTime = measurementTime;
        }
    }
}
