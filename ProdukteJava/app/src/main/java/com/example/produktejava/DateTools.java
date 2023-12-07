package com.example.produktejava;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.time.LocalDate;
import java.time.LocalTime;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Arrays;
import java.util.Date;
import java.util.List;

public class DateTools {
    public static String ToIosString(Date date) {
        return null;
    }


    public static String formatedDisplayDatesToIosString(String date) {
        try {
            List<String> testDates = Arrays.asList(date.split("/"));
            String parsedDates = "";
            for (String p : testDates) {
                parsedDates = p + (!parsedDates.equals("") ? "-" : "") + parsedDates;
            }
            String outDates = parsedDates + "T" + "00:00";
            return outDates;
        } catch (Exception e) {
            return "2023-11-29T00:00";
        }
    }


    public static String iosStringToformatedDisplayDates(String isoString) {
        try {
            String dateParts = isoString.split("T")[0];
            String[] stringsParsed = dateParts.split("-");
            String parsedDates = "";
            for (String p : stringsParsed) {
                parsedDates = p + (!parsedDates.equals("") ? "/" : "") + parsedDates;
            }
            return parsedDates;
        } catch (Exception e) {
            return "";
        }
    }

    private static String b(int n) {
        if (n == 0)
            n += 1;
        String s = "" + n;
        String sout = s;
        if (s.length() == 1) {
            sout = "0" + sout;
        }
        return sout;
    }

    public static String formatedDisplayDates(int d, int m, int y) {
        return b(d) + "/" + b(m) + "/" + b(y);
    }
}
