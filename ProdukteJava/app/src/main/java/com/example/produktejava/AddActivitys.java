package com.example.produktejava;

import androidx.appcompat.app.AppCompatActivity;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;

import com.example.produktejava.api.ConfigApi;
import com.example.produktejava.exeptionMgr.ConnectionExeptions;
import com.example.produktejava.exeptionMgr.HttpStatusError;
import com.example.produktejava.functions.TaskRunners;
import com.example.produktejava.types.ELloj;
import com.example.produktejava.types.ETipiProdukt;
import com.example.produktejava.types.LojiGetNumber;
import com.example.produktejava.types.Products;
import com.example.produktejava.types.TipiProduktGetNumber;
import com.google.android.material.snackbar.Snackbar;

import java.util.Arrays;
import java.util.Objects;

public class AddActivitys extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_activitys);
        initSpinnerLlojDropdown();
        initSpinnerTipiDropdown();
        buttonListeners();
    }

    private void initSpinnerLlojDropdown() {
        String[] hi = Arrays.stream(ELloj.values()).map(Enum::name).toArray(String[]::new);
        Spinner spinner = (Spinner) findViewById(R.id.Lloj);
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(

                this,
                android.R.layout.simple_spinner_item,
                hi
        );
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);

    }

    private void initSpinnerTipiDropdown() {

        String[] hi = Arrays.stream(ETipiProdukt.values()).map(Enum::name).toArray(String[]::new);
        Spinner spinner = (Spinner) findViewById(R.id.Tipi);
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(

                this,
                android.R.layout.simple_spinner_item,
                hi
        );
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);

    }

    private void buttonListeners() {
        Button buttonname = (Button) findViewById(R.id.button);
        Button backButton = (Button) findViewById(R.id.back);

        buttonname.setOnClickListener(view -> Posts());
        TextView DataSkadences = (TextView) findViewById(R.id.DataSkadences);
        DataSkadences.setOnClickListener(view -> openCalendarDialog());
        backButton.setOnClickListener(view -> navigateToPageBack());

    }

    private void openCalendarDialog() {


        TextView DataSkadences = (TextView) findViewById(R.id.DataSkadences);

        DatePickerDialog datePickerDialog = new DatePickerDialog(this, (view, year, monthOfYear, dayOfMonth) -> DataSkadences.setText(DateTools.formatedDisplayDates(dayOfMonth, monthOfYear, year)), 0000, 00, 00);
        datePickerDialog.show();


    }

    private void Posts() {
        TextView Emertimi = (TextView) findViewById(R.id.Emertimi);
        TextView Njesia = (TextView) findViewById(R.id.Njesia);
        TextView DataSkadences = (TextView) findViewById(R.id.DataSkadences);
        TextView Cmimi = (TextView) findViewById(R.id.Cmimi);
        Spinner Lloj = (Spinner) findViewById(R.id.Lloj);
        Switch KaTvsh = (Switch) findViewById(R.id.KaTvsh);
        Spinner Tipi = (Spinner) findViewById(R.id.Tipi);
        TextView BarkodKod = (TextView) findViewById(R.id.BarkodKod);
        if (DataSkadences.getText().toString().equals("")) {
            openPopups();
            return;
        }

        Products products = new Products();
//        products.id = id;
        products.Emertimi = Emertimi.getText().toString();
        products.Njesia = Njesia.getText().toString();
        products.DataSkadences = DateTools.formatedDisplayDatesToIosString(DataSkadences.getText().toString());
        products.Cmimi = (float) Float.parseFloat(Cmimi.getText().toString() != "" ? Cmimi.getText().toString() : "0");
        products.Lloj = LojiGetNumber.getNumber(ELloj.valueOf(Lloj.getSelectedItem().toString()));
        products.KaTvsh = KaTvsh.isChecked();
        products.Tipi = TipiProduktGetNumber.getNumber(ETipiProdukt.valueOf(Tipi.getSelectedItem().toString()));
        products.BarkodKod = BarkodKod.getText().toString();
        if (validatePosts(products)) {
            this.postApi(products);
        } else {
            openPopups();
        }
    }

    private void openPopups() {
        Snackbar.make(getWindow().getDecorView().getRootView(), "invaled inputs", Snackbar.LENGTH_LONG).show();
    }

    private boolean validatePosts(Products products) {
        if (products == null) {
            return false;
        }
        if (Objects.equals(products.Emertimi, ""))
            return false;
        if (Objects.equals(products.BarkodKod, ""))
            return false;
        if (Objects.equals(products.Njesia, ""))
            return false;
        if (Objects.equals(products.DataSkadences, ""))
            return false;
        if (products.Cmimi == 0)
            return false;
        return true;
    }


    private void openPopupsApiErrors() {
        Snackbar.make(getWindow().getDecorView().getRootView(), "api errors", Snackbar.LENGTH_LONG).show();
    }

    private void postApi(Products products) {
        TaskRunners.runTaskNullCallback(
                () -> {
                    try {
                        ConfigApi.apiRequests().addProducts(products);
                    } catch (HttpStatusError e) {
                        openPopupsApiErrors();
                    } catch (ConnectionExeptions e) {
                        openPopupsApiErrors();
                    }
                },
                this::navigateToPageBack
        );
    }

    private void navigateToPageBack() {
        Intent i = new Intent(AddActivitys.this, MainActivity.class);
        startActivity(i);
    }
}