﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class Sudoku
    {
        public static string Solve(string s)
        {
            var builder = new StringBuilder(s);
            for (var i = 0; i < 81; i++)
            {
                if (builder[i] != '0') continue;
                var v = UnusedValues(s, i).ToArray();
                builder[i] = v.Count() == 1 ? v.Single() : '0';
            }
            return Solve(builder.ToString(), 0);
        }

        private static string Solve(string s, int p)
        {
            if (p == 81) return s;
            if (s[p] != '0') return Solve(s, p + 1);
            foreach (var value in UnusedValues(s, p))
            {
                var builder = new StringBuilder(s);
                builder[p] = value;
                var result = Solve(builder.ToString(), p + 1);
                if (result != null) return result;
            }
            return null;
        }

        private static IEnumerable<char> UnusedValues(string a, int p)
        {
            var result = new bool[] { false, true, true, true, true, true, true, true, true, true };
            for (var i = 0; i < 9; i++)
            {
                result[(a[p / 9 * 9 + i]) - '0'] = false;
                result[(a[p % 9 + i * 9]) - '0'] = false;
                result[(a[p / 27 * 27 + i / 3 * 9 + p % 9 / 3 * 3 + i % 3]) - '0'] = false;
            }
            for (char c = '1'; c <= '9'; c++) if (result[c - '0']) yield return c;
        }
    }
}
