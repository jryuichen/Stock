using System;
using UniRx.Async;

public interface ICommand
{
    Func<UniTask> Execute();
}
