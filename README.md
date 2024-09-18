## RuStore Unity плагин для подключения пуш-уведомлений

### [🔗 Документация разработчика][10]

Плагин “RuStorePushClient” предоставляет функциональность для включения в приложение push-уведомлений через сервисы RuStore.

Репозиторий содержит плагины “RuStorePushClient” и “RuStoreCore”, а также демонстрационное приложение с примерами использования и настроек. Поддерживаются версии Unity 2022+.


### Сборка примера приложения

Вы можете ознакомиться с демонстрационным приложением содержащим представление работы всех методов sdk:
- [README](push_example/README.md)
- [push_example](https://gitflic.ru/project/rustore/unity-rustore-push-sdk/file?file=push_example)


### Установка плагина в свой проект

1. Импортируйте пакет Example/RuStorePushSDKExample.unitypackage в проект Unity.

2. Добавьте в проект пакет Newtonsoft Json: Window → Package Manager → + → Add package by name... → введите название пакета com.unity.nuget.newtonsoft-json

3. Откройте настройки проекта: Edit → Project Settings → Player → Android Settings.

4. В pазделе Publishing Settings: включите настройки Custom Main Manifest, Custom Main Gradle Template, Custom Gradle Properties Template. 

5. В разделе Other Settings: настройте package name, Minimum API Level = 24, Target API Level = 34.

6. Откройте настройки External Dependency Manager: Assets → External Dependency Manager → Android Resolver → Settings. Включите настройки Use Jetifier, Patch mainTemplate.gradle, Patch gradleTemplate.properties.

7. Обновите зависимости проекта: Assets → External Dependency Manager → Android Resolver → Force Resolve.


### Пересборка плагина

Если вам необходимо изменить код библиотек плагинов, вы можете внести изменения и пересобрать подключаемые .aar файлы.

1. Откройте в вашей IDE проект Android из папки _“android_libraries”_.

2. Выполните сборку проекта командой gradle assemble.

При успешном выполнении сборки в папке _“ru.rustore.push / Runtime / Android”_ будет обновлен файл `RuStoreUnityPushClient.aar`


### История изменений

[CHANGELOG](ru.rustore.push/CHANGELOG.md)


### Условия распространения

Данное программное обеспечение, включая исходные коды, бинарные библиотеки и другие файлы распространяется под лицензией MIT. Информация о лицензировании доступна в документе [MIT-LICENSE](MIT-LICENSE.txt).


### Техническая поддержка

Дополнительная помощь и инструкции доступны на странице [rustore.ru/help/](https://www.rustore.ru/help/) и по электронной почте [support@rustore.ru](mailto:support@rustore.ru).

[10]: https://www.rustore.ru/help/sdk/push-notifications/unity/6-1-0
