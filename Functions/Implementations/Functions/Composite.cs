﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Functions.Implementations.Intervals;
using Functions.Interfaces;

namespace Functions.Implementations.Functions
    {
    /// <summary>
    /// Представляет собой сложную функцию, состоящую из нескольких других, идущих друг за другом без промежутков в интервалах.
    /// </summary>
    /// <typeparam name="TSpace"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Composite<TSpace, TValue> : IFunction<TSpace, TValue> where TSpace : IComparable<TSpace>
    {
        private readonly List<IFunction<TSpace, TValue>> _functions;
        public TValue Value(TSpace point)
        {
            if(!Interval.Contains(point))
                throw new ArgumentOutOfRangeException();
            return _functions.First(t => t.Interval.Contains(point)).Value(point);
        }

        public IInterval<TSpace> Interval { get; }
        public bool TryUnion(IFunction<TSpace, TValue> function)
        {
            throw new NotImplementedException();
        }

        public IFunction<TSpace, TValue> Union(IFunction<TSpace, TValue> function)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Создаёт сложную функцию, состоящую из нескольких других, объединенных на непрерывном интервале.
        /// </summary>
        /// <param name="functions">Функции, из которых будет состоять создаваемая. Должны быть упорядочены по интервалам, не иметь разрывов между собой, а также не перечекаться.</param>
        public Composite(List<IFunction<TSpace, TValue>> functions)
        {
            _functions = new List<IFunction<TSpace, TValue>>(functions.Count);
            foreach (IFunction<TSpace, TValue> function in functions)
            {
                Interval = Interval?.Union(function.Interval) ?? function.Interval;
                AddFunction(function);
            }
        }
        private void AddFunction(IFunction<TSpace, TValue> function)
        {
            if(_functions.Count == 0 || _functions[_functions.Count-1].Interval.End.Position.CompareTo(function.Interval.Start.Position) == 0 && _functions[_functions.Count - 1].Interval.End.Inclusive != function.Interval.Start.Inclusive)
                _functions.Add(function);
        }
        
    }
}
