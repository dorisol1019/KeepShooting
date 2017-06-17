using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = @"ある日の暮方の事である。一人の下人（げにん）が、羅生門（らしょうもん）の下で雨やみを待っていた。
広い門の下には、この男のほかに誰もいない。ただ、所々丹塗（にぬり）の剥（は）げた、大きな円柱（まるばしら）に、蟋蟀（きりぎりす）が一匹とまっている。羅生門が、朱雀大路（すざくおおじ）にある以上は、この男のほかにも、雨やみをする市女笠（いちめがさ）や揉烏帽子（もみえぼし）が、もう二三人はありそうなものである。それが、この男のほかには誰もいない。";
            s += s;
            s += s;
            s += s;
            s += s;
            s += s;
            s += s;
            s += s;
            s += s;
            s += s;
            //            Console.WriteLine($"{s},{s.Length}");
            var ss = (Encode.Deflate.Compress(s));
            string sss = Convert.ToBase64String(ss);
            var ssss = Convert.FromBase64String(sss);
            Console.WriteLine($"{sss},{sss.Length}");
            bool t = true;
            for (int i = 0; i < ss.Length; i++)
            {
                if(ss[i]!=ssss[i])
                {
                    t=false;break;
                }
            }
            Console.WriteLine(t);
            string new_s = Encode.Deflate.Decompress((ss));
            //          Console.WriteLine(new_s);
                      Console.WriteLine(new_s.Length);

            Console.WriteLine(new_s == s);

        }
    }
}
