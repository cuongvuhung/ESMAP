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


        public static double? MBAScore1(MBA item, MBAr cLr)
        {
            if (cLr == null) { return 0; }
            double rOutside = cLr.Outside ?? 0;
            double rTemp = cLr.Temperature ?? 0;
            double rPd = cLr.Pd ?? 0;
            double rPdOnline = cLr.PdOnline ?? 0;
            double rOil = cLr.Oil ?? 0;
            double rOilair = cLr.Oilair ?? 0;
            double rOilOLTC = cLr.OilOLTC ?? 0;
            double rAirOLTC = cLr.AirOLTC ?? 0;
            double rOldPaperIsolate = cLr.OldPaperIsolate ?? 0;
            double rMotorOLTC = cLr.MotorOLTC ?? 0;
            double rNM = cLr.NM ?? 0;
            double rHistoryOper = cLr.HistoryOper ?? 0;
            double rOldOper = cLr.OldOper ?? 0;

            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? PdOnline = item.PdOnline;
            double? Oil = item.Oil;
            double? Oilair = item.Oilair;
            double? OilOLTC = item.OilOLTC;
            double? AirOLTC = item.AirOLTC;
            double? OldPaperIsolate = item.OldPaperIsolate;
            double? MotorOLTC = item.MotorOLTC;
            double? NM = item.NM;
            double? HistoryOper = item.HistoryOper;
            double? OldOper = item.OldOper;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.PdOnline == null) { rPdOnline = 0; PdOnline = 0; }
            if (item.Oil == null) { rOil = 0; Oil = 0;}
            if (item.Oilair == null) { rOilair = 0; Oilair = 0; }
            if (item.OilOLTC == null) { rOilOLTC = 0; OilOLTC = 0; }
            if (item.AirOLTC == null) { rAirOLTC = 0; AirOLTC = 0; }
            if (item.OldPaperIsolate == null) { rOldPaperIsolate = 0; OldPaperIsolate = 0; }
            if (item.MotorOLTC == null) { rMotorOLTC = 0; MotorOLTC = 0; }
            if (item.NM == null) { rNM = 0; NM = 0; }
            if (item.HistoryOper == null) { rHistoryOper = 0; HistoryOper = 0; }
            if (item.OldOper == null) { rOldOper = 0; OldOper = 0; }


            double sumRate = rOutside + rTemp + rPd + rPdOnline + rOil + rOilair + rOilOLTC + rAirOLTC + rOldPaperIsolate + rMotorOLTC + rNM + rHistoryOper + rOldOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + PdOnline * rPdOnline
                + Oil * rOil
                + Oilair * rOilair
                + OilOLTC * rOilOLTC
                + AirOLTC * rAirOLTC
                + OldPaperIsolate * rOldPaperIsolate
                + MotorOLTC * rMotorOLTC
                + NM * rNM
                + HistoryOper * rHistoryOper
                + OldOper * rOldOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? MBAScore23(MBA item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.MagnetIsolate
                + item.CoilIsolate
                + item.Ratio
                + item.TgLost
                + item.TgLostCapa
                + item.LowVoltage
                + item.OLTC
                + item.Frequency
                + item.RLostCurrent
                + item.HardCD
                + item.VoltageHightRate
                + item.CurrentVoltage
                + item.SensorPD                
                ), 2);
        }
        public static double? MCAIRScore1(MCAIR item, MCAIRr mCAIRr)
        {
            if (mCAIRr == null) { return 0; }
            double rOutside = mCAIRr.Outside ?? 0;
            double rTemp = mCAIRr.Temperature ?? 0;
            double rPd = mCAIRr.Pd ?? 0;
            double rPdTve = mCAIRr.PdTve ?? 0;
            double rPdOnline = mCAIRr.PdOnline ?? 0;
            double rHis = mCAIRr.HistoryMain ?? 0;
            double rNumberYearOper = mCAIRr.NumberYearOper ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? PdTve = item.PdTve;
            double? PdOnline = item.PdOnline;
            double? HistoryMain = item.HistoryMain;
            double? NumberYearOper = item.NumberYearOper ?? 0;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.PdTve == null) { rPdTve = 0; PdTve = 0; }
            if (item.PdOnline == null) { rPdOnline = 0; PdOnline = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }
            if (item.NumberYearOper == null) { rNumberYearOper = 0; NumberYearOper = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis + rPdTve+ rPdOnline+ rNumberYearOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + PdTve * rPdTve
                + PdOnline * rPdOnline
                + HistoryMain * rHis
                + NumberYearOper * rNumberYearOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? MCAIRScore23(MCAIR item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.RContact
                + item.TimeCut
                + item.RIsolateClose
                + item.RIsolateCut
                + item.RIsolateMotor
                + item.Air
                + item.HightVoltageAC
                + item.PdOnlineAnalysis
                + item.CutOnline
                + item.SpeedFlowCut
                ), 2);
        }
        public static double? MCGISScore1(MCGIS item, MCGISr mCGISr)
        {
            if (mCGISr == null) { return 0; }
            double rOutside = mCGISr.Outside ?? 0;
            double rTemp = mCGISr.Temperature ?? 0;
            double rPd = mCGISr.Pd ?? 0;
            double rPdTevUhf = mCGISr.PdTevUhf ?? 0;
            double rPdOnline = mCGISr.PdOnline ?? 0;
            double rSF6Lost = mCGISr.SF6Lost ?? 0;
            double rHis = mCGISr.HistoryMain ?? 0;
            double rNumberYearOper = mCGISr.NumberYearOper ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? PdTevUhf = item.PdTevUhf;
            double? PdOnline = item.PdOnline;
            double? SF6Lost = item.SF6Lost;
            double? HistoryMain = item.HistoryMain;
            double? NumberYearOper = item.NumberYearOper;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.PdTevUhf == null) { rPdTevUhf = 0; PdTevUhf = 0; }
            if (item.PdOnline == null) { rPdOnline = 0; PdOnline = 0; }
            if (item.SF6Lost == null) { rSF6Lost = 0; SF6Lost = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }
            if (item.NumberYearOper == null) { rNumberYearOper = 0; NumberYearOper = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis + rPdTevUhf + rPdOnline + rSF6Lost + rNumberYearOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + PdTevUhf * rPdTevUhf
                + PdOnline * rPdOnline
                + SF6Lost * rSF6Lost
                + HistoryMain * rHis
                + NumberYearOper * rNumberYearOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? MCGISScore23(MCGIS item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.RContact
                + item.TimeCut
                + item.RIsolateClose
                + item.RIsolateCut
                + item.PurifyHuminitySF6
                + item.SF6Decay
                + item.LostSF6
                + item.RIsolateOneWayMotor
                + item.HightVoltageAC
                + item.CutOnline
                + item.RMotor
                + item.SpeedFlowCut
                + item.SF6Analysis
                + item.PdAnalysis
                ), 2);
        }
        public static double? MCHGISScore1(MCHGIS item, MCHGISr mCHGISr)
        {
            if (mCHGISr == null) { return 0; }
            double rOutside = mCHGISr.Outside ?? 0;
            double rTemp = mCHGISr.Temperature ?? 0;
            double rPd = mCHGISr.Pd ?? 0;
            double rSF6Lost = mCHGISr.SF6Lost ?? 0;
            double rHis = mCHGISr.HistoryMain ?? 0;
            double rNumberYearOper = mCHGISr.NumberYearOper ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? SF6Lost = item.SF6Lost;
            double? HistoryMain = item.HistoryMain;
            double? NumberYearOper = item.NumberYearOper;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.SF6Lost == null) { rSF6Lost = 0; SF6Lost = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }
            if (item.NumberYearOper == null) { rNumberYearOper = 0; NumberYearOper = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis + rSF6Lost + rNumberYearOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + SF6Lost * rSF6Lost
                + HistoryMain * rHis
                + NumberYearOper * rNumberYearOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? MCHGISScore23(MCHGIS item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.RContact
                + item.TimeCut
                + item.RIsolateClose
                + item.RIsolateCut
                + item.PurifyHuminitySF6
                + item.SF6Decay
                + item.LostSF6
                + item.RIsolateMotor
                + item.HightVoltageAC
                + item.CutOnline
                + item.RMotor
                + item.SpeedFlowCut
                + item.SF6Analysis
                + item.PdAnalysis
                ), 2);
        }
        public static double? MCSF6Score1(MCSF6 item, MCSF6r mCSF6r)
        {
            if (mCSF6r == null) { return 0; }
            double rOutside = mCSF6r.Outside ?? 0;
            double rTemp = mCSF6r.Temperature ?? 0;
            double rPd = mCSF6r.Pd ?? 0;
            double rSF6Lost = mCSF6r.SF6Lost ?? 0;
            double rHis = mCSF6r.HistoryMain ?? 0;
            double rNumberYearOper = mCSF6r.NumberYearOper ?? 0;
            double maxScore = 9.9;

            Console.WriteLine("maxScore= " + maxScore);
            double? Outside = item.Outside;
            double? Temperature = item.Temperature;
            double? Pd = item.Pd;
            double? SF6Lost = item.SF6Lost;
            double? HistoryMain = item.HistoryMain;
            double? NumberYearOper = item.NumberYearOper;

            if (item.Outside == null) { rOutside = 0; Outside = 0; }
            if (item.Temperature == null) { rTemp = 0; Temperature = 0; }
            if (item.Pd == null) { rPd = 0; Pd = 0; }
            if (item.SF6Lost == null) { rSF6Lost = 0; SF6Lost = 0; }
            if (item.HistoryMain == null) { rHis = 0; HistoryMain = 0; }
            if (item.NumberYearOper == null) { rNumberYearOper = 0; NumberYearOper = 0; }

            double sumRate = rOutside + rTemp + rPd + rHis + rSF6Lost + rNumberYearOper;
            Console.WriteLine("sumRate= " + sumRate);
            double scorePPxR = maxScore / (3 * sumRate);
            Console.WriteLine("scorePPxR= " + scorePPxR);
            double Results = Math.Round(Convert.ToSingle(
                (Outside * rOutside
                + Temperature * rTemp
                + Pd * rPd
                + SF6Lost * rSF6Lost
                + HistoryMain * rHis
                + NumberYearOper * rNumberYearOper)
                * scorePPxR), 2);
            Console.WriteLine("Results= " + Results);
            return Results;
        }
        public static double? MCSF6Score23(MCSF6 item)
        {
            return Math.Round(Convert.ToSingle(
                item.RIsolate
                + item.RContact
                + item.TimeCut
                + item.RIsolateClose
                + item.RIsolateCut
                + item.PurifyHuminitySF6
                + item.SF6Decay
                + item.LostSF6
                + item.RIsolateOneWayMotor
                + item.HightVoltageAC
                + item.CutOnline
                + item.RMotor
                + item.SpeedFlowCut
                + item.SF6Analysis
                ), 2);
        }
    }
}