package ru.rustore.unitysdk.pushclient;

import android.content.Context;
import com.vk.push.common.clientid.ClientId;
import com.vk.push.common.clientid.ClientIdCallback;
import com.vk.push.common.clientid.ClientIdType;
import ru.rustore.sdk.pushclient.common.logger.Logger;
import ru.rustore.sdk.pushclient.provider.AbstractRuStorePushClientParams;

public class RuStorePushClientParamsExample extends AbstractRuStorePushClientParams {

	private boolean isTestModeEnabled = false;

    public RuStorePushClientParamsExample(Context context) {
        super(context);
    }

    @Override
    public Logger getLogger() {
        return new UnityLogger("RuStoreUnityPushClient");
    }

    @Override
    public boolean getTestModeEnabled() {
        return isTestModeEnabled;
    }

    @Override
    public ClientIdCallback getClientIdCallback() {
        return () -> new ClientId("your_gaid_or_oaid", ClientIdType.GAID);
    }
}
