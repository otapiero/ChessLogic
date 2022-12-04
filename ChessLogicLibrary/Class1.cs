using System.Collections;
using System.Text;

namespace ChessLogicLibrary
{
    public class Solution
    {
        public int MyAtoi(string s)
        {
            int res = 0;
            bool minus = false;
            int length = s.Length;
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case char x when x >= '0' && x <= '9':
                        res += (x) * (int)Math.Pow(10, (length - i));
                        break;
                    case '-':
                        minus = true;
                        break;
                    default:
                        length++;
                        break;
                }
            }
            if (minus)
            {
                res = -res;
            }
            return res;
        }
    }
}