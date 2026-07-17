# Информационная система "Азартные игры"  
  
## Технологический стек
* **Язык:** C#
* **Фреймворк:** .NET 9, ASP.NET Core Web API, WPF
* **База данных:** MySQL, Entity Framework Core
* **Аутентификация:** JWT
  
## Функции  
* Регистрация и авторизация пользователей
* Администратор может просматривать и редактировать данные
* Пользователь может сыграть в игры

## Как запустить:
1. Скачайте репозиторий
2. Убедитесь, что у вас установлен ***<u>Docker</u>*** (если нет, то скачать его можно тут -> https://www.docker.com/products/docker-desktop/)
3. Соберите проект через ***<u>PowerShell</u>*** команду ```docker-compose up -d``` в папке скачанного репозитория
4. Скачайте и разархивируйте клиентские приложения в https://github.com/ABACH-HABAD/Gambling/releases (или соберите их самостоятельно с помощью ***Visual Studio*** или выполнив команду ```dotnet run``` в папке ```src\Gambling.Wpf.Admin``` и ```src\Gambling.Wpf.User```)
6. Приложение готово к использованию
  
## Скриншоты:  
  
Выбор запускаемой версии в Visual Studio:  
<img width="258" height="216" alt="image" src="https://github.com/user-attachments/assets/c60bf40c-bb22-4237-b1ed-1fac9b163b56" />
  
Ярлыки:  
<img width="425" height="134" alt="image" src="https://github.com/user-attachments/assets/f6f0357a-6db2-43c6-970f-1c2db1379433" />  
  
Окно авторизации:  
<img width="791" height="583" alt="image" src="https://github.com/user-attachments/assets/d2698a32-e18d-48c7-80a3-f0d23c101ad0" />  

Окно администратора:  
<img width="1552" height="761" alt="image" src="https://github.com/user-attachments/assets/ade74451-3217-4458-8357-ff7eea08bb95" />  
  
Окно пользователя:  
<img width="1298" height="808" alt="image" src="https://github.com/user-attachments/assets/e75c762c-a018-48c4-b82d-cc51785beac6" />  
