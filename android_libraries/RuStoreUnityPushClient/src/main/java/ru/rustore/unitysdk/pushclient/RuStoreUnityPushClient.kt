package ru.rustore.unitysdk.pushclient

import android.app.Application
import com.vk.push.common.clientid.ClientId
import com.vk.push.common.clientid.ClientIdCallback
import com.vk.push.common.clientid.ClientIdType
import ru.rustore.sdk.core.exception.RuStoreException
import ru.rustore.sdk.pushclient.RuStorePushClient
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

	private var isTestModeEnabled = false

	private var clientId: String? = null
	private var clientIdType: ClientIdType = ClientIdType.GAID

	@JvmStatic
	fun runInTestMode() {
		isTestModeEnabled = true
	}

	fun setErrorHandling(allowErrorHandling: Boolean) {
		this.allowErrorHandling = allowErrorHandling
	}

	fun getErrorHandling() : Boolean {
		return allowErrorHandling
	}

	fun setListener(listener: RuStoreUnityMessagingServiceListener?) {
		this.serviceListener = listener
	}

	fun init(serviceListener: RuStoreUnityMessagingServiceListener?, logListener: UnityLogListener?) {
		val unityApp = PlayerProvider.getCurrentActivity().application
		val allowNativeErrorHandling = unityApp.resources.getIdentifier("rustore_PushClientSettings_allowNativeErrorHandling", "string", unityApp.packageName)
		val allowErrorHandling = unityApp.getString(allowNativeErrorHandling).toBoolean()

		init(allowErrorHandling, serviceListener, logListener)
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

	fun setClientId(clientId: String, clientIdType: String) {
		this.clientId = clientId
		this.clientIdType = ClientIdType.valueOf(clientIdType)
	}

	fun getClientId() : ClientId? {
		clientId?.let {
			return ClientId(it, clientIdType)
		}
		return null
	}

	@JvmStatic
	fun init(application: Application, metricType: String) {
		val projectId = application.resources.getIdentifier("rustore_PushClientSettings_VKPNSProjectId", "string", application.packageName)
		val testMode = application.resources.getIdentifier("rustore_PushClientSettings_testMode", "string", application.packageName)

		isTestModeEnabled = application.getString(testMode).toBoolean()

		init(
			projectId = application.getString(projectId),
			metricType = metricType,
			application = application,
			clientIdCallback = null
		)
	}

	@JvmStatic
	private fun init(projectId: String, metricType: String, application: Application? = null, clientIdCallback: ClientIdCallback? = null) {
		RuStorePushClient.init(
			application = application ?: PlayerProvider.getCurrentActivity().application,
			projectId = projectId,
			internalConfig = mapOf("type" to metricType),
			logger = UnityLogger(this.javaClass.simpleName),
			testModeEnabled = isTestModeEnabled,
			clientIdCallback = clientIdCallback
		)
	}

	private fun handleError(throwable: Throwable) {
		if (allowErrorHandling && throwable is RuStoreException) {
			throwable.resolveForPush(PlayerProvider.getCurrentActivity())
		}
	}

	override fun Log(logString: String?) {
		logListener?.Log(logString)
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
}
