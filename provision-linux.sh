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

# Додавання пакету MBulakh версії 1.0.0
#dotnet add package MBulakh --version 1.0.0

# Запуск проекту
dotnet run run lab1 --input LAB1/INPUT.TXT --output LAB1/OUTPUT.TXT