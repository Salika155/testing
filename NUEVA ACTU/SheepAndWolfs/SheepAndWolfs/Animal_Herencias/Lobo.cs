using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    //TODO: esto funciona
    public class Lobo : Animal
    {
        //private Coordenada? _coordenada;
        //food, water, stamina, type y sleep
        
        //public Coordenada coordenada;
        //public int food;
        //public int water;
        //public int stamina;
        //public int sleep;
        
        //TODO: esto funciona
        public Lobo(int food, int water, int stamina, int sleep, AnimalType type, string name, int velocidad) : base(food, water, stamina, sleep, AnimalType.LOBO, name, velocidad)
        {
        }

        //public void MoverLobo()
        //{
        //    _coordenada = new Coordenada();

        //}

        public override int GetVelocidad()
        {
            return base.GetVelocidad() + 5;  // Los lobos tienen 5 unidades más de velocidad que el valor base
        }

        public override void Mover()
        {
            Console.WriteLine($"{GetNombre()} el lobo se mueve a una velocidad de {GetVelocidad()}.");
        }
        //esto va en un enum -> o crearse una clase ai
        //-> me creo un array de count del enum para cada caso, y lo relleno de 0
        //-> hidratacion campo = 902 -> votos beber = 1000 - h
        //despues de decisiones le pasamos un random de -100 a 100
        //al final el mayor valor sera el prioritario
        //moverse arriba, abajo, derecha, izquierda
        //comer, beber
        //dormir
        //algoritmo a* pathfinder no es obligatorio

        //public void AtacarOveja()
        //{
        //}

        //class Animal 
        //Lobo : Animal
        //Oveja : Animal
    }
}
