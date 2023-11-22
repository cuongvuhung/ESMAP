using CBM_API.Entities;
using Microsoft.EntityFrameworkCore;
using Minio;

namespace CBM_API.Ultilities
{
    public class Calc
    {
        public static double? BDAScore1(BDA item, BDAr bDAr)
        {
            if (bDAr == null) { return 0; }
            double rOutside = bDAr.Outside ?? 0;
            double rTemp = bDAr.Temperature ?? 0;
            double rPd = bDAr.Pd ?? 0;
            double rSec = bDAr.SecondaryVoltage ?? 0;
            double rHis = bDAr.HistoryMain ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? SecondaryVoltage = item.SecondaryVoltage;
            double? Pd = item.Pd;
            double? HistoryMain = item.HistoryMain;
            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.SecondaryVoltage == null) { rSec = 0; SecondaryVoltage = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }

            double sumRate = rOutside + rTemp + rPd + rSec + rHis;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + SecondaryVoltage * rSec
                + HistoryMain * rHis)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? BDAScore23(BDA item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.Ratio
                + item.ROneWayCoil
                + item.TgLost
                + item.VoltageHightRate
                + item.RatioVoltageHight
                + item.OilIsolate
                + item.SensorWireLoop), 2
                );
        }




        public static double? BDDScore1(BDD item, BDDr bDDr)
        {
            if (bDDr == null) { return 0; }
            double rOutside = bDDr.Outside ?? 0;
            double rTemp = bDDr.Temperature ?? 0;
            double rPd = bDDr.Pd ?? 0;
            double rHis = bDDr.HistoryMain ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? HistoryMain = item.HistoryMain;
            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + HistoryMain * rHis)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? BDDScore23(BDD item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.Ratio
                + item.ROneWayCoil
                + item.TgLost
                + item.VoltageHightRate
                + item.RatioCurrentSource
                + item.Chemical
                + item.OilIsolate
                ), 2);
        }

        public static double? CLScore1(CL item, CLr cLr)
        {
            if (cLr == null) { return 0; }
            double rOutside = cLr.Outside ?? 0;
            double rTemp = cLr.Temperature ?? 0;
            double rPd = cLr.Pd ?? 0;
            double rHfctAndTev = cLr.HfctAndTev ?? 0;
            double rHis = cLr.HistoryMain ?? 0;
            double rNumberYearOper = cLr.NumberYearOper ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? HfctAndTev = item.HfctAndTev;
            double? HistoryMain = item.HistoryMain;
            double? NumberYearOper = item.NumberYearOper;
            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.HfctAndTev == null) { rHfctAndTev = 0; HfctAndTev = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }
            if (item.NumberYearOper == null) { rNumberYearOper = 0; NumberYearOper = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis + rHfctAndTev + rNumberYearOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + HfctAndTev * rHfctAndTev
                + HistoryMain * rHis
                + NumberYearOper * rNumberYearOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? CLScore23(CL item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.HightVoltageRes
                + item.HightVoltageResCase
                + item.PdDeep
                + item.TgLost
                ), 2);
        }

        public static double? CSVScore1(CSV item, CSVr cLr)
        {
            if (cLr == null) { return 0; }
            double rOutside = cLr.Outside ?? 0;
            double rTemp = cLr.Temperature ?? 0;
            double rPd = cLr.Pd ?? 0;
            double rPdOnline = cLr.PdOnline ?? 0;
            double rHis = cLr.HistoryMain ?? 0;
            double rNumberYearOper = cLr.NumberYearOper ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? PdOnline = item.PdOnline;
            double? HistoryMain = item.HistoryMain;
            double? NumberYearOper = item.NumberYearOper;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.PdOnline == null) { rPdOnline = 0; PdOnline = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }
            if (item.NumberYearOper == null) { rNumberYearOper = 0; NumberYearOper = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis + rPdOnline + rNumberYearOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + PdOnline * rPdOnline
                + HistoryMain * rHis
                + NumberYearOper * rNumberYearOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? CSVScore23(CSV item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.PdByIndeSource
                + item.PowerK
                + item.PdAnalysis
                ), 2);
        }

        public static double? DCLScore1(DCL item, DCLr cLr)
        {
            if (cLr == null) { return 0; }
            double rOutside = cLr.Outside ?? 0;
            double rTemp = cLr.Temperature ?? 0;
            double rPd = cLr.Pd ?? 0;
            double rHis = cLr.HistoryMain ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? HistoryMain = item.HistoryMain;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + HistoryMain * rHis)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? DCLScore23(DCL item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.RContact
                + item.ROneWayMotor
                + item.VoltageACMotor
                ), 2);
        }

    }
}