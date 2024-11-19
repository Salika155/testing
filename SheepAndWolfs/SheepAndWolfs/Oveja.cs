using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    //TODO: esto funciona
    public class Oveja : Animal
    {
        private Coordenada? _coordenada;
        private int _vida;
        private string _name = "";
        private string v;

        public Oveja(string v) : base(v, 500)
        {
            this.v = v;
            _vida = 500;
            _name = "oveja1";
        }

        public Oveja(string name, int v) : base(name, 500)
        {
            _vida = 500;
            _name = "oveja1";
        }

        //si los metodos vienen de la clase padre animal, habra que sobreescribirlos aqui

    }
}
