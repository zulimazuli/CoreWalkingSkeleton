﻿using System;
using System.Globalization;
using System.Text;

namespace CoreTemplate.ApplicationCore.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNonAlphabeticCharacters(this string str)
        {
            var arr = str.ToCharArray();
            arr = Array.FindAll(arr, char.IsLetter);
            return new string(arr);
        }

        public static string RemoveDiacritics(this string str)
        {
            var normalizedString = str.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}