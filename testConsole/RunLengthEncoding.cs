using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encode
{
    public class RunLengthEncoding
    {
        public static string Encode(string src)
        {
            string dest = "";
            int srcpoint = 0;
            int count = 1;
            char c = src[0]; srcpoint++;
            dest += c;
            while (srcpoint < src.Length)
            {
                if (c == src[srcpoint])
                {
                    count++;
                }
                else
                {
                    if (count >= 2)
                    {
                        //stringstream s;
                        string tmp = count.ToString();

                        //s << count;
                        //s >> tmp;
                        dest += tmp;
                    }
                    count = 1;
                    if ((srcpoint != src.Length))
                    {
                        c = src[srcpoint];
                        dest += c;
                    }
                }
                srcpoint++;
            }
            if (count != 1) dest += count.ToString();
            return dest;
        }

        public static string Decode(string src)
        {
            string dest = "";
            int srcpoint = 1;
            int numcount = 0;
            char c = src[0];
            dest = c.ToString();
            string ss = "";
            while (srcpoint < src.Length)
            {
                if (('0' <= src[srcpoint]) && (src[srcpoint] <= '9'))
                {
                    ss += src[srcpoint];
                }
                else
                {
                    int x = 0;
                    x = int.Parse(ss == "" ? "1" : ss);
                    for (int i = 1; i < x; i++)
                    {
                        dest += c;
                    }
                    if (srcpoint != src.Length) dest += (c = src[srcpoint]);
                    //ss.str(""); ss.clear();
                    ss = "";
                }
                srcpoint++;
            }
            int _x = 0;
            _x = int.Parse(ss == "" ? "1" : ss);
            for (int i = 1; i < _x; i++)
            {
                dest += c;
            }
            if (srcpoint != src.Length) dest += (c = src[srcpoint]);
            //ss.str(""); ss.clear();
            ss = "";
            return dest;
        }
    }
}
