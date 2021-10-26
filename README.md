# Рулетка
[![Build](https://github.com/BushchikIvan/roulette/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BushchikIvan/roulette/actions/workflows/dotnet.yml)


## Игра с открытым исходным кодом на VB.NET

Создатель и сопроводитель: Иван Бущик <ivan@bushchik.ru>

Лицензия: GNU GPLv3

Игра представляет собой Европейскую рулетку в консоли.

Протестировано на macOS-arm (через Rosetta 2), macOS-x64, linux-x86_64, linux-arm, linux-docker-x86_64.

## Сборка и запуск

Требования macOS | Linux, git | Docker, цветной терминал.

Рекомендуется dotnet-sdk >= 5.

    git clone https://github.com/BushchikIvan/roulette
    cd roulette
    dotnet build

    dotnet run

### Быстрый запуск версии из master ветки с помошью Docker (для x86_64)

    docker run -it bushchikivan/roulette:master
    
### Быстрый запуск последней стабильной версии с помощью Docker (для x86_64)

    docker run -it bushchikivan/roulette:latest

## Скриншоты

![Скриншот 1](/screenshots/1.png?raw=true "Скриншот 1")
![Скриншот 2](/screenshots/2.png?raw=true "Скриншот 2")

## Перевод / translation

Игра на данном этапе разработки только на русском языке, перевод на английский планируется.

The game is currently only in Russian, and an English translation is planned.
