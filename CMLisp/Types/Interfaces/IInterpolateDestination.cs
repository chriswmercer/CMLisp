using System;
using System.Collections.Generic;

namespace CMLisp.Types.Interfaces
{
    public interface IInterpolateDestination
    {
        void InterpolateFrom(List<KeyValuePairType> data);
    }
}
