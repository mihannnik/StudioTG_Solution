# StudioTG тестовое задание Сапёр

## Задание

>Каждая игра начинается с указания размера поля width и height, а также количества мин mines_count на нём. Исходная задача не подразумевает ограничений, но для тестовой реализации остановимся на разумном ограничении входных параметров: ширина и высота не более 30, количество мин не более width * height - 1 (всегда должна быть хотя бы одна свободная ячейка).
>
>Далее игроку предлагается в созданной игре (идентификация игры по полученному в ответ game_id) делать ходы, указывая координаты ячейки, которую игрок хочет открыть, а именно - row (номер ряда, начиная с нуля) и col (номер колонки, начиная с нуля).
>
>В ответ на оба метода приходят данные о самой игре: уникальный идентификатор игры game_id, размер поля и количество мин, указанные при создании игры, а также данные о поле field в виде двумерного массива символов (height строк, в каждой по width элементов), где пустые строки " " (пробелы) означают неоткрытые ячейки поля, поля с цифрами от "0" до "8" означают открытые ячейки, где цифры указывают, сколько мин расположено в непосредственной близости от текущей ячейки. Также возвращается параметр completed, указывающий, завершена ли текущая игра.
>
>Игра заканчивается в случае, если пользователь указал на ячейку, где установлена мина (ячейки с минами при этом отмечены символом "X" - латинская заглавная "икс"), либо пользователь открыл все ячейки, не занятые минами (в этом случае мины отмечены "M" - латинская заглавная "эм"). Также при завершении игры должна появиться информация по всем остальным ячейкам - количество мин рядом с каждой из ячеек.
>
>Если в процессе игры пользователь открывает ячейку, рядом с которой нет ни одной мины (то есть ячейка со значением "0"), должны "открыться" все смежные ячейки, рядом с которыми также нет ни одной мины, а также все смежные с ними "числовые" ячейки, рядом с которыми мины есть, с указанием их количества.
>
>Не допускается после завершения игры делать новые ходы, а также повторно проверять уже проверенную ячейку. Эти, а также иные ошибочные ситуации должны возвращать ошибку с кодом 400 Bad Request с текстовым описание ошибки в error.

## О программе

Web API Backend ASP.NET сервер, написанный под спецификацию на .Net 8.

## Требования для запуска

1) Docker Compose
2) git

## Установка

* Клонировать репозиторий в любую папку
    `git clone https://github.com/mihannnik/StudioTG_Solution`

* Настроить переменные среды
    1) Создать копию `.env.sample` и переименовать копию в `.env`
    2) Изменить парамеры, если необходимо

* Собрать и запустить контейнер

    `docker compose up`

    `docker compose up -d` - запуск без захвата консоли

## Использование

После запуска сервер доступен по адресу машины и ожидает соединений на порт `HTTP_PORT` (по умолчанию `5522`).

Приложение ведёт лог в консоль.

Доступна проверка работоспособности при запросе `/health`.

Для доступа к OpenAPI среда приложения должна быть `Development`.
