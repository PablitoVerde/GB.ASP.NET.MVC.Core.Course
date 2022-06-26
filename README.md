# GB.ASP.NET.MVC.Core.Course

## Урок 1:
Сделано:
1. Создайте новый проект Web App (Model-View-Controller) 
2. Выведите список товаров из каталога на странице catalog/products
3. ** Добавьте поле с картинкой товара и отобразите товар с картинкой на странице каталога
4. Добавьте страницу добавления новых товаров в каталог

## Урок 2:
Сделано:
1. Сделайте класс Catalog потокобезопасным
2. Создайте собственный потокобезопасный класс ConcurrentList<T>. Чтобы можно было добавлять и удалять элементы из разных потоков без ошибок, а также очищать список.
2.1 ★ Постарайтесь сделать свою коллекцию максимально быстрой
2.2 ★★ Реализуйте возможность обхода вашего класса через цикл foreach
Не сделано:
2.3 ★★★ Добавьте потокобезопасный метод сортировки элементов в вашей коллекции

## Урок 3:
Сделано:
1. Сделайте класс Catalog потокобезопасным с использованием одной из потокобезопасных коллекций. Не забудьте про реализацию метода удаления товара из каталога.

## Урок 4:
Сделано:
1. Реализуйте сервис отправки Email, используя библиотеку MailKit *.
2. При каждом добавлении нового товара в каталог, отправляйте об этом письмо через созданный сервис.
3. Не забудьте про DIP и потокобезопасность.
4. Для авторизации можете использовать следующие данные: *****

## Урок 5:
Сделано:
1. Сделайте класс отправки email’ов настраиваемым через IOptions.
2. Логин и пароль от ящика сохраните в пользовательские секреты.
3. Корректно подключите Serilog к проекту.
Не сделано:
4. (дополнительно) Добавьте 3 попытки отправки email’a о добавленном товаре при помощи Polly. Залогируйте все попытки, обратите внимание на уровни логирования.

## Урок 6:
Сделано:
1. Сделайте сервис отправки писем через EmailKit полностью асинхронным
Не сделано:
2. Упражнение: Напишите асинхронный метод, который будет считывать содержимое файлов, имена которых переданы в params и возвращать ВСЕ строки этих файлов