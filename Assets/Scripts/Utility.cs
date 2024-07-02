using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    public static class Utility
    {
        public static float Dampning( float x, float y ,float decay, float time)
        {
            return y + (x - y) * MathF.Exp(-decay * time);
        }
    }
}
