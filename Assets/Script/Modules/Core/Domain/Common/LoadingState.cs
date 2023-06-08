using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;

namespace Modules.Core.Domain.Common
{
    public class LoadingState : ValueObject
    {
        // Constants
        private const bool DefaultHasBackground = true;

        // Getters
        public bool CanSetLoading => !IsLoading;
        public bool CanSetNotLoading => IsLoading;
        public bool HasBackground { get; }

        private bool IsLoading { get; }

        // Interfaces
        public LoadingState SetLoading(bool hasBackground = DefaultHasBackground)
        {
            Contracts.Require(CanSetLoading, "Can only set not loading state loading");
            return hasBackground ? LoadingWithBackground : Loading;
        }

        public LoadingState SetNotLoading()
        {
            Contracts.Require(CanSetNotLoading, "Can only loading state not loading");
            return NotLoading;
        }

        // Constructors
        private LoadingState(bool isLoading, bool hasBackground)
        {
            IsLoading = isLoading;
            HasBackground = hasBackground;
        }

        // Factories
        public static LoadingState Loading => new LoadingState(true, false);
        public static LoadingState LoadingWithBackground => new LoadingState(true, true);
        public static LoadingState NotLoading => new LoadingState(false, DefaultHasBackground);

        // Operators
        public static implicit operator bool(LoadingState loadingState) => loadingState.IsLoading;
        public static explicit operator LoadingState(bool isLoading) => new LoadingState(isLoading, DefaultHasBackground);

        // Overrides
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IsLoading;
            yield return HasBackground;
        }
    }
}