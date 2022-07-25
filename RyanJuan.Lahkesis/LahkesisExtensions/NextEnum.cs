using System;
using System.Collections.Concurrent;
using System.Linq;

using JetBrains.Annotations;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
        /// <summary>
        /// 傳回隨機的列舉值。
        /// </summary>
        /// <typeparam name="TEnum">
        /// 指定的列舉型別。
        /// </typeparam>
        /// <param name="random"></param>
        /// <returns>
        /// 列舉 <typeparamref name="TEnum"/> 中的其中一個值。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> 的值為 null。
        /// </exception>
#else
        /// <summary>
        /// Return a random enum value.
        /// </summary>
        /// <typeparam name="TEnum">
        /// The specified enum type.
        /// </typeparam>
        /// <param name="random"></param>
        /// <returns>
        /// One of the enum value of <typeparamref name="TEnum"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
#endif
        [PublicAPI]
        public static TEnum NextEnum<TEnum>(this Random random)
            where TEnum : Enum
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return (TEnum)NextEnumInternal(random, typeof(TEnum));
        }

#if ZH_HANT
        /// <summary>
        /// 傳回隨機的列舉值。
        /// </summary>
        /// <param name="random"></param>
        /// <param name="enumType">
        /// 指定的列舉型別。
        /// </param>
        /// <returns>
        /// 列舉型別 <paramref name="enumType"/> 中的其中一個值。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> 或 <paramref name="enumType"/> 的值為 null。
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="enumType"/> 不是列舉型別。
        /// </exception>
#else
        /// <summary>
        /// Return a random enum value.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="enumType">
        /// The specified enum type.
        /// </param>
        /// <returns>
        /// One of the enum value of type <paramref name="enumType"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> or <paramref name="enumType"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="enumType"/> is not an enum type.
        /// </exception>
#endif
        [PublicAPI]
        public static object NextEnum(
            this Random random,
            Type enumType)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            Error.ThrowIfArgumentNull(enumType, nameof(enumType));
            Error.ThrowIfTypeIsNotEnum(nameof(enumType), enumType);
            return NextEnumInternal(random, enumType);
        }

        internal static object NextEnumInternal(
            Random random,
            Type enumType)
        {
            var typeCode = Type.GetTypeCode(enumType.GetEnumUnderlyingType());
            var values = EnumValueHelper.GetValues(enumType);
            if (typeCode == TypeCode.UInt32 ||
                typeCode == TypeCode.Int64 ||
                typeCode == TypeCode.UInt64)
            {
                return values.GetValue(random.NextInt64(values.LongLength));
            }
            else
            {
                return values.GetValue(random.Next(values.Length));
            }
        }

        internal static class EnumValueHelper
        {
            private static readonly ConcurrentDictionary<Type, Array> s_enumValueBuffer =
                new ConcurrentDictionary<Type, Array>();

            public static Array GetValues(Type enumType)
            {
                if (s_enumValueBuffer.TryGetValue(enumType, out var value))
                {
                    return value;
                }
                value = Enum.GetValues(enumType);
                s_enumValueBuffer.TryAdd(enumType, value);
                return value;
            }
        }
    }
}
