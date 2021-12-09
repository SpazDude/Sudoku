using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /*
        This class solves a soduku puzzle in two steps:
        1) update the puzzle by populating trivial cells (only one unused value)
        2) solve the puzzle's trivial cells by exhaustive search using a stack
    */
    public class Sudoku
    {
        /*
            This method populates the trivial cells then passes off the solution to the exhaustive search
        */
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

        /*
            This recursive method exhaustively searches for a solution of to the puzzle
            by trying each possible value for a cell until it
            1) finds a value for the last cell and returns the result (success)
            2) has no possible values for a cell and returns null (failure)
        */
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

        /*
            This method returns an enumerable list of values not found
            a) in the same row as the cell
            b) in the same column as the cell
            c) in the same block (3x3) as the cell
            This is accomplished with a bit-mask representing the characters from '0' to '9'
        */
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
