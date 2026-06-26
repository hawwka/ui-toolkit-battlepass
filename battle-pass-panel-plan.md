---
name: Battle Pass Panel
overview: Спроектировать окно боевого пропуска как отдельную MVVM-панель UI Toolkit, совместимую с текущей архитектурой модальных панелей и готовую к будущей замене локальных данных на backend/downloadable assets.
todos:
  - id: battlepass-data-contracts
    content: Создать модели, SO-конфиги и provider-интерфейсы для данных Battle Pass и состояния пользователя.
    status: pending
  - id: battlepass-mvvm
    content: Реализовать BattlePassPanelViewModel и item/reward ViewModel без зависимости от UI Toolkit.
    status: pending
  - id: battlepass-ui
    content: Создать UXML/USS панели и item template с layout free/level/premium.
    status: pending
  - id: horizontal-scroll
    content: Настроить горизонтальный ScrollView с 10 видимыми item, wheel и drag/swipe input adapter.
    status: pending
  - id: panel-integration
    content: Зарегистрировать Battle Pass как `IPanelView` с `PanelId = battle_pass` и открыть через существующий `OpenPanelButtonAction`.
    status: pending
  - id: validation
    content: Проверить компиляцию, открытие/закрытие, динамическое заполнение и premium lock/unlock состояния.
    status: pending
isProject: false
---

# Промпт для агента-исполнителя: Battle Pass Panel

Работай в Unity-проекте `C:\Projects\ui-toolkit-battlepass`. Используй уже выбранную архитектуру: MVVM, UI Toolkit, модальные панели через `IPanelView`/`IPanelService`, открытие через `OpenPanelButtonAction`. Не ломай главный экран: Battle Pass должен быть отдельной панелью, которую можно открыть как любую другую панель.

## Цель

Реализовать окно боевого пропуска. Панель динамически строит горизонтальную ленту уровней из локального `List`/SO-конфига. Каждый уровень содержит бесплатную награду сверху, номер уровня по центру и платную награду снизу. Платная награда активна только если у пользователя куплен battle pass. Прогресс пользователя пока хранится в ScriptableObject.

## Файлы и папки

Добавь новую область Battle Pass, не смешивай ее с MainScreen:

- `[Assets/_Project/Scripts/UI/BattlePass](Assets/_Project/Scripts/UI/BattlePass)` — View, ViewModel, модели, providers.
- `[Assets/_Project/Scripts/UI/BattlePass/Config](Assets/_Project/Scripts/UI/BattlePass/Config)` — ScriptableObject-конфиги.
- `[Assets/_Project/UI/UXML/BattlePassPanel.uxml](Assets/_Project/UI/UXML/BattlePassPanel.uxml)` — разметка панели.
- `[Assets/_Project/UI/UXML/BattlePassLevelItem.uxml](Assets/_Project/UI/UXML/BattlePassLevelItem.uxml)` — template одного уровня.
- `[Assets/_Project/UI/USS/BattlePassPanel.uss](Assets/_Project/UI/USS/BattlePassPanel.uss)` — стили панели и горизонтальной ленты.
- `[Assets/_Project/Configs/BattlePass](Assets/_Project/Configs/BattlePass)` — тестовые assets данных.

## Модель данных

Создай runtime-модели без зависимости от UI Toolkit:

```csharp
public sealed class BattlePassLevelModel
{
    public int Level { get; }
    public RewardModel FreeReward { get; }
    public RewardModel PremiumReward { get; }
}

public sealed class RewardModel
{
    public string Id { get; }
    public string Title { get; }
    public string IconAssetKey { get; }
    public int Amount { get; }
}

public sealed class UserBattlePassState
{
    public bool HasPremiumPass { get; }
    public int CurrentLevel { get; }
}
```

Для текущего этапа сделай SO-конфиги:

- `BattlePassConfig` с `List<BattlePassLevelConfig>`.
- `BattlePassUserStateConfig` с `HasPremiumPass` и `CurrentLevel`.
- `RewardConfig`/вложенная serializable-модель для наград.

Важно: UI не должен читать SO напрямую. SO читает provider/repository, затем отдает чистые runtime-модели во ViewModel.

## Data access с заделом под backend

Добавь интерфейсы:

```csharp
public interface IBattlePassDataProvider
{
    IReadOnlyList<BattlePassLevelModel> GetLevels();
}

public interface IUserBattlePassStateProvider
{
    UserBattlePassState GetState();
}

public interface IRewardIconProvider
{
    Sprite GetIcon(string assetKey);
}
```

На текущем этапе реализуй:

- `ScriptableObjectBattlePassDataProvider` — читает `BattlePassConfig`.
- `ScriptableObjectUserBattlePassStateProvider` — читает `BattlePassUserStateConfig`.
- `ResourcesOrSerializedRewardIconProvider` либо простой provider через сериализованный список иконок.

Не закладывай backend-код сейчас. Просто держи интерфейсы так, чтобы позже добавить `RemoteBattlePassDataProvider` и addressables/downloadable assets provider без изменения View/ViewModel.

## MVVM

Сделай `BattlePassPanelViewModel`, который получает данные только через providers:

- строит `IReadOnlyList<BattlePassLevelItemViewModel>`;
- вычисляет `IsFreeUnlocked` по `CurrentLevel >= Level`;
- вычисляет `IsPremiumUnlocked` по `HasPremiumPass && CurrentLevel >= Level`;
- хранит `HasPremiumPass`, `CurrentLevel` как данные состояния;
- не содержит `VisualElement`, `Button`, `Sprite` loading logic напрямую, кроме передачи icon key во view или через отдельный icon provider.

Модель одного айтема:

```csharp
public sealed class BattlePassLevelItemViewModel
{
    public int Level { get; }
    public RewardViewModel FreeReward { get; }
    public RewardViewModel PremiumReward { get; }
    public bool IsFreeUnlocked { get; }
    public bool IsPremiumUnlocked { get; }
}
```

## View/UI Toolkit

`BattlePassPanelView` должен реализовать `IPanelView`:

- `Root` возвращает корневой `VisualElement` панели;
- `CloseRequested` вызывается при нажатии на крестик;
- `Bind(object viewModel)` принимает `BattlePassPanelViewModel`;
- `OnOpened()` и `OnClosed()` используются только для lifecycle, без бизнес-логики.

UXML панели:

- root panel container;
- header с названием `Battle Pass` и кнопкой закрытия;
- горизонтальный scroll area;
- content row для item views;
- не больше 10 items должно визуально помещаться в viewport панели.

Каждый `BattlePassLevelItem`:

- сверху free reward;
- по центру level label;
- снизу premium reward;
- locked/unlocked состояние через USS-классы;
- premium reward показывает locked state, если нет premium pass или уровень не достигнут.

## Горизонтальный скролл и свайп

Используй UI Toolkit `ScrollView` с горизонтальным направлением:

- `ScrollViewMode.Horizontal`;
- вертикальный скролл отключить;
- ширину айтема рассчитать так, чтобы в панели помещались 10 уровней;
- поддержать mouse wheel: колесико должно двигать горизонтальный scroll offset;
- поддержать drag/swipe пальцем и мышью через pointer events, если стандартного поведения UI Toolkit недостаточно.

Добавь отдельный helper, например `HorizontalScrollInputAdapter`, который привязывается к `ScrollView`. Не зашивай обработку свайпов в ViewModel.

## Интеграция с существующей системой панелей

Добавь регистрацию Battle Pass панели в существующую panel factory/registry:

- `PanelId = "battle_pass"`;
- кнопка главного экрана открывает Battle Pass через `OpenPanelButtonAction`;
- закрытие крестиком должно идти через `CloseRequested`;
- закрытие по backdrop остается ответственностью `ModalOverlayView`/`PanelService`, а не Battle Pass панели.

## Будущее расширение

Сохрани точки расширения:

- backend: заменить `ScriptableObjectBattlePassDataProvider` на `RemoteBattlePassDataProvider`;
- downloadable assets: заменить `IRewardIconProvider` на Addressables/remote catalog provider;
- purchase state: заменить `ScriptableObjectUserBattlePassStateProvider` на account/profile provider;
- claim rewards: позже добавить action/service, но сейчас кнопки claim не реализовывать, если их нет в ТЗ.

## Проверка готовности

После реализации проверь:

- Unity компилируется без ошибок;
- Battle Pass открывается как модальная панель поверх главного экрана;
- крестик закрывает панель;
- backdrop закрывает панель через общий panel service;
- панель строит элементы динамически из локального списка;
- визуально видно 10 уровней одновременно;
- колесико мыши скроллит горизонтально;
- drag/swipe мышью и пальцем двигает ленту;
- premium reward меняет состояние в зависимости от `HasPremiumPass` и `CurrentLevel`;
- ViewModel не зависит от UI Toolkit и ScriptableObject напрямую.