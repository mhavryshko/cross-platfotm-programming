#!/bin/bash

# Оновлення та встановлення .NET SDK на Ubuntu 20.04
sudo apt-get update
sudo apt-get install -y wget apt-transport-https
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Перехід до директорії проекту
cd /vagrant

# Додавання джерела для BaGet (приватний NuGet репозиторій)
dotnet nuget add source http://localhost:5000/v3/index.json --name BaGet --store-password-in-clear-text

# Перевірка наявності BaGet як джерела
dotnet nuget list source

# Додавання пакету MHavryshko версії 1.0.0 з BaGet
dotnet add package MHavryshko --version 1.0.0 --source BaGet

# Запуск проекту
dotnet run run lab1 --input LAB1/INPUT.TXT --output LAB1/OUTPUT.TXT
