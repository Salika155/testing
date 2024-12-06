using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    public class Coordenada
    {
        public readonly int X;
        public readonly int Y;

        public Coordenada()
        {
        }
        public Coordenada(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        //TODO: esto funciona
        public bool EqualsToCoordenada(int x, int y)
        {
            return X == x && Y == y;
        }
    }
}
