using System;
using System.Collections.Generic;
using System.Text;
using COSMOS_RTC = Cosmos.HAL.RTC;

namespace PurpleMoonV2.Hardware
{
    public static class RTC
    {
        // time
        public static int GetHour() { return COSMOS_RTC.Hour; }
        public static int GetMinute() { return COSMOS_RTC.Minute; }
        public static int GetSecond() { return COSMOS_RTC.Second; }
        public static float GetMillisecond() { return (float)COSMOS_RTC.Second * (float)1000; }

        // time - strings
        public static string GetTime() { return COSMOS_RTC.Hour.ToString() + ":" + COSMOS_RTC.Minute.ToString() + ":" + COSMOS_RTC.Second.ToString(); }

        // formatted date
        public static string GetDateFormatted()
        {
            string date = "00/00/0000";
            date = COSMOS_RTC.Month + "/" + COSMOS_RTC.DayOfTheMonth + "/20" + COSMOS_RTC.Year;
            return date;
        }

        // formatted time
        public static string GetTimeFormatted()
        {
            string hour, minute;

            // format hour
            int hr; bool morning = true;
            if (GetHour() <= 12) { hr = GetHour(); if (hr < 11) { morning = true; } }
            else { hr = GetHour() - 12; if (hr < 12) { morning = false; } }

            // format hour
            if (hr < 10) { hour = "0" + hr.ToString(); }
            if (hr == 0) { hour = "12"; }
            else { hour = hr.ToString(); }

            // format minute
            if (COSMOS_RTC.Minute < 10) { minute = "0" + COSMOS_RTC.Minute; }
            else { minute = COSMOS_RTC.Minute.ToString(); }

            // am or pm?
            if (morning) { return hour + ":" + minute + " AM"; }
            else { return hour + ":" + minute + " PM"; }
        }
    }
}
