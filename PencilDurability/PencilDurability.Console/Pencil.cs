using System;
using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public void WriteOn(string text, ISurface surface)
        {
            if (surface == null) throw new NothingToWriteOnException();
            if (text == null) return;
            foreach (var c in text)
            {
                surface.Write(c);
            }
        }
    }

    public class NothingToWriteOnException: Exception
    {
    }
}