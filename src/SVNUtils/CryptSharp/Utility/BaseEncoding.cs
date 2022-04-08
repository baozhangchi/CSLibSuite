using SVNUtils.CryptSharp.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVNUtils.CryptSharp.Utility
{
    /// <summary>
    /// Performs generic binary-to-text encoding.
    /// </summary>
    // Token: 0x0200000E RID: 14
    public class BaseEncoding : Encoding
    {
        /// <inheritdoc />
        // Token: 0x06000063 RID: 99 RVA: 0x00003840 File Offset: 0x00001A40
        public override int GetMaxCharCount(int byteCount)
        {
            Check.Range("byteCount", byteCount, 0, int.MaxValue);
            return checked(byteCount * 8 + BitsPerCharacter - 1) / BitsPerCharacter;
        }

        /// <inheritdoc />
        // Token: 0x06000064 RID: 100 RVA: 0x00003878 File Offset: 0x00001A78
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            int charCount = GetCharCount(bytes, byteIndex, byteCount);
            return GetChars(bytes, byteIndex, byteCount, chars, charIndex, charCount);
        }

        /// <summary>
        /// Converts bytes from their binary representation to a text representation.
        /// </summary>
        /// <param name="bytes">An input array of bytes.</param>
        /// <param name="byteIndex">The index of the first byte.</param>
        /// <param name="byteCount">The number of bytes to read.</param>
        /// <param name="chars">An output array of characters.</param>
        /// <param name="charIndex">The index of the first character.</param>
        /// <param name="charCount">The number of characters to write.</param>
        /// <returns>The number of characters written.</returns>
        // Token: 0x06000065 RID: 101 RVA: 0x000038A4 File Offset: 0x00001AA4
        public int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount)
        {
            Check.Bounds("bytes", bytes, byteIndex, byteCount);
            Check.Bounds("chars", chars, charIndex, charCount);
            int byteEnd = checked(byteIndex + byteCount);
            int bitStartOffset = 0;
            for (int i = 0; i < charCount; i++)
            {
                byte thisByte = byteIndex < byteEnd ? bytes[byteIndex] : default;
                byte nextByte = byteIndex + 1 < byteEnd ? bytes[byteIndex + 1] : default;
                int bitEndOffset = bitStartOffset + BitsPerCharacter;
                byte value;
                if (MsbComesFirst)
                {
                    value = BitMath.ShiftRight(thisByte, 8 - bitStartOffset - BitsPerCharacter);
                    if (bitEndOffset > 8)
                    {
                        value |= BitMath.ShiftRight(nextByte, 16 - bitStartOffset - BitsPerCharacter);
                    }
                }
                else
                {
                    value = BitMath.ShiftRight(thisByte, bitStartOffset);
                    if (bitEndOffset > 8)
                    {
                        value |= BitMath.ShiftRight(nextByte, bitStartOffset - 8);
                    }
                }
                bitStartOffset = bitEndOffset;
                if (bitStartOffset >= 8)
                {
                    bitStartOffset -= 8;
                    byteIndex++;
                }
                chars[i] = GetChar(value);
            }
            return charCount;
        }

        /// <inheritdoc />
        // Token: 0x06000066 RID: 102 RVA: 0x000039B8 File Offset: 0x00001BB8
        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            Check.Bounds("bytes", bytes, index, count);
            return GetMaxCharCount(count);
        }

        /// <inheritdoc />
        // Token: 0x06000067 RID: 103 RVA: 0x000039E0 File Offset: 0x00001BE0
        public override int GetMaxByteCount(int charCount)
        {
            Check.Range("charCount", charCount, 0, int.MaxValue);
            return checked(charCount * BitsPerCharacter) / 8;
        }

        /// <inheritdoc />
        // Token: 0x06000068 RID: 104 RVA: 0x00003A10 File Offset: 0x00001C10
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            int byteCount = GetByteCount(chars, charIndex, charCount);
            return GetBytes(chars, charIndex, charCount, bytes, byteIndex, byteCount);
        }

        /// <summary>
        /// Converts characters from their text representation to a binary representation.
        /// </summary>
        /// <param name="chars">An input array of characters.</param>
        /// <param name="charIndex">The index of the first character.</param>
        /// <param name="charCount">The number of characters to read.</param>
        /// <param name="bytes">An output array of bytes.</param>
        /// <param name="byteIndex">The index of the first byte.</param>
        /// <param name="byteCount">The number of bytes to write.</param>
        /// <returns>The number of bytes written.</returns>
        // Token: 0x06000069 RID: 105 RVA: 0x00003A3C File Offset: 0x00001C3C
        public int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount)
        {
            Check.Bounds("chars", chars, charIndex, charCount);
            Check.Bounds("bytes", bytes, byteIndex, byteCount);
            Array.Clear(bytes, byteIndex, byteCount);
            int byteEnd = checked(byteIndex + byteCount);
            int bitStartOffset = 0;
            for (int i = 0; i < charCount; i++)
            {
                byte value = (byte)GetValue(chars[i]);
                int bitEndOffset = bitStartOffset + BitsPerCharacter;
                if (MsbComesFirst)
                {
                    if (byteIndex < byteEnd)
                    {
                        int num = byteIndex;
                        bytes[num] |= BitMath.ShiftLeft(value, 8 - bitStartOffset - BitsPerCharacter);
                    }
                    if (byteIndex + 1 < byteEnd && bitEndOffset > 8)
                    {
                        int num2 = byteIndex + 1;
                        bytes[num2] |= BitMath.ShiftLeft(value, 16 - bitStartOffset - BitsPerCharacter);
                    }
                }
                else
                {
                    if (byteIndex < byteEnd)
                    {
                        int num3 = byteIndex;
                        bytes[num3] |= BitMath.ShiftLeft(value, bitStartOffset);
                    }
                    if (byteIndex + 1 < byteEnd && bitEndOffset > 8)
                    {
                        int num4 = byteIndex + 1;
                        bytes[num4] |= BitMath.ShiftLeft(value, bitStartOffset - 8);
                    }
                }
                bitStartOffset = bitEndOffset;
                if (bitStartOffset >= 8)
                {
                    bitStartOffset -= 8;
                    byteIndex++;
                }
            }
            return byteCount;
        }

        /// <inheritdoc />
        // Token: 0x0600006A RID: 106 RVA: 0x00003BC4 File Offset: 0x00001DC4
        public override int GetByteCount(char[] chars, int index, int count)
        {
            Check.Bounds("chars", chars, index, count);
            return GetMaxByteCount(count);
        }

        /// <summary>
        /// Defines a binary-to-text encoding.
        /// </summary>
        /// <param name="characterSet">The characters of the encoding.</param>
        /// <param name="msbComesFirst">
        ///     <c>true</c> to begin with the most-significant bit of each byte.
        ///     Otherwise, the encoding begins with the least-significant bit.
        /// </param>
        // Token: 0x0600006B RID: 107 RVA: 0x00003BEB File Offset: 0x00001DEB
        public BaseEncoding(string characterSet, bool msbComesFirst) : this(characterSet, msbComesFirst, null, null)
        {
        }

        /// <summary>
        /// Defines a binary-to-text encoding.
        /// Additional decode characters let you add aliases, and a filter callback can be used
        /// to make decoding case-insensitive among other things.
        /// </summary>
        /// <param name="characterSet">The characters of the encoding.</param>
        /// <param name="msbComesFirst">
        ///     <c>true</c> to begin with the most-significant bit of each byte.
        ///     Otherwise, the encoding begins with the least-significant bit.
        /// </param>
        /// <param name="additionalDecodeCharacters">
        ///     A dictionary of alias characters, or <c>null</c> if no aliases are desired.
        /// </param>
        /// <param name="decodeFilterCallback">
        ///     A callback to map arbitrary characters onto the characters that can be decoded.
        /// </param>
        // Token: 0x0600006C RID: 108 RVA: 0x00003BFC File Offset: 0x00001DFC
        public BaseEncoding(string characterSet, bool msbComesFirst, IDictionary<char, int> additionalDecodeCharacters, BaseEncodingDecodeFilterCallback decodeFilterCallback)
        {
            Check.Null("characterSet", characterSet);
            if (!BitMath.IsPositivePowerOf2(characterSet.Length))
            {
                throw Exceptions.Argument("characterSet", "Length must be a power of 2.", new object[0]);
            }
            if (characterSet.Length > 256)
            {
                throw Exceptions.Argument("characterSet", "Character sets with over 256 characters are not supported.", new object[0]);
            }
            _bitCount = 31 - BitMath.CountLeadingZeros(characterSet.Length);
            _bitMask = (1 << _bitCount) - 1;
            _characters = characterSet;
            _msbComesFirst = msbComesFirst;
            _decodeFilterCallback = decodeFilterCallback;
            _values = additionalDecodeCharacters != null ? new Dictionary<char, int>(additionalDecodeCharacters) : new Dictionary<char, int>();
            for (int i = 0; i < characterSet.Length; i++)
            {
                char ch = characterSet[i];
                if (_values.ContainsKey(ch))
                {
                    throw Exceptions.Argument("Duplicate characters are not supported.", "characterSet", new object[0]);
                }
                _values.Add(ch, (byte)i);
            }
        }

        /// <summary>
        /// Gets the value corresponding to the specified character.
        /// </summary>
        /// <param name="character">A character.</param>
        /// <returns>A value, or <c>-1</c> if the character is not part of the encoding.</returns>
        // Token: 0x0600006D RID: 109 RVA: 0x00003D1C File Offset: 0x00001F1C
        public virtual int GetValue(char character)
        {
            if (_decodeFilterCallback != null)
            {
                character = _decodeFilterCallback(character);
            }
            int value;
            return _values.TryGetValue(character, out value) ? value : -1;
        }

        /// <summary>
        /// Gets the character corresponding to the specified value.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns>A character.</returns>
        // Token: 0x0600006E RID: 110 RVA: 0x00003D60 File Offset: 0x00001F60
        public virtual char GetChar(int value)
        {
            return _characters[value & BitMask];
        }

        /// <summary>
        /// The bit mask for a single character in the current encoding.
        /// </summary>
        // Token: 0x17000014 RID: 20
        // (get) Token: 0x0600006F RID: 111 RVA: 0x00003D88 File Offset: 0x00001F88
        public int BitMask
        {
            get
            {
                return _bitMask;
            }
        }

        /// <summary>
        /// The number of bits per character in the current encoding.
        /// </summary>
        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000070 RID: 112 RVA: 0x00003DA0 File Offset: 0x00001FA0
        public int BitsPerCharacter
        {
            get
            {
                return _bitCount;
            }
        }

        /// <summary>
        /// <c>true</c> if the encoding begins with the most-significant bit of each byte.
        /// Otherwise, the encoding begins with the least-significant bit.
        /// </summary>
        // Token: 0x17000016 RID: 22
        // (get) Token: 0x06000071 RID: 113 RVA: 0x00003DB8 File Offset: 0x00001FB8
        public bool MsbComesFirst
        {
            get
            {
                return _msbComesFirst;
            }
        }

        // Token: 0x0400002D RID: 45
        private int _bitCount;

        // Token: 0x0400002E RID: 46
        private int _bitMask;

        // Token: 0x0400002F RID: 47
        private string _characters;

        // Token: 0x04000030 RID: 48
        private bool _msbComesFirst;

        // Token: 0x04000031 RID: 49
        private Dictionary<char, int> _values;

        // Token: 0x04000032 RID: 50
        private BaseEncodingDecodeFilterCallback _decodeFilterCallback;
    }
}
