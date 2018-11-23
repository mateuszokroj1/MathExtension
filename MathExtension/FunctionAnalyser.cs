using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathExtension.FunctionAnalyser
{
    /// <summary>
    /// Function Analyser
    /// </summary>
    public class Analyser : IDisposable
    {
        #region Constructor
            public Analyser(Func<double,double> f) : this(f, ValueRange.Real)
            { }

            public Analyser(Func<double, double> f, ValueRange inputRange)
            {
                if (f == null) throw new ArgumentNullException("f");
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
            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator<double> IEnumerable<double>.GetEnumerator()
            {
                throw new NotImplementedException();
            }
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
        object IEnumerator.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        double IEnumerator<double>.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        void IDisposable.Dispose()
        {
            
        }

        bool IEnumerator.MoveNext()
        {
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }

    #region Enums
        public enum IncludeValues { None, Min, Max, Both }
        public enum Monotonicity { Costant, Increasing, Decreasing }
        public enum BaseRange { Real, Integer }
    #endregion
}
