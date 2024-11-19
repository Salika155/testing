using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    public enum AnimalType
    {
        ANIMAL,
        OVEJA,
        LOBO
    }
    public class Animal
    {
        //TODO: esto funciona
        public readonly string? Nombre = "";
        public int Vida;
        public AnimalType type = AnimalType.ANIMAL;
        public Coordenada? coordenada;


        //TODO: esto funciona
        public Animal(string name, int n)
        {
            Nombre = name;
            Vida = n;
        }

        //crearanimales en el constructor

        //animals va a tener los metodos de moverse, pero dudo si deberia tener el metodo de comer animales, ya que eso
        //solo lo haran los lobos, al igual que la hierba las ovejas
    }
}
