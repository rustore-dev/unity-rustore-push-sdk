> ⚠️ Не используйте кнопку "Код → Скачать" на сайте GitFlic – этот метод не загружает файлы из Git LFS. [Инструкция по клонированию](README_CLONE.md).

### Unity-плагин RuStore для подключения пуш-уведомлений

#### [🔗 Документация разработчика][10]

#### Установка External Dependency Manager

**External Dependency Manager** для Android поставляется в составе пакетов <code>ru.rustore.core-<em>version</em>.tgz</code> и <code>RuStoreUnityPushSDK-<em>version</em>.unitypackage</code>.

При установке плагинов RuStore через `*.unitypackage` **External Dependency Manager** не требует специальной установки. Вы можете сразу перейти к разделу **Настройка External Dependency Manager**, см. ниже.

Для установки из <code>ru.rustore.core-<em>version</em>.tgz</code> выполните следующие действия.

1. Откройте **RuStore Core** в окне менеджера пакетов: **Window → Package Manager → Packages RuStore → RuStore Core**.
1. Перейдите на вкладку **Sample**.
1. Импортируйте сэмпл **External Dependency Manager**.
1. Выполните шаги раздела **Настройка External Dependency Manager**, см. ниже.

Вы также можете установить последнюю версию **External Dependency Manager** из официального репозитория на [GitHub](https://github.com/googlesamples/unity-jar-resolver.git?path=/upm).

#### Настройка External Dependency Manager

Откройте настройки **External Dependency Manager**: **Assets → External Dependency Manager → Android Resolver → Settings**, включите следующие настройки:
   - Use Jetifier.
   - Patch mainTemplate.gradle.
   - Patch gradleTemplate.properties.

#### История изменений

[CHANGELOG](CHANGELOG.md)

#### Условия распространения

Данное программное обеспечение, включая исходные коды, бинарные библиотеки и другие файлы, распространяется под лицензией MIT. Информация о лицензировании доступна в документе [MIT-LICENSE](MIT-LICENSE.txt).

#### Техническая поддержка

Дополнительная помощь и инструкции доступны в [документации RuStore](https://www.rustore.ru/help/) и по электронной почте support@rustore.ru.

[10]: https://www.rustore.ru/help/sdk/push-notifications/unity/6-10-0
