﻿using System;
using System.Collections.Generic;
using System.Linq;
using builder.Codeplex;

namespace builder
{
    /// <summary>
    /// Optional is a switch with two cases:
    ///     case Value
    ///     case Absent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Optional<T>
    {
        public abstract TR Select<TR>(Func<T, TR> then, Func<TR> else_);

        public sealed class Value: Optional<T>
        {
            public override TR Select<TR>(Func<T, TR> then, Func<TR> else_)
            {
                return then(V);
            }

            public Value(T v)
            {
                V = v;
            }

            readonly T V;
        }

        private sealed class AbsentT : Optional<T>
        {
            public override TR Select<TR>(Func<T, TR> then, Func<TR> else_)
            {
                return else_();
            }
        }

        public IEnumerable<T> ToEnum()
        {
            return Select(v => new[] { v }, Enumerable.Empty<T>);
        }

        public static implicit operator Optional<T>(T v)
        {
            return v.OptionalOf();
        }

        public static implicit operator Optional<T>(Optional.AbsentT _)
        {
            return new AbsentT();
        }

        Optional()
        {
        }
    }

    public static class Optional
    {
        public struct AbsentT
        {
        }

        public static readonly AbsentT Absent = new AbsentT();

        public static Optional<T> OptionalOf<T>(this T value)
        {
            return new Optional<T>.Value(value);
        }

        public static Optional<T> FromNullable<T>(this T value)
            where T: class
        {
            return value == null ? Optional.Absent : value.OptionalOf();
        }
    }
}