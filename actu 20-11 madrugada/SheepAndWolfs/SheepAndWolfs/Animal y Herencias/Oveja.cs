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

        //private Coordenada? _coordenada;
        //food, water, stamina, type y sleep
        public AnimalType type = AnimalType.OVEJA;
        //public Coordenada coordenada;
        //public int food;
        //public int water;
        //public int stamina;
        //public int sleep;


        //TODO: esto funciona
        public Oveja(int food, int water, int stamina, int sleep, AnimalType type) : base(food, water, stamina, sleep, type)
        {

        }




        //si los metodos vienen de la clase padre animal, habra que sobreescribirlos aqui

    }
}
