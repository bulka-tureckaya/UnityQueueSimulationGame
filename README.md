# Создание игры на юнити с имитационным моделированием на основе очередей

## Описание проекта
Данный проект представляет собой игру, созданную с использованием Unity, основанную на имитационном моделировании покупателей и очередей в магазине. Игра позволяет задавать вероятность появления покупателей в минуту и количество касс.

## Функциональность
- Имитация покупателей в магазине.
- Возможность задавать параметры: вероятность появления покупателей и количество касс.
- Визуализация процесса очереди через Unity.

## Используемые технологии
- Unity
- C#
- Visual Studio

## Ход работы
Проект основан на программе, созданной в Visual Studio, которая позволяет наблюдать за нагруженностью касс в магазинах и оценками обслуживания от покупателей. Для запуска программы необходимо ввести два значения: вероятность появления покупателя в следующую минуту в процентах и количество касс.

![Конструктор](https://github.com/user-attachments/assets/b909dabf-11c1-48e0-99e6-c30aeecbab78)  
*Конструктор:* Верхний график отображает кассы и их загруженность. На оси X указаны индексы касс, на оси Y - количество покупателей на каждой из касс. На нижнем графике с течением времени появляются оценки обслуживания от покупателей.

![Пример работы программы](https://github.com/user-attachments/assets/0d950c3a-ef56-4372-ab3c-b33312eaec21)  
*Пример работы программы:* 

## Описание кода
Далее опишу сам код программы.
Основной класс программы — `Form1`, который отвечает за управление элементами интерфейса и выполняет логику имитационного моделирования.

### Конструктор
```csharp
public Form1()
{
    InitializeComponent();
    timer.SynchronizingObject = this;
    timer.Interval = 1000;
    timer.Elapsed += onTimer;
    timer.AutoReset = true;
}```
В конструкторе инициализируется таймер, который запускает метод onTimer каждые 1000 миллисекунд (1 секунда). В программе считаю, что 1 секунда реального времени соответствует 1 минуте программного времени.

### Переменные и коллекции
- List<Queue> cashRegisters — список касс (очередей).
- List<Element> people — список всех покупателей.
- List<Element> shop_people — список покупателей, находящихся в магазине.
- List<int> Estimations — список оценок обслуживания от покупателей.
- int chanceOfBuyerApperience, numberOfCashRegisters — вероятность появления покупателя и количество касс.

### Основные методы
#### private Queue SelectQueue(List<Queue> queues)
Метод выбирает очередь с наименьшим количеством людей.

#### private int Estimate(int time)
Метод возвращает оценку от покупателя в зависимости от времени ожидания в очереди:

- Меньше 5 секунд — 5 (отлично).
- 5-10 секунд — 4 (хорошо).
- 10-13 секунд — 3 (нормально).
- 13-17 секунд — 2 (плохо).
- Больше 17 секунд — 1 (ужасно).
#### public void onTimer(object obj, ElapsedEventArgs e)
Метод, который выполняется каждую секунду. Он отвечает за:

- Обновление графиков.
- Счетчик времени и количество покупателей.
- Генерацию покупателей на основе заданной вероятности.
- Обработку очередей касс: если время ожидания истекло, выходит покупатель и оценивает время ожидания.
- Обновление графиков загруженности касс и оценок обслуживания.
#### private void button1_Click(object sender, EventArgs e)
Событие для старта симуляции:

- Сброс счетчиков и списков.
- Чтение параметров из текстовых полей и инициализация очередей.
#### private void button2_Click(object sender, EventArgs e) и private void button3_Click(object sender, EventArgs e)
Методы для остановки и возобновления таймера соответственно.

### Заключение
Программа моделирует очередь покупателей в магазине, позволяя визуализировать нагрузку на кассы и оценки обслуживания. Код написан с использованием C# и Windows Forms.
