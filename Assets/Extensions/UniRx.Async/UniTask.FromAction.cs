#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace UniRx.Async
{
    public partial struct UniTask
    {
        /// <summary>shorthand of new UniTask(Func[UniTask] factory)</summary>
        public static UniTask Lazy(Func<UniTask> factory)
        {
            return new UniTask(factory);
        }

        public static UniTask LazyAction(Action action)
        {
            return new UniTask(() =>
            {
                var onCompletionSource = new UniTaskCompletionSource();
                try
                {
                    action();
                    onCompletionSource.TrySetResult();
                }
                catch (Exception e)
                {
                    onCompletionSource.TrySetException(e);
                }
                return onCompletionSource.Task;
            });
        }
    }
}
#endif