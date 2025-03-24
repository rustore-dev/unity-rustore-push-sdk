package ru.rustore.unitysdk.pushclient

import android.app.Application
import com.vk.push.common.clientid.ClientId
import com.vk.push.common.clientid.ClientIdCallback
import com.vk.push.common.clientid.ClientIdType
import ru.rustore.sdk.core.exception.RuStoreException
import ru.rustore.sdk.pushclient.RuStorePushClient
import ru.rustore.sdk.pushclient.common.logger.DefaultLogger
import ru.rustore.sdk.pushclient.common.logger.Logger
import ru.rustore.sdk.pushclient.messaging.exception.RuStorePushClientException
import ru.rustore.sdk.pushclient.messaging.model.RemoteMessage
import ru.rustore.sdk.pushclient.messaging.model.TestNotificationPayload
import ru.rustore.sdk.pushclient.utils.resolveForPush
import ru.rustore.unitysdk.core.PlayerProvider
import ru.rustore.unitysdk.core.callbacks.FeatureAvailabilityListener
import ru.rustore.unitysdk.pushclient.callbacks.DeleteTokenListener
import ru.rustore.unitysdk.pushclient.callbacks.GetTokenListener
import ru.rustore.unitysdk.pushclient.callbacks.RuStoreUnityMessagingServiceListener
import ru.rustore.unitysdk.pushclient.callbacks.SendTestNotificationListener
import ru.rustore.unitysdk.pushclient.callbacks.SubscribeTopicListener
import ru.rustore.unitysdk.pushclient.callbacks.UnityLogListener

object RuStoreUnityPushClient : UnityLogListener  {

	private var allowErrorHandling: Boolean = false
	private var serviceListener: RuStoreUnityMessagingServiceListener? = null

	private var messages: MutableList<RemoteMessage> = ArrayList()
	private var errors: MutableList<RuStorePushClientException> = ArrayList()

	private var logListener: UnityLogListener? = null

	fun setErrorHandling(allowErrorHandling: Boolean) {
		this.allowErrorHandling = allowErrorHandling
	}

	fun getErrorHandling() : Boolean {
		return allowErrorHandling
	}

	fun init(application: Application, projectId: String, loggerMode: RuStoreUnityLoggerMode, loggerTag: String, testModeEnabled: Boolean, clientIdType: RuStoreUnityClientIdType?, clientIdValue: String?) {
		val clientIdCallback = clientIdType?.let { type ->
			clientIdValue?.let { value ->
				ClientIdCallback {
					ClientId(
						clientIdType = when(type) {
							RuStoreUnityClientIdType.GAID -> ClientIdType.GAID
							RuStoreUnityClientIdType.OAID -> ClientIdType.OAID
						},
						clientIdValue = value
					)
				}
			}
		}

		val logger: Logger = when(loggerMode) {
			RuStoreUnityLoggerMode.DEFAULTLOGGER -> DefaultLogger(loggerTag)
			RuStoreUnityLoggerMode.UNITYLOGGER -> UnityLogger(loggerTag)
		}

		RuStorePushClient.init(
			application = application,
			projectId = projectId,
			internalConfig = mapOf("type" to "unity"),
			logger = logger,
			testModeEnabled = testModeEnabled,
			clientIdCallback = clientIdCallback
		)
	}

	fun init(allowNativeErrorHandling: Boolean, serviceListener: RuStoreUnityMessagingServiceListener?, logListener: UnityLogListener?) {
		this.allowErrorHandling = allowNativeErrorHandling

		this.serviceListener = serviceListener
		this.logListener = logListener

		if (messages.isNotEmpty()) {
			messages.forEach {
				serviceListener?.OnMessageReceived(it)
			}
			messages.clear()
		}

		if (errors.isNotEmpty()) {
			serviceListener?.OnError(errors)
			errors.clear()
		}
	}

	fun checkPushAvailability(listener: FeatureAvailabilityListener) {
		RuStorePushClient.checkPushAvailability()
			.addOnSuccessListener { result -> listener.OnSuccess(result) }
			.addOnFailureListener { throwable ->
				handleError(throwable)
				listener.OnFailure(throwable)
			}
	}

	fun getToken(listener: GetTokenListener) {
		RuStorePushClient.getToken()
			.addOnSuccessListener { result -> listener.OnSuccess(result) }
			.addOnFailureListener { throwable ->
				handleError(throwable)
				listener.OnFailure(throwable)
			}
	}

	fun deleteToken(listener: DeleteTokenListener) {
		RuStorePushClient.deleteToken()
			.addOnSuccessListener { listener.OnSuccess() }
			.addOnFailureListener { throwable ->
				handleError(throwable)
				listener.OnFailure(throwable)
			}
	}

	fun subscribeToTopic(topicName: String, listener: SubscribeTopicListener) {
		RuStorePushClient.subscribeToTopic(topicName)
			.addOnSuccessListener { listener.OnSuccess() }
			.addOnFailureListener { throwable ->
				handleError(throwable)
				listener.OnFailure(throwable)
			}
	}

	fun unsubscribeFromTopic(topicName: String, listener: SubscribeTopicListener) {
		RuStorePushClient.unsubscribeFromTopic(topicName)
			.addOnSuccessListener { listener.OnSuccess() }
			.addOnFailureListener { throwable ->
				handleError(throwable)
				listener.OnFailure(throwable)
			}
	}

	fun sendTestNotification(testNotificationPayload: TestNotificationPayload, listener: SendTestNotificationListener) {
		RuStorePushClient.sendTestNotification(testNotificationPayload)
			.addOnSuccessListener { listener.OnSuccess() }
			.addOnFailureListener { throwable ->
				handleError(throwable)
				listener.OnFailure(throwable)
			}
	}

	fun onNewToken(token: String) {
		serviceListener?.OnNewToken(token)
    }

    fun onMessageReceived(message: RemoteMessage) {
		if (serviceListener == null) {
			messages.add(message)
		}
        serviceListener?.OnMessageReceived(message)
    }

    fun onDeletedMessages() {
        serviceListener?.OnDeletedMessages()
    }

    fun onError(errors: List<RuStorePushClientException>) {
		if (serviceListener == null){
			this.errors.addAll(errors)
		}
        serviceListener?.OnError(errors)
    }

	private fun handleError(throwable: Throwable) {
		if (allowErrorHandling && throwable is RuStoreException) {
			throwable.resolveForPush(PlayerProvider.getCurrentActivity())
		}
	}

	override fun Log(logString: String?) {
		logListener?.Log(logString)
	}

	override fun LogInfo(logString: String?) {
		logListener?.LogInfo(logString)
	}

	override fun LogWarning(logString: String?) {
		logListener?.LogWarning(logString)
	}

	override fun LogError(logString: String?) {
		logListener?.LogError(logString)
	}

	override fun LogException(throwable: Throwable?) {
		logListener?.LogException(throwable)
	}

	override fun LogDebug(logString: String?) {
		logListener?.LogDebug(logString)
	}

	override fun LogVerbose(logString: String?) {
		logListener?.LogVerbose(logString)
	}
}
