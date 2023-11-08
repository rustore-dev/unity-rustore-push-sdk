# RuStore Unity SDK для подключения пуш-уведомлений

## [Документация RuStore Unity SDK для подключения пуш-уведомлений.](https://help.rustore.ru/rustore/for_developers/developer-documentation/sdk_push-notifications/unity)

### Пример внедрения SDK.

Импортируйте пакет Example/RuStorePushSDKExample.unitypackage в новый проект Unity.

Откройте настройки проекта: File -> Edit -> Project Settings -> Player -> Android Settings.
- pаздел Publishing Settings: включите настройки Custom Main Manifest, Custom Main Gradle Template, Custom Gradle Properties Template, настройте keystore для подписи приложения. 
- раздел Other Settings: настройте package name, Minimum API Level = 24, Target API Level = 31 или выше.

Откройте настройки External Dependency Manager: Assets -> External Dependency Manager -> Android Resolver -> Settings
- включите настройки Use Jetifier, Patch mainTemplate.gradle, Patch gradleTemplate.properties.

Обновите зависимости проекта: Assets -> External Dependency Manager -> Android Resolver -> Force Resolve

Откройте настройки RuStore Push SDK: Window -> RuStoreSDK -> Settings -> PushClient. 
- VKPNS Project Id — идентификатор вашего проекта в консоли разработчика RuStore
