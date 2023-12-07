package com.example.produktejava.functions;

import com.example.produktejava.exeptionMgr.ConnectionExeptions;
import com.example.produktejava.exeptionMgr.HttpStatusError;
import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.nio.charset.StandardCharsets;

public class ApiJsonFunctions {
    public static boolean itsOnline(String baseUrl) {
//        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
//        StrictMode.setThreadPolicy(policy);
        URL url = null;
        try {
            url = new URL(baseUrl);
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setConnectTimeout(5000);
            urlConnection.connect();
            urlConnection.disconnect();
            return true;
        } catch (IOException e) {
            return false;
        } catch (Exception e) {
            return false;
        }
    }

    public static void ChecksOnline(String baseUrl) throws ConnectionExeptions {
        URL url = null;
        try {
            if (!itsOnline(baseUrl))
                throw new ConnectionExeptions();
        } catch (Exception e) {
            throw new ConnectionExeptions();
        }
    }

    public static <T> String apiImplementation(String urlStrings, String methods, T body) throws HttpStatusError {
        //        return;
        URL url = null;
        try {
            url = new URL(urlStrings);
            HttpURLConnection con = (HttpURLConnection) url.openConnection();
            con.setRequestMethod(methods);
            con.setRequestProperty("Content-Type", "application/json");
            if (body != null) {
                con.setRequestProperty("Accept", "application/json");
                con.setDoOutput(true);
                Gson gson = new Gson();
                String jsonString = gson.toJson(body);
                con.setDoOutput(true);
                try (OutputStream os = con.getOutputStream()) {
                    byte[] input = jsonString.getBytes(StandardCharsets.UTF_8);
                    os.write(input, 0, input.length);
                }
            }

            int resCodes = con.getResponseCode();
            if (resCodes != 200)
                throw new HttpStatusError(resCodes);

            try (BufferedReader br = new BufferedReader(
                    new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8))) {
                StringBuilder response = new StringBuilder();
                String responseLine = null;
                while ((responseLine = br.readLine()) != null) {
                    response.append(responseLine.trim());
                }
                System.out.println(response.toString());
                return response.toString();
            }

        } catch (ProtocolException e) {
            throw new RuntimeException(e);
        } catch (MalformedURLException e) {
            throw new RuntimeException(e);
        } catch (UnsupportedEncodingException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public static <T> String ApiJson(String url, String methods, T body) throws IOException, HttpStatusError, ConnectionExeptions {
        ApiJsonFunctions.ChecksOnline(url);
        if (methods == "GET")
            return ApiJsonFunctions.apiImplementation(url, methods, null);
        if (methods == "POST")
            return ApiJsonFunctions.apiImplementation(url, methods, body);
        if (methods == "PUT")
            return ApiJsonFunctions.apiImplementation(url, methods, body);
        if (methods == "DELETE")
            return ApiJsonFunctions.apiImplementation(url, methods, null);
        throw new IOException("invaled method");
    }
}
