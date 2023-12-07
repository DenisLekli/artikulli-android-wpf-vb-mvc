package com.example.produktejava.exeptionMgr;

public class HttpStatusError extends Exception {
    public int errorNumber;
    public String errors;

    public HttpStatusError(int errorNumber) {
        this.errorNumber = errorNumber;
    }

    public HttpStatusError(int errorNumber, String errors) {
        this.errorNumber = errorNumber;
        this.errors = errors;
    }
}
