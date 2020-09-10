using Microsoft.AspNetCore.Identity;

namespace System.Collections.Generic
{
    internal class ICollectionDebugView<T>
    {
        private IEnumerable<IdentityError> errors;

        public ICollectionDebugView(IEnumerable<IdentityError> errors)
        {
            this.errors = errors;
        }

        public object Items { get; internal set; }
    }
}