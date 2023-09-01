using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace TMPro
{
    public static class TMPro_ExtensionMethods
    {

        public static int[] ToIntArray(this string text)
        {
            int[] intArray = new int[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                intArray[i] = text[i];
            }

            return intArray;
        }

        public static string ArrayToString(this char[] chars)
        {
            string s = string.Empty;

            for (int i = 0; i < chars.Length && chars[i] != 0; i++)
            {
                s += chars[i];
            }

            return s;
        }

        public static string IntToString(this int[] unicodes)
        {
            char[] chars = new char[unicodes.Length];

            for (int i = 0; i < unicodes.Length; i++)
 