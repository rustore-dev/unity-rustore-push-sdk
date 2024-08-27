## RuStore Unity плагин для подключения пуш-уведомлений

### [🔗 Документация разработчика][10]

- [Условия работы push-уведомлений](#Условия-работы-push-уведомлений)
- [Подготовка требуемых параметров](#Подготовка-требуемых-параметров)
- [Настройка примера приложения](#Настройка-примера-приложения)
- [Сценарий использования](#Сценарий-использования)
- [Условия распространения](#Условия-распространения)
- [Техническая поддержка](#Техническая-поддержка)


### Условия работы push-уведомлений

Для работы push-уведомлений необходимо соблюдение следующих условий:

1. На устройстве пользователя должно быть установлено приложение RuStore.

2. Приложение RuStore должно поддерживать функциональность push-уведомлений.

3. Приложению RuStore разрешен доступ к работе в фоновом режиме.

4. Пользователь должен быть авторизован в приложении RuStore.

5. Отпечаток подписи приложения должен совпадать с отпечатком, добавленным в Консоль RuStore.


### Подготовка требуемых параметров

Перед настройкой примера приложения необходимо подготовить следующие данные:

1. `applicationId` — уникальный идентификатор приложения в системе Android в формате обратного доменного имени (пример: ru.rustore.sdk.example).

2. `*.keystore` — файл ключа, который используется для [подписи и аутентификации Android приложения](https://www.rustore.ru/help/developers/publishing-and-verifying-apps/app-publication/apk-signature/).

3. `projectId` — ID push-проекта из консоли разработчика RuStore. Необходим для формирования push token и [отправки push-уведомлений](https://www.rustore.ru/help/sdk/push-notifications/send-push-notifications) (пример: https://console.rustore.ru/apps/_“your_account_id”_/push/projects/-Yv4b5cM2yfXm0bZyY6Rk7AHX8SrHmLI, `projectId` = -Yv4b5cM2yfXm0bZyY6Rk7AHX8SrHmLI).


### Настройка примера приложения

Для проверки работы приложения вы можете воспользоваться функционалом [тестовых платежей](https://www.rustore.ru/help/developers/monetization/sandbox).

1. Откройте проект Unity из папки _“push_example”_.

2. Откройте настройки RuStore Push SDK: Window → RuStoreSDK → Settings → PushClient.

3. В поле _“VKPNS Project Id”_ укажите значение `projectId` — ID push-проекта из консоли разработчика RuStore.

4. В файле AndroidManifest.xml: Assets/Plugins/Android/ замените строку _“YOUR_PROJECT_ID”_ на значение `projectId`.

5. В разделе Publishing Settings: Edit → Project Settings → Player → Android Settings выберите вариант _“Custom Keystore”_ и задайте параметры “Path / Password”, “Alias / Password” подготовленного файла `*.keystore`.

6. В разделе Other Settings: Edit → Project Settings → Player → Android Settings настройте раздел “Identification”, отметив опцию “Override Default Package Name” и указав `applicationId` в поле “Package Name”.

7. Выполните сборку проекта командой Build: File → Build Settings и проверьте работу приложения.


### Сценарий использования

#### Получение push-токена пользователя

Тап по кнопке `Get push token` выполняет процедуру [получения текущего push-токена пользователя][20]. Push-токен пользователя, полученный в приложении, используется в структуре `message` для [отправки push-уведомлений](https://www.rustore.ru/help/sdk/push-notifications/send-push-notifications).

![Получение push-токена пользователя](images/02_get_push_token.png)


### Условия распространения

Данное программное обеспечение, включая исходные коды, бинарные библиотеки и другие файлы распространяется под лицензией MIT. Информация о лицензировании доступна в документе [MIT-LICENSE](../MIT-LICENSE.txt).


### Техническая поддержка

Дополнительная помощь и инструкции доступны на странице [rustore.ru/help/](https://www.rustore.ru/help/) и по электронной почте [support@rustore.ru](mailto:support@rustore.ru).

[10]: https://www.rustore.ru/help/sdk/push-notifications/unity/6-1-0
[20]: https://www.rustore.ru/help/sdk/push-notifications/unity/6-1-0/#get-push-token
