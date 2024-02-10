# Как пользоваться

## Как открыть окно
Для этого добавьте в WindowService и вызовите в нем метод Open или Close с id

```C#
    private WindowService _windowService;

    [Inject]
    public void Construct(WindowService windowService, YandexGame monetization)
    {
        _windowService = windowService;
        _windowService.Open(WindowId.Win);
    }
```