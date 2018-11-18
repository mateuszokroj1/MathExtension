using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MathExtension
{
    /// <summary>
    /// Ciąg o określonej definicji
    /// </summary>
    public class Sequence : IEnumerable<double>
    {
        /// <summary>
        /// Maksymalna długość ciągu wyliczanego przez Enumerator
        /// </summary>
        [Range(0,long.MaxValue)]
        public ulong DefaultEnumeratorMaxN { get; set; } = 100;

        /// <summary>
        /// Definicja ciągu
        /// </summary>
        public virtual Func<ulong, double> Definition { get; set; } = (n) => n;

        public double this[ulong index] => this.Definition(index+1);

        IEnumerator IEnumerable.GetEnumerator() => new SequenceEnumerator(this);

        IEnumerator<double> IEnumerable<double>.GetEnumerator() => new SequenceEnumerator(this);

        [ReadOnly(true)]
        public static Sequence Fibonacci
        {
            get
            {
                Sequence seq = new Sequence();
                seq.Definition = (n) =>
                {
                    switch (n)
                    {
                        case 1: return 0;
                        case 2: return 1;
                        default: return seq.Definition(n - 2) + seq.Definition(n-1);
                    }
                };
                return seq;
            }
        }
    }

    internal class SequenceEnumerator : IEnumerator<double>
    {
        public SequenceEnumerator(Sequence sequence) : base() { seq = sequence; }

        private Sequence seq;
        private long index = -1;

        object IEnumerator.Current
        {
            get
            {
                if (index == -1) throw new InvalidOperationException("Use MoveNext first.");
                return seq[(ulong)index];
            }
        }

        double IEnumerator<double>.Current
        {
            get
            {
                if (index == -1) throw new InvalidOperationException("Use MoveNext first.");
                return seq[(ulong)index];
            }
        }

        bool IEnumerator.MoveNext()
        {
            if (index == long.MaxValue) return false;
            index++;
            if (index + 1 > (long)seq.DefaultEnumeratorMaxN) return false;
            return true;
        }

        void IEnumerator.Reset() => index = -1;

        #region IDisposable Support
        private bool disposedValue = false; // Aby wykryć nadmiarowe wywołania

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.seq = null;
                }

                disposedValue = true;
            }
        }

        // Ten kod został dodany w celu prawidłowego zaimplementowania wzorca rozporządzającego.
        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

    public class ArythmeticProgression : Sequence
    {
        public ArythmeticProgression() : base() { base.Definition = this.Definition; }
        public double First { get; set; } = 1;
        public double Difference { get; set; } = 1;

        protected new Func<ulong, double> Definition
        {
            get { return (n) => First + Difference * (n-1); }
        }
    }

    public class GeometricProgression : Sequence
    {
        public GeometricProgression() : base() { base.Definition = this.Definition; }

        public double First { get; set; } = 1;

        public double Ratio = 2;

        protected new Func<ulong,double> Definition
        {
            get { return (n) => First * System.Math.Pow(Ratio, n-1); }
        }
    }
}
