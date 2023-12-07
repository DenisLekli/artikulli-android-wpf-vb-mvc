package com.example.produktejava.api;

import android.os.StrictMode;

import com.example.produktejava.exeptionMgr.ConnectionExeptions;
import com.example.produktejava.exeptionMgr.HttpStatusError;
import com.example.produktejava.functions.ApiJsonFunctions;
import com.example.produktejava.types.Products;
import com.example.produktejava.types.ProductsApi;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.Reader;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.InetSocketAddress;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.Socket;
import java.net.SocketAddress;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

public class CrudHttpRequests implements CrudInterfaces {
    public static String baseUrl = "http://192.168.0.104:5029";



    @Override
    public List<Products> getProducts() throws HttpStatusError, ConnectionExeptions {
        try {
            ApiJsonFunctions.ChecksOnline(baseUrl);
           String res =  ApiJsonFunctions.ApiJson(baseUrl + "/api/artikulli","GET",null);
            Gson gson = new Gson();
            List<ProductsApi> productsApiLists = gson.fromJson(res.toString(), new TypeToken<List<ProductsApi>>() {
            }.getType());
            return productsApiLists.stream().map(ProductsApi::toProducts).collect(Collectors.toList());
        } catch (MalformedURLException e) {
            throw new RuntimeException(e);
        } catch (ProtocolException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

    }

    @Override
    public void addProducts(Products products) throws HttpStatusError, ConnectionExeptions {
        try {
            String res =  ApiJsonFunctions.ApiJson(baseUrl + "/api/artikulli","POST",products);

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

    @Override
    public Products getProductsId(int id) throws HttpStatusError, ConnectionExeptions {
        try {
            String res =  ApiJsonFunctions.ApiJson(baseUrl + "/api/artikulli" + "/" + id,"GET",null);
            Gson gson = new Gson();
            ProductsApi productsApiLists = gson.fromJson(res, ProductsApi.class);
            return ProductsApi.toProducts(productsApiLists);
        } catch (MalformedURLException e) {
            throw new RuntimeException(e);
        } catch (ProtocolException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public void updateProducts(Products products) throws HttpStatusError, ConnectionExeptions {
        URL url = null;
        try {
            String res =  ApiJsonFunctions.ApiJson(baseUrl + "/api/artikulli" + "/" + products.id,"PUT",products);


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

    @Override
    public List<Products> searchProducts(String names) throws HttpStatusError {
        try {

            if(Objects.equals(names, "")){
                return this.getProducts();
            }
            String res =  ApiJsonFunctions.ApiJson(baseUrl + "/api/artikulli" + "/search/" + names,"GET",null);


            Gson gson = new Gson();
            List<ProductsApi> productsApiLists = gson.fromJson(res, new TypeToken<List<ProductsApi>>() {
            }.getType());

            return productsApiLists.stream().map(ProductsApi::toProducts).collect(Collectors.toList());
        } catch (MalformedURLException e) {
            throw new RuntimeException(e);
        } catch (ProtocolException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        } catch (ConnectionExeptions e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public void deleteProducts(int id) throws HttpStatusError, ConnectionExeptions {
        try {
            String res =  ApiJsonFunctions.ApiJson(baseUrl + "/api/artikulli" + "/" + id,"DELETE",null);
        } catch (MalformedURLException e) {
            throw new RuntimeException(e);
        } catch (ProtocolException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
