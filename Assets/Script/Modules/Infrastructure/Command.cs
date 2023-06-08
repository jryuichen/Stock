using System;
using UniRx.Async;

namespace Modules.Core.Infrastructure
{
    public interface ICommand
    {
        void Execute(object parameter);
        void CheckExecute(object parameter);
    }

    public class Command<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Func<T, UniTask> _execute;


        public Command(Action<T> execute) : this(_ => true, execute)
        {
        }

        public Command(Func<T, bool> canExecute, Action<T> execute)
        {
            _execute = async arg => execute(arg);
            _canExecute = canExecute;
        }

        public Command(Func<T, UniTask> execute) : this(_ => true, execute)
        {
        }

        public Command(Func<T, bool> canExecute, Func<T, UniTask> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            if (parameter == null && typeof(T).IsValueType) return false;
            return _canExecute((T)parameter);
        }


        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void CheckExecute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            Execute(parameter);
        }

        public UniTask ExecuteAsync(object parameter) => _execute((T)parameter);

        public async UniTask CheckExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter)) return;
            await ExecuteAsync(parameter);
        }
    }


    public class Command : Command<object>
    {
        public Command(Func<bool> canExecute, Action execute) : base(_ => canExecute(), _ => execute())
        {
        }


        public Command(Action execute) : this(() => true, execute)
        {
        }

        public Command(Func<bool> canExecute, Func<UniTask> execute) : base(_ => canExecute(), _ => execute())
        {
        }


        public Command(Func<UniTask> execute) : this(() => true, execute)
        {
        }

        public void Execute()
        {
            base.Execute(null);
        }

        public void CheckExecute()
        {
            base.CheckExecute(null);
        }

        public UniTask ExecuteAsync() => base.ExecuteAsync(null);

        public UniTask CheckExecuteAsync() => base.CheckExecuteAsync(null);
    }
}