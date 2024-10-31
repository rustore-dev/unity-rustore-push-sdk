package ru.rustore.unitysdk;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import com.unity3d.player.UnityPlayerActivity;

public class RuStoreUnityActivity extends Activity {
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		// Process your intents here
		
		Intent newIntent = new Intent(this, UnityPlayerActivity.class);
        startActivity(newIntent);
        finish();
	}
}
