using System;
using UniRx.Async;

[Serializable]
public class BackCommandBase : ICommand
{
    private Func<UniTask> _action;

    public BackCommandBase(Func<UniTask> action)
    {
        _action = action;
    }

    public BackCommandBase(Action action)
    {
        _action = () =>
        {
            action.Invoke();
            return UniTask.CompletedTask;
        };
    }

    public  Func<UniTask> Execute()
    {
        return _action.Invoke;
    }
}