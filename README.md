# Рулетка
[![Build](https://github.com/BushchikIvan/roulette/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BushchikIvan/roulette/actions/workflows/dotnet.yml)


## Игра с открытым исходным кодом на VB.NET

Создатель и сопроводитель: Иван Бущик <ivan@bushchik.ru>

Лицензия: GNU GPLv3

Игра представляет собой Европейскую рулетку в консоли.

Протестировано на macOS-arm (через Rosetta 2), macOS-x64, linux-x86_64, linux-arm, linux-docker-x86_64.

## Запуск

Системные требования macOS или Linux, git или Docker, цветной терминал.

### Без Docker

Требуется dotnet-sdk >= 5.

    git clone https://github.com/BushchikIvan/roulette
    cd roulette
    dotnet run

### Docker с master веткой(для x86_64)

    docker run -it bushchikivan/roulette:master
    
### Docker с последним релизом (для x86_64)

    docker run -it bushchikivan/roulette:latest

## Скриншоты

![Скриншот 1](screenshots/1.png)
![Скриншот 2](screenshots/2.png)

## Перевод / translation

Игра на данном этапе разработки только на русском языке, перевод на английский планируется.

The game is currently only in Russian, and an English translation is planned.

