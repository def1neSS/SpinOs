# Соревновательно-фановое приложение SpinOS для игры в osu!
## (В режиме разработки) Pre-Alpha 1.0

**Внимание!**

Создатель данного приложения дилетант и просто неуч.
Код приложения представлен в самом худшем виде. Не соблюдено ничего святого, но есть к чему стремиться. Над этим ведутся работы. В худшем случае, возможны баги и вылеты.
В концепции это приложение будет иметь несколько режимов игры, но пока есть только один - SpinOs GameRoLL!!!
<br>
SpinOs GameRoLL!!! - не такой фановый, поэтому следующим я добавлю режим for fun, в котором будут только забавные и "неудобные" задания. При этом каждый игрок на своем ходу сможет сам выбирать какой режим он хочет сыграть.

**Интерфейс главного окна**

![alt text](https://sun9-44.userapi.com/impg/IgcYUiHXyDtnT86-0lQ9PcEq2npsWl3zwYS_sA/0xZ2AEAGOlg.jpg?size=786x493&quality=96&sign=39fbb546eb8254bd471444268b2bcffb&type=album)

По интерфейсу (слева направо / сверху вниз):
- Таблица игроков
- Кнопка начала игры
- Окошко "текущего игрока"
- Кнопки условного присваивания очков ( Не обязательны для использования ) '*'
- Текущий круг
- Инфо-окно (log)
- Кнопка удалить игроков
- Кнопка добавить игроков
- Кнопка загрузки игроков с прошлой игры
- Кнопка запуска режима игры GameRoLL

Игра идет в походовом режиме. Участники добавляются в таблицу и начинается игра.
<br>
'*' Кнопки "условного присваивания очков" добавляют конкретное кол-во очков указанное на них игроку. Являются заменой отигровки любого режима игры (в данный момент только GameRoLL!!!) на ход. К использованию не обязательны. В концепции являются неким режимом игры, где текущему игроку остальные игроки предлагают выполнить какое-либо задание, а за результат начислить выбранное кол-во очков.


## Режим SpinOs GameRoLL!!! :

![alt text](https://sun9-51.userapi.com/impg/so12PMaZOtDrmpnV1Ncr2zW-id7_2aAF9QZ_Yw/pT60OtwKG4U.jpg?size=786x493&quality=96&sign=dfb8894f55232a233296b7480b5474cc&type=album)

От игроков требуется выбрать диапазон исходных сложностей карт, которые они будут играть и выставить значения диапазона в настройках игры - 

![alt text](https://sun9-50.userapi.com/impg/6ZXnK9GNzwWyO1u9A3ma4GmTpkso_iAvGDZCIQ/U0CSlqqUUWE.jpg?size=582x182&quality=96&sign=761c0eb34be925a2c590989f83314b66&type=album)

После добавления участников в таблицу игроков все игроки нумеруются и ходят поочередно (окошко справа сверху от таблицы показывает чей ход в данный момент).

Каждый игрок на своем ходу :

1) Выбирает сложность распределения и производится ролл
2) Роллит себе карту через Random в окне выбора карт osu!
3) Играет карту с модом, выпавшим в ролле
4) Получает очки в соответсвии с критериями


### Критерий "Сложность распределения"
Всего есть 4 режима сложности SpinOs GameRoLL!!!

Каждый режим соответсвует определенному количеству очков.
На разных сложностях есть определенный процент выпадания (%) конкретного мода.

![alt text](https://sun9-49.userapi.com/impg/7mp5ok9rAMYVQqPG8BMMFEMNACoeJ4XPJAk2rA/h11DVvG6GRs.jpg?size=516x217&quality=96&sign=331a76244750be277580dbe9e4f59000&type=album)

### Критерий "Тип результата"
Определяет тип конечного прохождения карты.
Он делится на 2 типа и имеет определенный процент выпадания, независимо от режима сложности.
Каждый критерий имеет множитель, который будет применен к количеству очков выбранного типа сложности.

Типы критерия:
1) Pass (Шанс выпадания 80%)
2) FullCombo (FC) (Шанс выпадания 20%)

Удовлетворение критерию ставит множитель x1.0, то есть игрок получает то количество очков, которое соответствует типу сложности.

Так же есть другие различные исходы:

![alt text](https://sun9-15.userapi.com/impg/3ujafquvrpAz6dAWxiEkKJmo9yjpIXupPUhiZA/mfSA8eFcUNo.jpg?size=652x119&quality=96&sign=7b6ac756eb5dc0c36c7cb01663c2c6cc&type=album)

### Критерий "Разброс точности"
Определяет диапазон процентов, в котором должна быть пройдена карта.
Критерий не рабочий и, в данный момент, не влияет ни на что.

В концепции будет содержать множители к очкам выбранной сложности относительно результирующих процентов точности по карте. При этом множители по "типу результата" ставяться x1.0 для любого исхода в данном критерии.

<br>

### Итоговая формула (не учитывая критерий "Разброс точности") :
### Очки = ( Очки выбранной "Сложности распределения" ) * ( (Исх)ТР X (Итг)ТР )
, где **(Исх)ТР** - исходный(выпавший) "Тип результата", **(Итг)ТР** - итоговый "Тип результата", а **X** - оператор, берущий значение множителя на их перечечении ( таблица "Различных исходов") 
<br>
### Пример
Игрок выбирает сложность "Hard" и ему выпадает мод "HD" с критерием "Тип результата" равным "FC".

Значит игрок должен сыграть после выпавшую карту с модом "HD" на "FC".

Возможные исходы результата прохождения карты игроком при данных критериях (Hard = 2) : 
1) Очки = 2 * ( FC X FC ) = 2 * 1.0 = 2
2) Очки = 2 * ( FC X Pass ) = 2 * 0.25 = 0.5
3) Очки = 2 * ( FC X Fail) = 2 * 0.0 = 0

<br>
<br>

### Итоги

Игра заканчивается в тот момент, когда завершится количество кругов, заданное игроками.

Итогом игры будет служить таблица игроков.

<br>

### Примечание по трансляции ##

На данном этапе, для игры с друзьями потребуется:

а) Человек, который это скачает, запустит и будет транслировать

б) Терпение этого человека

Проблема заключается в том, что если использовать трансляцию Дискорда или любой другой сервис (скорее всего) с захватом приложения для данного мероприятия, то не будет совершаться переключение на выпадающее окно и есть 3 пути :
1) Транслировать весь экран
2) Попробовать использовать не очень приятный способ, который я нашел, с использованием OBS. Придется добавить в сцены 2 окна - окно главное и выпадающее; поставить выпадающее окно на 1 место,а главное на 2 место соответственно; правой кнопкой мыши на экран OBS - показать превью; после этого транслировать в дискорде окно-превью OBS.
Способ глупый и просто тупой, но пока автор приложения еще не решил данный момент ввиду своей некомпетентности.
3) Может вы сами что-то придумаете
