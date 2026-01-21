using System;

namespace Utils
{
    public sealed class BoolPredicate
    {
        private readonly Func<bool> _eval;

        public BoolPredicate(Func<bool> eval)
        {
            _eval = eval ?? throw new ArgumentNullException(nameof(eval));
        }

        public bool Evaluate() => _eval();

        public static implicit operator BoolPredicate(Func<bool> f)
            => new BoolPredicate(f);
        
        public static implicit operator Func<bool>(BoolPredicate p)
            => p._eval;

        public static BoolPredicate operator &(BoolPredicate a, BoolPredicate b)
            => new BoolPredicate(() => a.Evaluate() && b.Evaluate());

        public static BoolPredicate operator |(BoolPredicate a, BoolPredicate b)
            => new BoolPredicate(() => a.Evaluate() || b.Evaluate());

        public static BoolPredicate operator !(BoolPredicate p)
            => new BoolPredicate(() => !p.Evaluate());

        public static bool operator true(BoolPredicate p)
            => p.Evaluate();

        public static bool operator false(BoolPredicate p)
            => !p.Evaluate();
    }
}