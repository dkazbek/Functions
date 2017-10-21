﻿using System;
using System.Collections.Generic;
using System.Text;
using Functions.Implementations.Intervals;

namespace Functions.Interfaces
{
    public interface IInterval<TSpace> where TSpace : IComparable<TSpace>
    {
        IntervalEdge<TSpace> Start { get; }
        IntervalEdge<TSpace> End { get; }
        bool Contains(TSpace point);
        bool TryUnion(IInterval<TSpace> interval);
        IInterval<TSpace> Union(IInterval<TSpace> interval);
    }
}
