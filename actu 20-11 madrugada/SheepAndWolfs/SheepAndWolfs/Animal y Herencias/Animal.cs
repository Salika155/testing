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

        public AnimalType type;
        public Coordenada? coordenada;
        //food, water, stamina, type y sleep
        public int food;
        public int water;
        public int stamina;
        public int sleep;



        //TODO: esto funciona
        public Animal(int food, int water, int stamina, int sleep, AnimalType type)
        {
            this.food = food;
            this.water = water;
            this.stamina = stamina;
            this.sleep = sleep;
            this.type = type;
        }

        //crearanimales en el constructor

        //animals va a tener los metodos de moverse, pero dudo si deberia tener el metodo de comer animales, ya que eso
        //solo lo haran los lobos, al igual que la hierba las ovejas
    }
}
