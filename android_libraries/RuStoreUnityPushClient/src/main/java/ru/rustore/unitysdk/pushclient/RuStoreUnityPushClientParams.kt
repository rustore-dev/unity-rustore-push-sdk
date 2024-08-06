package ru.rustore.unitysdk.pushclient

import android.content.Context
import android.util.Log
import com.vk.push.common.clientid.ClientId
import com.vk.push.common.clientid.ClientIdCallback
import ru.rustore.sdk.pushclient.common.logger.Logger
import ru.rustore.sdk.pushclient.provider.AbstractRuStorePushClientParams

class RuStoreUnityPushClientParams(context: Context) : AbstractRuStorePushClientParams(context) {

    private val isTestModeEnabled: Boolean = getTestModeSetting(context)

    override fun getLogger(): Logger = UnityLogger("RuStoreUnityPushClient")

    override fun getTestModeEnabled(): Boolean = isTestModeEnabled

    override fun getClientIdCallback(): ClientIdCallback = ClientIdCallback { RuStoreUnityPushClient.getClientId() }

    private fun getTestModeSetting(context: Context) : Boolean {
        val testMode = context.resources.getIdentifier("rustore_PushClientSettings_testMode", "string", context.packageName)
        return context.getString(testMode).toBoolean()
    }
}