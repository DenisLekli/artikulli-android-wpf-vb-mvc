package com.example.produktejava.types;

import android.os.Build;

import com.example.produktejava.api.ConfigApi;

import java.time.ZoneOffset;
import java.time.ZonedDateTime;
import java.time.format.DateTimeFormatter;

public class ProductsApi {
    public int id;
    public String emertimi;
    public String njesia;
    public String dataSkadences;
    public double cmimi;
    public int lloj;
    public boolean kaTvsh;
    public int tipi;
    public String barkodKod;


    public static Products toProducts(ProductsApi productsApi) {
        Products products = new Products();
        products.id=productsApi.id;
        products.Emertimi = productsApi.emertimi;
        products.Njesia = productsApi.njesia;
        products.DataSkadences = productsApi.dataSkadences;
        products.Cmimi = productsApi.cmimi;
        products.Lloj = productsApi.lloj;
        products.KaTvsh = productsApi.kaTvsh;
        products.Tipi = productsApi.tipi;
        products.BarkodKod = productsApi.barkodKod;
        return products;
    }

    public static ProductsApi toProductsApi(Products products) {
        ProductsApi productsApi = new ProductsApi();
        productsApi.id=products.id;
        productsApi.emertimi = products.Emertimi;
        productsApi.njesia = products.Njesia;
        productsApi.dataSkadences = products.DataSkadences;
        productsApi.cmimi = products.Cmimi;
        productsApi.lloj = products.Lloj;
        productsApi.kaTvsh = products.KaTvsh;
        productsApi.tipi = products.Tipi;
        productsApi.barkodKod = products.BarkodKod;
        return productsApi;
    }

    public ProductsApi() {
    }

    public ProductsApi(int id, String emertimi, String njesia, String dataSkadences, double cmimi, int lloj, boolean kaTvsh, int tipi, String barkodKod) {
        this.id = id;
        this.emertimi = emertimi;
        this.njesia = njesia;
        this.dataSkadences = dataSkadences;
        this.cmimi = cmimi;
        this.lloj = lloj;
        this.kaTvsh = kaTvsh;
        this.tipi = tipi;
        this.barkodKod = barkodKod;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getEmertimi() {
        return emertimi;
    }

    public void setEmertimi(String emertimi) {
        this.emertimi = emertimi;
    }

    public String getNjesia() {
        return njesia;
    }

    public void setNjesia(String njesia) {
        this.njesia = njesia;
    }

    public String getDataSkadences() {
        return dataSkadences;
    }

    public void setDataSkadences(String dataSkadences) {
        this.dataSkadences = dataSkadences;
    }

    public double getCmimi() {
        return cmimi;
    }

    public void setCmimi(double cmimi) {
        this.cmimi = cmimi;
    }

    public int getLloj() {
        return lloj;
    }

    public void setLloj(int lloj) {
        this.lloj = lloj;
    }

    public boolean isKaTvsh() {
        return kaTvsh;
    }

    public void setKaTvsh(boolean kaTvsh) {
        this.kaTvsh = kaTvsh;
    }

    public int getTipi() {
        return tipi;
    }

    public void setTipi(int tipi) {
        this.tipi = tipi;
    }

    public String getBarkodKod() {
        return barkodKod;
    }

    public void setBarkodKod(String barkodKod) {
        this.barkodKod = barkodKod;
    }
}
