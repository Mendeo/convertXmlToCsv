# Преобразует экспортированные из MS Access данные в виде xml в формат csv с разделителем ";".

Я заметил, что прямой экспорт таблицы из MS Access в csv приводит к округлению конечных данных, т.е. снижается точность. Однако, этого не происходит при экспорте в xml. Поэтому я написал простенькую программу, которая из xml делает csv.

Для работы программе нужно передать путь к файлу xml. Результат работы сохранится в той же папке.