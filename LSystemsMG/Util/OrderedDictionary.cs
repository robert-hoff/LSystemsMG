using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

/*
 * this garbage is from https://github.com/mattmc3/dotmore
 *
 */
namespace LSystemsMG.Util
{
    public class OrderedDictionary<TKey, TValue> : IOrderedDictionary<TKey, TValue>
    {
        private KeyedCollection2<TKey, KeyValuePair<TKey, TValue>> _keyedCollection;

        public TValue this[TKey key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                SetValue(key, value);
            }
        }

        public TValue this[int index]
        {
            get
            {
                return GetItem(index).Value;
            }
            set
            {
                SetItem(index, value);
            }
        }

        public int Count
        {
            get { return _keyedCollection.Count; }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return _keyedCollection.Select(x => x.Key).ToList();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return _keyedCollection.Select(x => x.Value).ToList();
            }
        }

        /// <summary>
        /// Gets the key comparer for this dictionary
        /// </summary>
        public IEqualityComparer<TKey> Comparer
        {
            get;
            private set;
        }

        public OrderedDictionary()
        {
            Initialize();
        }

        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            Initialize(comparer);
        }

        public OrderedDictionary(IOrderedDictionary<TKey, TValue> dictionary)
        {
            Initialize();
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                _keyedCollection.Add(pair);
            }
        }

        public OrderedDictionary(IOrderedDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            Initialize(comparer);
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                _keyedCollection.Add(pair);
            }
        }

        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            Initialize();
            foreach (KeyValuePair<TKey, TValue> pair in items)
            {
                _keyedCollection.Add(pair);
            }
        }

        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> comparer)
        {
            Initialize(comparer);
            foreach (KeyValuePair<TKey, TValue> pair in items)
            {
                _keyedCollection.Add(pair);
            }
        }

        private void Initialize(IEqualityComparer<TKey> comparer = null)
        {
            Comparer = comparer;
            if (comparer != null)
            {
                _keyedCollection = new KeyedCollection2<TKey, KeyValuePair<TKey, TValue>>(x => x.Key, comparer);
            }
            else
            {
                _keyedCollection = new KeyedCollection2<TKey, KeyValuePair<TKey, TValue>>(x => x.Key);
            }
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.  The value can be null for reference types.</param>
        public void Add(TKey key, TValue value)
        {
            _keyedCollection.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Removes all keys and values from this object.
        /// </summary>
        public void Clear()
        {
            _keyedCollection.Clear();
        }

        /// <summary>
        /// Inserts a new key-value pair at the index specified.
        /// </summary>
        /// <param name="index">The insertion index.  This value must be between 0 and the count of items in this object.</param>
        /// <param name="key">A unique key for the element to add</param>
        /// <param name="value">The value of the element to add.  Can be null for reference types.</param>
        public void Insert(int index, TKey key, TValue value)
        {
            _keyedCollection.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Gets the index of the key specified.
        /// </summary>
        /// <param name="key">The key whose index will be located</param>
        /// <returns>Returns the index of the key specified if found.  Returns -1 if the key could not be located.</returns>
        public int IndexOf(TKey key)
        {
            if (_keyedCollection.Contains(key))
            {
                return _keyedCollection.IndexOf(_keyedCollection[key]);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Determines whether this object contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate in this object.</param>
        /// <returns>True if the value is found.  False otherwise.</returns>
        public bool ContainsValue(TValue value)
        {
            return Values.Contains(value);
        }

        /// <summary>
        /// Determines whether this object contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate in this object.</param>
        /// <param name="comparer">The equality comparer used to locate the specified value in this object.</param>
        /// <returns>True if the value is found.  False otherwise.</returns>
        public bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer)
        {
            return Values.Contains(value, comparer);
        }

        /// <summary>
        /// Determines whether this object contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in this object.</param>
        /// <returns>True if the key is found.  False otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return _keyedCollection.Contains(key);
        }

        /// <summary>
        /// Returns the KeyValuePair at the index specified.
        /// </summary>
        /// <param name="index">The index of the KeyValuePair desired</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the index specified does not refer to a KeyValuePair in this object
        /// </exception>
        public KeyValuePair<TKey, TValue> GetItem(int index)
        {
            if (index < 0 || index >= _keyedCollection.Count)
            {
                throw new ArgumentException($"The index was outside the bounds of the dictionary: {index}");
            }
            return _keyedCollection[index];
        }

        /// <summary>
        /// Sets the value at the index specified.
        /// </summary>
        /// <param name="index">The index of the value desired</param>
        /// <param name="value">The value to set</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the index specified does not refer to a KeyValuePair in this object
        /// </exception>
        public void SetItem(int index, TValue value)
        {
            if (index < 0 || index >= _keyedCollection.Count)
            {
                throw new ArgumentException($"The index is outside the bounds of the dictionary: {index}");
            }
            var kvp = new KeyValuePair<TKey, TValue>(_keyedCollection[index].Key, value);
            _keyedCollection[index] = kvp;
        }

        /// <summary>
        /// Returns an enumerator that iterates through all the KeyValuePairs in this object.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _keyedCollection.GetEnumerator();
        }

        /// <summary>
        /// Removes the key-value pair for the specified key.
        /// </summary>
        /// <param name="key">The key to remove from the dictionary.</param>
        /// <returns>True if the item specified existed and the removal was successful.  False otherwise.</returns>
        public bool Remove(TKey key)
        {
            return _keyedCollection.Remove(key);
        }

        /// <summary>
        /// Removes the key-value pair at the specified index.
        /// </summary>
        /// <param name="index">The index of the key-value pair to remove from the dictionary.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _keyedCollection.Count)
            {
                throw new ArgumentException($"The index was outside the bounds of the dictionary: {index}");
            }
            _keyedCollection.RemoveAt(index);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value to get.</param>
        public TValue GetValue(TKey key)
        {
            if (_keyedCollection.Contains(key) == false)
            {
                throw new ArgumentException($"The given key is not present in the dictionary: {key}");
            }
            var kvp = _keyedCollection[key];
            return kvp.Value;
        }

        /// <summary>
        /// Sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The the value to set.</param>
        public void SetValue(TKey key, TValue value)
        {
            var kvp = new KeyValuePair<TKey, TValue>(key, value);
            var idx = IndexOf(key);
            if (idx > -1)
            {
                _keyedCollection[idx] = kvp;
            }
            else
            {
                _keyedCollection.Add(kvp);
            }
        }

        /// <summary>
        /// Tries to get the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the desired element.</param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified key if
        /// that key was found.  Otherwise it will contain the default value for parameter's type.
        /// This parameter should be provided uninitialized.
        /// </param>
        /// <returns>True if the value was found.  False otherwise.</returns>
        /// <remarks></remarks>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_keyedCollection.Contains(key))
            {
                value = _keyedCollection[key].Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public void SortKeys()
        {
            _keyedCollection.SortByKeys();
        }

        public void SortKeys(IComparer<TKey> comparer)
        {
            _keyedCollection.SortByKeys(comparer);
        }

        public void SortKeys(Comparison<TKey> comparison)
        {
            _keyedCollection.SortByKeys(comparison);
        }

        public void SortValues()
        {
            var comparer = Comparer<TValue>.Default;
            SortValues(comparer);
        }

        public void SortValues(IComparer<TValue> comparer)
        {
            _keyedCollection.Sort((x, y) => comparer.Compare(x.Value, y.Value));
        }

        public void SortValues(Comparison<TValue> comparison)
        {
            _keyedCollection.Sort((x, y) => comparison(x.Value, y.Value));
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            Add(key, value);
        }

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return ContainsKey(key);
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return TryGetValue(key, out value);
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return Values; }
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return this[key];
            }
            set
            {
                this[key] = value;
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            _keyedCollection.Add(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            _keyedCollection.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _keyedCollection.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _keyedCollection.CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            get { return _keyedCollection.Count; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return _keyedCollection.Remove(item);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
        {
            return new DictionaryEnumerator<TKey, TValue>(this);
        }

        void IOrderedDictionary.Insert(int index, object key, object value)
        {
            Insert(index, (TKey) key, (TValue) value);
        }

        void IOrderedDictionary.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        object IOrderedDictionary.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (TValue) value;
            }
        }

        void IDictionary.Add(object key, object value)
        {
            Add((TKey) key, (TValue) value);
        }

        void IDictionary.Clear()
        {
            Clear();
        }

        bool IDictionary.Contains(object key)
        {
            return _keyedCollection.Contains((TKey) key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new DictionaryEnumerator<TKey, TValue>(this);
        }

        bool IDictionary.IsFixedSize
        {
            get { return false; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return false; }
        }

        ICollection IDictionary.Keys
        {
            get { return (ICollection) Keys; }
        }

        void IDictionary.Remove(object key)
        {
            Remove((TKey) key);
        }

        ICollection IDictionary.Values
        {
            get { return (ICollection) Values; }
        }

        object IDictionary.this[object key]
        {
            get
            {
                return this[(TKey) key];
            }
            set
            {
                this[(TKey) key] = (TValue) value;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection) _keyedCollection).CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return ((ICollection) _keyedCollection).Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection) _keyedCollection).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection) _keyedCollection).SyncRoot; }
        }
    }

    public class KeyedCollection2<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private const string DelegateNullExceptionMessage = "Delegate passed cannot be null";
        private Func<TItem, TKey> _getKeyForItemFunction;

        public KeyedCollection2(Func<TItem, TKey> getKeyForItemFunction) : base()
        {
            if (getKeyForItemFunction == null)
                throw new ArgumentNullException(DelegateNullExceptionMessage);
            _getKeyForItemFunction = getKeyForItemFunction;
        }

        public KeyedCollection2(Func<TItem, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> comparer) : base(comparer)
        {
            if (getKeyForItemDelegate == null)
                throw new ArgumentNullException(DelegateNullExceptionMessage);
            _getKeyForItemFunction = getKeyForItemDelegate;
        }

        protected override TKey GetKeyForItem(TItem item)
        {
            return _getKeyForItemFunction(item);
        }

        public void SortByKeys()
        {
            var comparer = Comparer<TKey>.Default;
            SortByKeys(comparer);
        }

        public void SortByKeys(IComparer<TKey> keyComparer)
        {
            var comparer = new Comparer2<TItem>((x, y) => keyComparer.Compare(GetKeyForItem(x), GetKeyForItem(y)));
            Sort(comparer);
        }

        public void SortByKeys(Comparison<TKey> keyComparison)
        {
            var comparer = new Comparer2<TItem>((x, y) => keyComparison(GetKeyForItem(x), GetKeyForItem(y)));
            Sort(comparer);
        }

        public void Sort()
        {
            var comparer = Comparer<TItem>.Default;
            Sort(comparer);
        }

        public void Sort(Comparison<TItem> comparison)
        {
            var newComparer = new Comparer2<TItem>((x, y) => comparison(x, y));
            Sort(newComparer);
        }

        public void Sort(IComparer<TItem> comparer)
        {
            List<TItem> list = Items as List<TItem>;
            if (list != null)
            {
                list.Sort(comparer);
            }
        }
    }

    public interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IOrderedDictionary
    {
        new TValue this[int index] { get; set; }
        new TValue this[TKey key] { get; set; }
        new int Count { get; }
        new ICollection<TKey> Keys { get; }
        new ICollection<TValue> Values { get; }
        new void Add(TKey key, TValue value);
        new void Clear();
        void Insert(int index, TKey key, TValue value);
        int IndexOf(TKey key);
        bool ContainsValue(TValue value);
        bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer);
        new bool ContainsKey(TKey key);
        new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();
        new bool Remove(TKey key);
        new void RemoveAt(int index);
        new bool TryGetValue(TKey key, out TValue value);
        TValue GetValue(TKey key);
        void SetValue(TKey key, TValue value);
        KeyValuePair<TKey, TValue> GetItem(int index);
        void SetItem(int index, TValue value);
    }

    public class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IDisposable
    {
        readonly IEnumerator<KeyValuePair<TKey, TValue>> _impl;
        public void Dispose() { _impl.Dispose(); }
        public DictionaryEnumerator(IDictionary<TKey, TValue> value)
        {
            _impl = value.GetEnumerator();
        }
        public void Reset() { _impl.Reset(); }
        public bool MoveNext() { return _impl.MoveNext(); }
        public DictionaryEntry Entry
        {
            get
            {
                var pair = _impl.Current;
                return new DictionaryEntry(pair.Key, pair.Value);
            }
        }
        public object Key { get { return _impl.Current.Key; } }
        public object Value { get { return _impl.Current.Value; } }
        public object Current { get { return Entry; } }
    }

    public class Comparer2<T> : Comparer<T>
    {
        private readonly Comparison<T> _compareFunction;

        public Comparer2(Comparison<T> comparison)
        {
            if (comparison == null)
                throw new ArgumentNullException("comparison");
            _compareFunction = comparison;
        }

        public override int Compare(T arg1, T arg2)
        {
            return _compareFunction(arg1, arg2);
        }
    }
}

