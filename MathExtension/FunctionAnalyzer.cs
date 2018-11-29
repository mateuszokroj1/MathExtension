using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MathExtension.FunctionAnalyzer
{
    /// <summary>
    /// Function Analyzer
    /// </summary>
    public class Analyzer : IDisposable
    {
        #region Constructor
            public Analyzer(Expression<Func<double,double>> f) : this(f, ValueRange.Real)
            { }

            public Analyzer(Expression<Func<double, double>> f, ValueRange inputRange)
            {
                if (f == null) throw new ArgumentNullException("f");
                /* Checking common expressions */
                if (f.NodeType == ExpressionType.Constant) // x=>1
                {
                    
                }
                else if((f.Body.NodeType == ExpressionType.Parameter && f.Parameters[0].Name == (f.Body as ParameterExpression).Name) || f.Body.NodeType == ExpressionType.ArrayIndex) //x=>x || x=>x[0]
                {

                }
                else if(
                    ((f.Body.NodeType == ExpressionType.Add ||
                      f.Body.NodeType == ExpressionType.Subtract) &&
                    ((f.Body as BinaryExpression).Left.NodeType == ExpressionType.Constant) || (f.Body as BinaryExpression).Left.NodeType == ExpressionType.Parameter)
                    ) // x=>x + 0 || x=>x - 1 || x=>x
                {

                }
            }
        #endregion

        #region Fields
        protected List<double> zeros = new List<double>();
        protected Dictionary<ValueRange, Monotonicity> monotonicity = new Dictionary<ValueRange, Monotonicity>();
        #endregion

        #region Properties
            public Func<double,double> f { get; protected set; }
            public ValueRange Definition { get; protected set; }
            public double[] ZeroSet => this.zeros.ToArray();
            public KeyValuePair<ValueRange, Monotonicity>[] Monotonicity => this.monotonicity.ToArray();
        #endregion

        #region Methods
            void IDisposable.Dispose()
            {
                this.f = null;
                this.Definition = null;
                this.monotonicity.Clear();
                this.zeros.Clear();
            }
        #endregion
    }

    public class ValueRange : IEnumerable<double>
    {
        #region Fields
        protected double min = 0.0;
        protected double max = 0.0;
        #endregion

        #region Properties
        public double Min
        {
            get { return this.min; }
            set
            {
                if (value > this.max) throw new ArgumentException("Minimum must be lower than or equal to a maximum.");
                this.min = value;
            }
        }
        public double Max
        {
            get { return this.max; }
            set
            {
                if (value < this.min) throw new ArgumentException("Maximum must be greater than or equal to a maximum.");
                this.max = value;
            }
        }
        public List<ValueRange> Excludes { get; set; } = new List<ValueRange>();
        public IncludeValues Include { get; set; }
        public BaseRange BaseRange { get; set; } = BaseRange.Real;
        #endregion

        #region Methods
        public bool Contains(double value)
        {
            if (
                value < this.min ||
                (value == this.min && this.Include != IncludeValues.Min && this.Include != IncludeValues.Both) ||
                  this.Excludes.Where(e => e.Contains(value)).Count() == 0 ||
                  value > this.max ||
                  (value == this.max && this.Include != IncludeValues.Max && this.Include != IncludeValues.Both)
                )
                    return false;
                return true;
            }

            IEnumerator IEnumerable.GetEnumerator() => new ValueRangeEnumerator(this);

            IEnumerator<double> IEnumerable<double>.GetEnumerator() => new ValueRangeEnumerator(this);
        #endregion

        #region Static
            public static ValueRange Real => new ValueRange
            {
                Min = double.NegativeInfinity,
                Max = double.PositiveInfinity,
                Include = IncludeValues.None
            };
            public static ValueRange Integer => new ValueRange
            {
                Min = double.NegativeInfinity,
                Max = double.PositiveInfinity,
                Include = IncludeValues.None,
                BaseRange = BaseRange.Integer
            };
            public static ValueRange Natural => new ValueRange
            {
                Min = 0.0,
                Max = double.PositiveInfinity,
                Include = IncludeValues.Min,
                BaseRange = BaseRange.Integer
            };
        #endregion
    }

    /// <summary>
    /// Enumerator of the range
    /// </summary>
    internal class ValueRangeEnumerator : IEnumerator<double>
    {
        public ValueRangeEnumerator(ValueRange Range)
        {
            if (Range == null) throw new ArgumentNullException("Range");
            range = Range;
            if (range.Min == double.NegativeInfinity) range.Min = double.MinValue;
            if (range.Max == double.PositiveInfinity) range.Max = double.MaxValue;
        }
        private ValueRange range;
        double value = double.NegativeInfinity;
        object IEnumerator.Current
        {
            get
            {
                if (double.IsNegativeInfinity(value)) throw new InvalidOperationException();
                return value;
            }
        }

        double IEnumerator<double>.Current
        {
            get
            {
                if (double.IsNegativeInfinity(value)) throw new InvalidOperationException();
                return value;
            }
        }

        protected double Epsilon
        {
            get
            {
                if (range.BaseRange == BaseRange.Integer) return 1;
                else
                    return 0.25;
            }
        }

        void IDisposable.Dispose()
        {
            this.range = null;
        }

        bool IEnumerator.MoveNext()
        {
            if (value == double.MaxValue) return false;
            Increment:
            value += Epsilon;
            if (value > range.Max ||
                    (value == range.Max && range.Include != IncludeValues.Max && range.Include != IncludeValues.Both)) return false;
            if (!range.Contains(value)) goto Increment;
            return true;
        }

        void IEnumerator.Reset() => this.value = double.NegativeInfinity;
    }

    #region Enums
        public enum IncludeValues { None, Min, Max, Both }
        public enum Monotonicity { Costant, Increasing, Decreasing }
        public enum BaseRange { Real, Integer }
    #endregion
}
