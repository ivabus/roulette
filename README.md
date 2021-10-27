# Рулетка
[![Build](https://github.com/BushchikIvan/roulette/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BushchikIvan/roulette/actions/workflows/dotnet.yml)


## Игра с открытым исходным кодом на VB.NET

Создатель и сопроводитель: Иван Бущик <ivan@bushchik.ru>

Лицензия: MIT

Игра представляет собой Европейскую рулетку в консоли.

Протестировано на macOS-aarch64 (через Rosetta 2), macOS-x64, linux-x86_64, linux-aarch64, linux-docker-x86_64, macOS-docker-aarch64.

## Запуск

Системные требования: macOS или Linux, git или Docker, цветной терминал.

### Docker с последним релизом

    docker run -it bushchikivan/roulette:latest

### Docker с master веткой

    docker run -it bushchikivan/roulette:master
    
### Без Docker

Требуется dotnet-sdk >= 5.

    git clone https://github.com/BushchikIvan/roulette
    cd roulette
    dotnet run

## Скриншоты

![Скриншот 1](screenshots/1.png)
![Скриншот 2](screenshots/2.png)

## Перевод / translation

Игра на данном этапе разработки только на русском языке, перевод на английский планируется.

The game is currently only in Russian, and an English translation is planned.

