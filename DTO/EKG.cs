using System;
using System.Collections.Generic;

namespace DTO
{
    public class EKG
    {
        public List<double> EKGsamples { get; set; }
        public double MeasurementPeriod { get; set; }
        public Patient patient { get; set; }

        public DateTime MeasurementTime{ get; set; }

        public EKG(List<double> ekgSamples, double measurementPeriod, Patient patient, DateTime measurementTime)
        {
            EKGsamples = ekgSamples;
            MeasurementPeriod = measurementPeriod;
            this.patient = patient;
            MeasurementTime = measurementTime;
        }
    }
}
