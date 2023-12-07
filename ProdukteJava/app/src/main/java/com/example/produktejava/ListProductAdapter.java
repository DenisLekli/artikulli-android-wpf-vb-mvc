package com.example.produktejava;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Callable;
import java.util.function.Consumer;

public class ListProductAdapter extends ArrayAdapter<ViewHolderProducts> {
    private int resourceLayout;
    private Context mContext;
    private List<ViewHolderProducts> itemLists;

    public Callable<Integer> buttonDeleteOnClick;

    public ListProductAdapter(Context context, int resource, List<ViewHolderProducts> items) {
        super(context, resource, items);
        this.resourceLayout = resource;
        this.mContext = context;
        this.itemLists = items;
    }

    @Override
    public ViewHolderProducts getItem(int position) {
        return itemLists.get(position);
    }

    private Consumer<Integer> buttonDeleteOnClickCallback;

    public void buttonDeleteOnClick(Consumer<Integer> buttonDeleteOnClickCallback) {
        this.buttonDeleteOnClickCallback = buttonDeleteOnClickCallback;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        View v = convertView;

        if (v == null) {
            LayoutInflater vi;
            vi = LayoutInflater.from(mContext);
            v = vi.inflate(resourceLayout, null);
        }

        ViewHolderProducts p = getItem(position);


        Button deleteListsButtons = (Button) v.findViewById(R.id.deleteListsButtons);
        if (buttonDeleteOnClickCallback != null)
            deleteListsButtons.setOnClickListener(view -> {
                buttonDeleteOnClickCallback.accept(p.id);
            });

        if (p != null) {
            TextView tt1 = (TextView) v.findViewById(R.id.display_id);
            TextView tt2 = (TextView) v.findViewById(R.id.display_name);

            if (tt1 != null) {
//                tt1.setText(""+p.id);
            }

            if (tt2 != null) {
                tt2.setText(p.name);
            }
        }

        return v;
    }
}
