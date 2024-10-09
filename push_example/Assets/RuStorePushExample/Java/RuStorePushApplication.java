package ru.rustore.unitysdk;

import android.app.Application;
import ru.rustore.unitysdk.pushclient.RuStoreUnityPushClient;
import ru.rustore.unitysdk.pushclient.RuStoreUnityLoggerMode;

public class RuStorePushApplication extends Application {
	public final String PROJECT_ID = "-Yv4b5cM2yfXm0bZyY6Rk7AHX8SrHmLI";
	
	@Override
	public void onCreate() {
		super.onCreate();
		
		RuStoreUnityPushClient.INSTANCE.init(
			this,
			PROJECT_ID,
			RuStoreUnityLoggerMode.UNITYLOGGER,
			"RuStoreUnityPushClient",
			false,
			null,
			null
		);
	}
}
