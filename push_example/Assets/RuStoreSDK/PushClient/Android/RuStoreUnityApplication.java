package ru.rustore.unitysdk;

import android.app.Application;
import ru.rustore.unitysdk.pushclient.RuStoreUnityPushClient;

public class RuStoreUnityApplication extends Application {

    @Override public void onCreate() {
         super.onCreate();
         RuStoreUnityPushClient.init(this, "unity");
    }
}
