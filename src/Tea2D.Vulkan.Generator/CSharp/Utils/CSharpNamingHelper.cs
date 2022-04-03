﻿using System;
using System.Linq;

namespace Tea2D.Vulkan.Generator.CSharp.Utils
{
    public static class CSharpNamingHelper
    {
        public const string UnsafeKeyword = "unsafe";
        public const string FixedKeyword = "fixed";
        public const string ObjectKeyword = "object";

        public static string NormalizeFiledName(string name)
        {
            return name.Equals(ObjectKeyword, StringComparison.OrdinalIgnoreCase)
                ? '@' + name
                : name;
        }

        public static string NormalizeEnumItemName(string name)
        {
            var parts = name.ToLower()
                .Split('_')
                .Select(p => char.ToUpper(p[0]) + p.Substring(1));

            return string.Concat(parts);
        }
    }
}