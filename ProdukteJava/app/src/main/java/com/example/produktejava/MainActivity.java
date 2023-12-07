package com.example.produktejava;

import androidx.appcompat.app.AppCompatActivity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

import com.example.produktejava.api.ConfigApi;
import com.example.produktejava.api.CrudHttpRequests;
import com.example.produktejava.exeptionMgr.ConnectionExeptions;
import com.example.produktejava.exeptionMgr.HttpStatusError;
import com.example.produktejava.functions.Loading;
import com.example.produktejava.functions.TaskRunners;
import com.example.produktejava.types.Products;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.snackbar.Snackbar;

import java.io.IOException;
import java.security.Provider;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;
import java.util.function.Supplier;
import java.util.stream.Collectors;

public class MainActivity extends AppCompatActivity {
    Loading loading;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        loading = new Loading(this, "geting articuls", "get articles");
        setContentView(R.layout.activity_main);
        this.initListViewsProducts();
        initPlusButton();
        initSearches();
    }

    private void getListProductsApi(Consumer<ArrayList<ViewHolderProducts>> arrayListProvider) {
        loading.Load();
        TaskRunners.<List<Products>>run(() -> {
            try {
                return ConfigApi.apiRequests().getProducts();
            } catch (ConnectionExeptions e) {
                runOnUiThread(() -> {
                    TextView listView = (TextView) findViewById(R.id.onlines);
                    listView.setText("no connection to :" + CrudHttpRequests.baseUrl);
                });
                return new ArrayList<Products>();
            } catch (HttpStatusError e) {
                openPopups();
                return new ArrayList<>();
            }
        }, (n) -> {
            List<ViewHolderProducts> productsApiLists = n.stream().map(s -> new ViewHolderProducts(s.id, s.Emertimi)).collect(Collectors.toList());
            ArrayList<ViewHolderProducts> products = (ArrayList<ViewHolderProducts>) productsApiLists;
            arrayListProvider.accept(products);
            loading.Stop();
        });
    }


    private void initSearches() {
        Button button = (Button) findViewById(R.id.searchbuttons);
        button.setOnClickListener(view -> initListViewsSearchChanges());
    }

    private ArrayList<ViewHolderProducts> getListProductsSearchesApi() {
        EditText searchBoxes = (EditText) findViewById(R.id.searchBoxes);
        List<ViewHolderProducts> productsApiLists = null;
        try {
            productsApiLists = ConfigApi.apiRequests().searchProducts(searchBoxes.getText().toString()).stream().map(n -> new ViewHolderProducts(n.id, n.Emertimi)).collect(Collectors.toList());
        } catch (Exception e) {
            TextView listView = (TextView) findViewById(R.id.onlines);
            listView.setText("no connection to :" + CrudHttpRequests.baseUrl);
            return new ArrayList<ViewHolderProducts>();
        }
        ArrayList<ViewHolderProducts> products = (ArrayList<ViewHolderProducts>) productsApiLists;
        return products;
    }

    private void initListViewsSearchChanges() {
        loading.Load();
        ArrayList<ViewHolderProducts> products = TaskRunners.runAwait(this::getListProductsSearchesApi);
        loading.Stop();
        ArrayAdapter adapter = new ListProductAdapter(this, R.layout.listsproductsviews, products);
        ListView listView = (ListView) findViewById(R.id.ListProducts);
        listView.setAdapter(adapter);
        listView.setOnItemClickListener((adapterView, view, i, l) -> {
            ViewHolderProducts item = (ViewHolderProducts) adapter.getItem(i);
            navigateToPageChange(item.id);
        });
    }

    private void initListViewsProducts() {
        getListProductsApi(products -> {
            runOnUiThread(() -> {
                ListView listView = (ListView) findViewById(R.id.ListProducts);
                ListProductAdapter adapter = new ListProductAdapter(this, R.layout.listsproductsviews, products);
                listView.setOnItemClickListener((adapterView, view, i, l) -> {
                    ViewHolderProducts item = (ViewHolderProducts) adapter.getItem(i);
                    navigateToPageChange(item.id);
                });
                listView.setAdapter(adapter);

                adapter.buttonDeleteOnClick(this::DeletePosts);
//                Button deleteListsButtons = (Button) findViewById(R.id.deleteListsButtons);
//                deleteListsButtons.setOnClickListener(view->{
//                    String hi="";
//                });
            });
        });

    }


    private void DeletePosts(int postsId) {
        TaskRunners.runTaskNullCallback(
                () -> {
                    try {
                        ConfigApi.apiRequests().deleteProducts(postsId);
                    } catch (HttpStatusError e) {
                        openPopups();
                    } catch (ConnectionExeptions e) {
                        throw new RuntimeException(e);
                    }
                },
                () -> {
                    runOnUiThread(() -> {
                        this.initListViewsProducts();
                    });
                }
        );
    }

    private void initPlusButton() {
        FloatingActionButton button = (FloatingActionButton) findViewById(R.id.floatingActionButton);
        button.setOnClickListener(view -> navigateToPageAdd());
    }

    private void navigateToPageAdd() {
        Intent i = new Intent(MainActivity.this, AddActivitys.class);
        startActivity(i);
    }

    private void navigateToPageChange(int id) {
        Intent i = new Intent(MainActivity.this, EditActivity.class);
        i.putExtra("id", id);
        startActivity(i);
    }
    private void openPopups() {
        Snackbar.make(getWindow().getDecorView().getRootView(), "api error", Snackbar.LENGTH_LONG).show();
    }

}