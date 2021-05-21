using Iot.Device.Buzzer.Samples;

namespace System
{
    /// <summary>
    /// Provides a type- and memory-safe representation of a contiguous region of arbitrary
    /// </summary>
    [Serializable, CLSCompliant(false)]
    internal readonly ref struct SpanMelodyElement
    {
        private readonly MelodyElement[] _array;
        private readonly int _start;
        private readonly int _length;

        /// <summary>
        /// Creates a new System.Span`1 object over the entirety of a specified array.
        /// </summary>
        /// <param name="array">The array from which to create the System.Span object.</param>
        public SpanMelodyElement(MelodyElement[] array)
        {
            _array = array;
            _length = array != null ? array.Length : 0;
            _start = 0;
        }

        /// <summary>
        /// Creates a new System.Span`1 object that includes a specified number of elements
        /// of an array starting at a specified index.
        /// </summary>
        /// <param name="array">The source array.</param>
        /// <param name="start">The index of the first element to include in the new System.Span</param>
        /// <param name="length">The number of elements to include in the new System.Span</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// array is null, but start or length is non-zero. -or- start is outside the bounds
        /// of the array. -or- start and length exceeds the number of elements in the array.
        /// </exception>
        public SpanMelodyElement(MelodyElement[] array, int start, int length)
        {
            if (array != null)
            {
                if ((length > array.Length - start) || (start >= array.Length))
                {
                    throw new ArgumentOutOfRangeException($"Array length too small");
                }
            }
            else
            {
                if ((start != 0) || (length != 0))
                {
                    throw new ArgumentOutOfRangeException($"Array is null but start and length are not 0");
                }
            }

            _array = array;
            _start = start;
            _length = length;
        }

        /// <summary>
        /// Gets the element at the specified zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the element.</param>
        /// <returns>The element at the specified index.</returns>
        // public ref MelodyElement this[int index] => ref _array[_start + index]; // <= this is not working and raises exception after few access
        public MelodyElement this[int index]
        {
            get
            {
                if (index > _length)
                {
                    throw new ArgumentOutOfRangeException($"Index out of range");
                }

                return _array[_start + index];
            }

            set
            {
                if (index > _length)
                {
                    throw new ArgumentOutOfRangeException($"Index out of range");
                }

                _array[_start + index] = value;
            }
        }

        /// <summary>
        /// Returns an empty System.Span object.
        /// </summary>
        public static SpanMelodyElement Empty => new SpanMelodyElement();

        /// <summary>
        /// Returns the length of the current span.
        /// </summary>
        public int Length => _length;

        /// <summary>
        /// Returns a value that indicates whether the current System.Span is empty.
        /// true if the current span is empty; otherwise, false.
        /// </summary>
        public bool IsEmpty => _length == 0;

        /// <summary>
        /// Copies the contents of this System.Span into a destination System.Span.
        /// </summary>
        /// <param name="destination"> The destination System.Span object.</param>
        /// <exception cref="System.ArgumentException">
        /// destination is shorter than the source System.Span.
        /// </exception>
        public void CopyTo(SpanMelodyElement destination)
        {
            if (destination.Length < _length)
            {
                throw new ArgumentException($"Destination too small");
            }

            for (int i = 0; i < _length; i++)
            {
                destination[i] = _array[_start + i];
            }
        }

        /// <summary>
        /// Forms a slice out of the current span that begins at a specified index.
        /// </summary>
        /// <param name="start">The index at which to begin the slice.</param>
        /// <returns>A span that consists of all elements of the current span from start to the end of the span.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">start is less than zero or greater than System.Span.Length.</exception>
        public SpanMelodyElement Slice(int start)
        {
            if ((start > _length) || (start < 0))
            {
                throw new ArgumentOutOfRangeException($"start is less than zero or greater than length");
            }

            return new SpanMelodyElement(_array, start, _length - start);
        }

        /// <summary>
        /// Forms a slice out of the current span starting at a specified index for a specified length.
        /// </summary>
        /// <param name="start">The index at which to begin this slice.</param>
        /// <param name="length">The desired length for the slice.</param>
        /// <returns>A span that consists of length elements from the current span starting at start.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">start or start + length is less than zero or greater than System.Span.Length.</exception>
        public SpanMelodyElement Slice(int start, int length)
        {
            if ((start < 0) || (length < 0) || (start + length > _length))
            {
                throw new ArgumentOutOfRangeException($"start or start + length is less than zero or greater than length");
            }

            return new SpanMelodyElement(_array, _start + start, length);
        }

        /// <summary>
        /// Copies the contents of this span into a new array.
        /// </summary>
        /// <returns> An array containing the data in the current span.</returns>
        public MelodyElement[] ToArray()
        {
            MelodyElement[] array = new MelodyElement[_length];
            for (int i = 0; i < _length; i++)
            {
                array[i] = _array[_start + i];
            }

            return array;
        }

        public static implicit operator SpanMelodyElement(MelodyElement[] array)
        {
            return new SpanMelodyElement(array);
        }

        /// <summary>
        /// Defines an implicit conversion of a System.Span`1 to a System.ReadOnlySpan.
        /// </summary>
        /// <param name="span">The object to convert to a System.ReadOnlySpan.</param>
        public static implicit operator ReadOnlySpanMelodyElement(SpanMelodyElement span)
        {
            return span;
        }
    }
}