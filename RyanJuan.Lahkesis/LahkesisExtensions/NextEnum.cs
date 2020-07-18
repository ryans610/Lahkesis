using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
#else
#endif
        /// <summary>
        /// 
        /// </summary>
        public static TEnum NextEnum<TEnum>(
            this Random random)
            where TEnum : Enum
        {
            return (TEnum)random.NextEnum(typeof(TEnum));
        }

#if ZH_HANT
#else
#endif
        /// <summary>
        /// 
        /// </summary>
        public static object NextEnum(
            this Random random,
            Type enumType)
        {
            Error.ThrowIfArgumentNull(nameof(random), random);
            Error.ThrowIfArgumentNull(nameof(enumType), enumType);
            Error.ThrowIfTypeIsNotEnum(nameof(enumType), enumType);
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
