using System;
using System.Collections.Generic;
using DTO;

namespace LogicLayer
{
    public class Algoritme
        //algoritme som beregner om der er tegn for atrieflimren i den foretagne måling
    {
        private double PtakThreshold = 0.2; //genkender takker over 0.2
        private double RtakThreshold = 0.85; //genkender takker over 0.85
        private bool belowThresholdPtak = true;
        private bool belowThresholdRtak = true;
        private int PtakCount = 0;
        private int RtakCount = 0;


        public bool Analyze(EKG ekg)
        {
            List<double> EKGSample = ekg.EKGsamples;

            //Antallet af R-takker tælles
            for (int i = 0; i < EKGSample.Count; i++)
            {
                if(EKGSample[i] > RtakThreshold && belowThresholdRtak == true)
                {
                    RtakCount++;
                }
                
                if (EKGSample[i] < RtakThreshold)
                {
                    belowThresholdRtak = true;
                }
                else
                {
                    belowThresholdRtak = false;
                }
            }

            //Antallet af alle takker tælles
            for (int i = 0; i < EKGSample.Count; i++)
            {
                if (EKGSample[i] > PtakThreshold && belowThresholdPtak == true)
                {
                    PtakCount++;
                }

                if (EKGSample[i] < PtakThreshold)
                {
                    belowThresholdPtak = true;
                }
                else
                {
                    belowThresholdPtak = false;
                }
            }


            PtakCount = PtakCount - RtakCount; //Da R-takker tælles med i P-tak count trækkes de fra her igen

            if (PtakCount == 0 || RtakCount == 0)
            {
                return false;
            }

            {
                if (PtakCount / RtakCount >= 3) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
