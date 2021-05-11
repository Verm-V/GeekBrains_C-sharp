﻿*[Назад](./../README.md)*  
  
### Задание для урока №7  
  
- [X] 1 Написать любое приложение, произвести его сборку с помощью MSBuild, 
осуществить декомпиляцию с помощью dotPeek, внести правки в программный код и пересобрать приложение.  
- [X] 2 (*) выполнить задание 1, используя вместо dotPeek инструменты ildasm/ilasm.  
  
---  
  
### Пояснения по выполнению задания  
  
Для выполнения задания создана простая программа рисующая квадрат с заданного размера в консоли.  
Максимальный размер квадрата задается readonly полем MAX_LENGTH, а при вводе неправильного размера, выводится строка "Ошибка. "  
  
1) После создания проекта, он был собран с помощью msbuild в консоли.  
2) Затем с помощью ildasm был создан дамп il-кода в файле lesson-07.il  
3) Дамп правится в редакторе. Поле MAX_LENGTH меняется с 10 на 16. И инвертируется условие проверки на вывод строки "Ошибка. "  
4) С помощью ilasm, дамп компилируется в файл lesson-07-new.exe  
5) Затем lesson-07-new.exe файл декомпилируется с помощью dotPeek и экспортируется в проект Visual Studios  
  
Весь процесс записан на видео 2мин. (ускорено в 2 раза):  
  
**[https://www.youtube.com/watch?v=tY48KN0IddU](https://www.youtube.com/watch?v=tY48KN0IddU)**  
  