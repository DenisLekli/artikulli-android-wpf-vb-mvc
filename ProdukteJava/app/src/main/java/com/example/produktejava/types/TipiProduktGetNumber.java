package com.example.produktejava.types;

public class TipiProduktGetNumber {
    public static int getNumber(ETipiProdukt eTipiProdukt){
        switch (eTipiProdukt){
            case Ushqimore:
                return 0;
            case Bulmet:
                return 1;
            case Pije:
                return 2;
            case Embelsire:
                return 3;
            default:
                return -1;
        }
    }
}
