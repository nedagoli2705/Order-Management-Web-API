using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DependencyInjection
{
    public interface IDiContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}
