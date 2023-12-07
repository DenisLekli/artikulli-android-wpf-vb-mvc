package com.example.produktejava.types;

public class LojiGetNumber {
    public static int getNumber(ELloj eLloj){
        switch (eLloj){
            case I:
                return 0;
            case V:
                return 1;
            default:
                return -1;
        }
    }
}
