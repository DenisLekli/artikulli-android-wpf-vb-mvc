package com.example.produktejava.api;

import com.example.produktejava.exeptionMgr.ConnectionExeptions;
import com.example.produktejava.exeptionMgr.HttpStatusError;
import com.example.produktejava.types.Products;
import com.example.produktejava.types.ProductsApi;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.util.List;

public interface CrudInterfaces {
    List<Products> getProducts() throws IOException, ConnectionExeptions, HttpStatusError;
    Products getProductsId(int id) throws HttpStatusError, ConnectionExeptions;


    void addProducts(Products products) throws HttpStatusError, ConnectionExeptions;
    void updateProducts(Products products) throws HttpStatusError, ConnectionExeptions;
    List<Products> searchProducts(String names) throws HttpStatusError;
    void deleteProducts(int id) throws HttpStatusError, ConnectionExeptions;
}
