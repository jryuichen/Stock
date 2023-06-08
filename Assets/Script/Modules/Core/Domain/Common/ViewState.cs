using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;

namespace Modules.Core.Domain.Common
{
    public class ViewState : ValueObject
    {
        // Getters
        public bool CanShow => !IsViewing;
        public bool CanHide => IsViewing;

        private bool IsViewing { get; }

        // Interfaces
        public ViewState Show()
        {
            Contracts.Require(CanShow, "Can only show hidden view state");
            return Shown;
        }

        public ViewState Hide()
        {
            Contracts.Require(CanHide, "Can only hide shown view state");
            return Hidden;
        }

        // Constructors
        private ViewState(bool isViewing)
        {
            IsViewing = isViewing;
        }

        // Factories
        public static ViewState Shown => new ViewState(true);
        public static ViewState Hidden => new ViewState(false);

        // Operators
        public static implicit operator bool(ViewState viewState) => viewState.IsViewing;
        public static explicit operator ViewState(bool isViewing) => new ViewState(isViewing);

        // Overrides
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IsViewing;
        }
    }
}