package ru.rustore.unitysdk.pushclient

import ru.rustore.sdk.pushclient.messaging.model.TestNotificationPayload
import ru.rustore.unitysdk.pushclient.callbacks.SendTestNotificationListener

class TestNotificationHelper {

	private var data: MutableMap<String, String> = mutableMapOf()

	fun addData(key: String, value: String) {
		data[key] = value
	}

	fun sendTestNotification(title: String, body: String, imgUrl: String, listener: SendTestNotificationListener)  {
		val payload = TestNotificationPayload(
			title = title,
			body = body,
			imgUrl = imgUrl,
			data = data
		)
		RuStoreUnityPushClient.sendTestNotification(payload, listener)
	}
}