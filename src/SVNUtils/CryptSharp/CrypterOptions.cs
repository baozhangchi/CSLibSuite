using System;
using System.Collections;
using System.Collections.Generic;
using SVNUtils.CryptSharp.Internal;

namespace SVNUtils.CryptSharp
{
    /// <summary>
    /// Stores options for the crypt operation.
    /// </summary>
    public class CrypterOptions : IEnumerable<KeyValuePair<CrypterOptionKey, object>>, IEnumerable
    {
        /// <summary>
        /// Sets the value of an option, if the option has not already been set.
        /// </summary>
        /// <param name="key">The key of the option.</param>
        /// <param name="value">The value of the option.</param>
        public void Add(CrypterOptionKey key, object value)
        {
            Check.Null("key", key);
            key.CheckValue(value);
            AboutToChange();
            _options.Add(key, value);
        }

        private void CheckValue(CrypterOptionKey key, object value)
        {
            Check.Null("key", key);
            key.CheckValue(value);
        }

        /// <summary>
        /// Clears all options.
        /// </summary>
        public void Clear()
        {
            AboutToChange();
            _options.Clear();
        }

        /// <summary>
        /// Checks if an option is set.
        /// </summary>
        /// <param name="key">The key of the option.</param>
        /// <returns><c>true</c> if the option is set.</returns>
        public bool ContainsKey(CrypterOptionKey key)
        {
            Check.Null("key", key);
            return _options.ContainsKey(key);
        }

        /// <summary>
        /// Returns an enumerator that iterates through all options.
        /// </summary>
        /// <returns>An enumerator for the options.</returns>
        public IEnumerator<KeyValuePair<CrypterOptionKey, object>> GetEnumerator()
        {
            return _options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the value of an option, if the option is set, or a default value otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the option's value.</typeparam>
        /// <param name="key">The key of the option.</param>
        /// <returns>The option's value.</returns>
        public T GetValue<T>(CrypterOptionKey key)
        {
            return GetValue(key, default(T));
        }

        /// <summary>
        /// Gets the value of an option, if the option is set, or a specified default value otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the option's value.</typeparam>
        /// <param name="key">The key of the option.</param>
        /// <param name="defaultValue">The default value if the option is not set.</param>
        /// <returns>The option's value.</returns>
        public T GetValue<T>(CrypterOptionKey key, T defaultValue)
        {
            object value;
            T result;
            if (!TryGetValue(key, out value))
            {
                result = defaultValue;
            }
            else
            {
                try
                {
                    result = (T)value;
                }
                catch (InvalidCastException)
                {
                    throw Exceptions.Argument(key.Description, "Expected type {0}.", new object[]
                    {
                        typeof(T)
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Clears an option.
        /// </summary>
        /// <param name="key">The key of the option.</param>
        /// <returns><c>true</c> if the option was found and cleared.</returns>
        public bool Remove(CrypterOptionKey key)
        {
            Check.Null("key", key);
            AboutToChange();
            return _options.Remove(key);
        }

        /// <summary>
        /// Gets the value of an option, if the option is set.
        /// </summary>
        /// <param name="key">The key of the option.</param>
        /// <param name="value">The value, or <c>null</c> if the option is not set.</param>
        /// <returns><c>true</c> if the option is set.</returns>
        public bool TryGetValue(CrypterOptionKey key, out object value)
        {
            Check.Null("key", key);
            return _options.TryGetValue(key, out value);
        }

        /// <summary>
        /// The number of options that have been set.
        /// </summary>
        // Token: 0x17000030 RID: 48
        public int Count
        {
            get
            {
                return _options.Count;
            }
        }

        /// <summary>
        /// Gets or sets an option.
        /// </summary>
        /// <param name="key">The key of the option.</param>
        /// <returns>The value of the option.</returns>
        public object this[CrypterOptionKey key]
        {
            get
            {
                return _options[key];
            }
            set
            {
                Check.Null("key", key);
                key.CheckValue(value);
                AboutToChange();
                _options[key] = value;
            }
        }

        /// <summary>
        /// No options.
        /// </summary>
        // Token: 0x17000032 RID: 50
        public static CrypterOptions None { get; private set; } = new CrypterOptions().MakeReadOnly();

        // Token: 0x0600012D RID: 301 RVA: 0x000095C8 File Offset: 0x000077C8
        private void AboutToChange()
        {
            if (IsReadOnly)
            {
                throw Exceptions.InvalidOperation();
            }
        }

        /// <summary>
        /// Prevents future changes to the options.
        /// </summary>
        /// <returns>The same <see cref="T:CryptSharp.CrypterOptions" />.</returns>
        public CrypterOptions MakeReadOnly()
        {
            IsReadOnly = true;
            return this;
        }

        /// <summary>
        /// <c>true</c> if the options cannot be changed.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        // Token: 0x0400006F RID: 111
        private Dictionary<CrypterOptionKey, object> _options = new Dictionary<CrypterOptionKey, object>();
    }
}
