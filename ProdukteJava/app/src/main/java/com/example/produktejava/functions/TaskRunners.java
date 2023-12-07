package com.example.produktejava.functions;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.function.Consumer;
import java.util.function.Supplier;

public class TaskRunners {


    public static <T>void run(Supplier<T> supplier,Consumer<T> tConsumer) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                T hi = supplier.get();
                tConsumer.accept(hi);
            }
        }).start();
    }

    public static void runTask(Runnable runnable) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                runnable.run();
            }
        }).start();
    }
    public static void runTaskNullCallback(Runnable runnable,Runnable callBack) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                runnable.run();
                callBack.run();
            }
        }).start();
    }
    public static <T> T runAwait(Supplier<T> supplier)  {
        List<T> hi = new ArrayList<>();
        CountDownLatch latch = new CountDownLatch(1);
        Thread b = new Thread(new Runnable() {
            @Override
            public void run() {
                hi.add(supplier.get());
                latch.countDown();
            }
        });

        try {
            b.start();
            latch.await();
            return hi.get(0);
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }

    public static void runNullAwait(Runnable supplier)  {
        CountDownLatch latch = new CountDownLatch(1);
        Thread b = new Thread(new Runnable() {
            @Override
            public void run() {
                supplier.run();
                latch.countDown();
            }
        });
        try {
            b.start();
            latch.await();
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }
}
