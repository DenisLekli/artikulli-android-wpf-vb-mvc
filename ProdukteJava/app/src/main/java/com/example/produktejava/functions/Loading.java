package com.example.produktejava.functions;

import android.app.ProgressDialog;
import android.content.Context;

public class Loading {









    private ProgressDialog progress;
    public Loading(Context context,String text,String title){
        progress = new ProgressDialog(context);
        progress.setTitle(title);
        progress.setMessage(text);
        progress.setCancelable(false); // disable dismiss by tapping outside of the dialog
    }
    public void Load(){
        progress.show();
    }
    public void Stop(){
        progress.dismiss();
    }
}
