using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    internal class AI
    {
        public enum IAType
        {
            MoverArriba,
            MoverDerecha,
            MoverAbajo,
            MoverIzquierda,
            Comer,
            Beber,
            Dormir

            //posible que si hago mover animal acorte y solo tenga que pasarle el tipo para saber si es un lobo o una oveja
        }

        private IAType[] _iATypes = new IAType[] {IAType.MoverArriba, IAType.MoverDerecha, IAType.MoverAbajo, IAType.MoverIzquierda,
        IAType.Comer, IAType.Beber, IAType.Dormir};
    }
}
