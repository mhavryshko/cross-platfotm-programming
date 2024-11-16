#!/bin/bash

# Встановлення Homebrew, якщо його ще немає
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Встановлення .NET SDK через Homebrew
brew install --cask dotnet-sdk

# Перехід до директорії проекту і запуск
cd /vagrant
dotnet run run lab1 --input LAB1/INPUT.TXT --output LAB1/OUTPUT.TXT