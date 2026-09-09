> ⚠️ Не используйте кнопку "Код → Скачать" на сайте GitFlic – этот метод не загружает файлы из Git LFS. [Инструкция по клонированию](../README_CLONE.md).

### Unity-плагин RuStore для подключения пуш-уведомлений

#### [🔗 Документация разработчика][10]

#### Установка плагина в свой проект

Поддерживаются версии Unity 2022+. Для установки выполните следующие действия.

1. Выполните клонирование репозитрия пакета **RuStore Core**. [Инструкция по клонированию](https://gitflic.ru/project/rustore/unity-rustore-core-sdk/README_CLONE.md).
1. Выполните клонирование репозитрия пакета **RuStore Push**. [Инструкция по клонированию](../README_CLONE.md).
1. Импортируйте пакеты из папок `ru.rustore.core` и `ru.rustore.push` в проект через **Package Manager** (**Window → Package Manager → __+__ → Add package from disk...**).
1. Обновите зависимости проекта с помощью [External Dependency Manager](../README_EDM.md) (**Assets → External Dependency Manager → Android Resolver → Force Resolve**).
1. Выполните шаги раздела [Настройка проекта](../README.md).

#### История изменений

[CHANGELOG](../CHANGELOG.md)

#### Условия распространения

Данное программное обеспечение, включая исходные коды, бинарные библиотеки и другие файлы, распространяется под лицензией MIT. Информация о лицензировании доступна в документе [MIT-LICENSE](../MIT-LICENSE.txt).

#### Техническая поддержка

Дополнительная помощь и инструкции доступны в [документации RuStore](https://www.rustore.ru/help/) и по электронной почте support@rustore.ru.

[10]: https://www.rustore.ru/help/sdk/push-notifications/unity/7-0-0
