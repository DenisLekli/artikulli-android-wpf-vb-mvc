package com.example.produktejava;

import android.widget.TextView;

public class ViewHolderProducts {
    public ViewHolderProducts(){
        ///
    }
    public ViewHolderProducts(int id,String name){
        this.id=id;
        this.name=name;
    }

    public int id;
    public String name;
}
