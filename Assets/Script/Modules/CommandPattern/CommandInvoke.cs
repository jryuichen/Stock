using System;
using System.Collections.Generic;
using Domain.Common;
using UniRx.Async;

public class CommandInvoke<T> where T : ICommand
{
    // Events
    public class CommandEvent
    {
        // Data
        private readonly List<Action> _handlers = new();

        // Interfaces
        public void AddListener(Action handler)
        {
            _handlers.Add(handler);
        }

        public void RemoveAllListeners()
        {
            _handlers.Clear();
        }

        public void Invoke()
        {
            foreach (var handler in _handlers) handler();
        }
    }

    // Properties
    public CommandEvent FinalCommandExecuted { get; } = new();

    // Data
    private readonly Stack<T> _backCommandStack = new();
    public bool IsAnimLock { get; private set; }
    public bool IsExecuting { get; private set; }

    // Properties
    public bool HasCommands => _backCommandStack.Count > 0;
    public bool HasNoCommands => !HasCommands;
    public int CommandsCount => _backCommandStack.Count;

    // Interfaces
    public void AnimLock() => IsAnimLock = true;

    public void AnimUnLock() => IsAnimLock = false;

    public void AddBackCommand(T command)
    {
        _backCommandStack.Push(command);
    }

    public async UniTask ExecuteNextAsync()
    {
        Contracts.Require(!IsExecuting, "Cannot execute next command while executing");
        Contracts.Require(!IsAnimLock, "Cannot execute next command while anim lock is active");
        Contracts.Require(HasCommands, "Cannot execute next command while there are no commands");
        await ExecuteNextCommand();
        if (HasNoCommands) NotifyFinalCommandExecuted();
    }

    public async UniTask ExecuteToTop()
    {
        await ExecuteToIndex(0);
    }

    // Methods
    private async UniTask ExecuteNextCommand()
    {
        await ExecuteCommandsTo(_backCommandStack.Count - 1);
    }

    private async UniTask ExecuteToIndex(int index)
    {
        if (IsAnimLock) return;
        if (index < 0 || index >= _backCommandStack.Count) return;
        await ExecuteCommandsTo(index);
        if (HasNoCommands) NotifyFinalCommandExecuted();
    }

    private async UniTask ExecuteCommandsTo(int index)
    {
        if (index < 0 || index >= _backCommandStack.Count)
            throw new InvalidOperationException(
                $"Cannot execute command to {index}, since command stack does not contain it");
        IsExecuting = true;
        for (var i = _backCommandStack.Count - 1; i >= index; i--)
        {
            var command = _backCommandStack.Pop();
            await command.Execute().Invoke();
        }

        await UniTask.DelayFrame(1);
        IsExecuting = false;
    }

    private void NotifyFinalCommandExecuted()
    {
        FinalCommandExecuted.Invoke();
    }
}