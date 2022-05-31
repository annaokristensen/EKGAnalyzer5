using System;
using System.Collections.Generic;

namespace DTO
{
    public class EKG
        //DTO som gemmer nedstående data i forhold til ekg-målingen
    {
        public List<double> EKGsamples { get; set; }
        public double SampleRate { get; set; }
        public int IntervalSec { get; set; }
        public string DataFormat { get; set; }
        public string BinEllerTekst { get; set; }
        public string Maaleformat_type { get; set; }
        public string CPR { get; set; }
        public DateTime MeasurementTime{ get; set; }

        public EKG()
        {}

        public EKG(List<double> ekgSamples, double sampleRate, int intervalSec,string dataFormat, string binEllerTekst, string maaleformatType, string cpr, DateTime measurementTime)
            
        {
            EKGsamples = ekgSamples;
            SampleRate = sampleRate;
            IntervalSec = intervalSec;
            DataFormat = dataFormat;
            BinEllerTekst = binEllerTekst;
            Maaleformat_type = maaleformatType;
            CPR = cpr;
            MeasurementTime = measurementTime;
        }
    }
}
