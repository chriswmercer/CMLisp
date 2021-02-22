using System;
using System.Collections.Generic;

namespace CMLisp.Types.Interfaces
{
    public interface IInterpolateSource
    {
        List<KeyValuePairType> GetInterpolationData();
    }
}
