# Battle

[Посмотреть видео](https://drive.google.com/file/d/1onZWPis4KgNYQO3z9Z5GdqTkCJCovLII/view?usp=sharing)

[Скачать](https://drive.google.com/drive/folders/1XZVUzmo5x8lfKoFAUIxVXiWvTxGgXt7i?usp=drive_link)

Игра представляет собой сцену где два персонажа стоят друг на против друга и нажимают на способности по очереди.

1) В игре есть заглушка для демонстрации работы клиент-серверного взаимодействия. Толстый клиент. Клиент отправляет на сервер какой-то ивент после чего сервер информирует второго участника об этом ивенте.
2) В игре используются AssetBundle's. Клиент загружает с условного сервера уже распакованный бандл. В игре 3 бандла - Entities, Abilities, Effects. Abilities содержат ссылку на Effects. В игре есть Abilities которые создают эффекты (Fireball). 
3) Игра использует сериализацию. В игре есть две локали, настройки (локаль по умолчанию и первое число для счетчика) и сейвы. Если сейв есть то по умолчанию настройки берутся из него. Нажав на кнопку Update Content мы перезагружаем ассеты с сервера и перезагружаем локальные настройки.
4) ИИ реализован по принципу рандома.
