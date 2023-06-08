using System;

namespace Modules.Core.Domain.Logic
{
    public struct Either<TL, TR>
    {
        // Interfaces
        public TL Left { get; }
        public TR Right { get; }
        public bool IsLeft { get; }
        public bool IsRight => !IsLeft;

        public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc) => IsLeft ? leftFunc(Left) : rightFunc(Right);

        // Constructors
        public Either(TL left)
        {
            Left = left;
            Right = default(TR);
            IsLeft = true;
        }

        public Either(TR right)
        {
            Left = default(TL);
            Right = right;
            IsLeft = false;
        }

        // Operators
        public static implicit operator Either<TL, TR>(TL left) => new Either<TL, TR>(left);

        public static implicit operator Either<TL, TR>(TR right) => new Either<TL, TR>(right);
    }
}