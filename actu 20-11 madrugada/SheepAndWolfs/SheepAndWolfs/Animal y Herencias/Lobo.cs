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
        public AnimalType type = AnimalType.LOBO;
        //public Coordenada coordenada;
        //public int food;
        //public int water;
        //public int stamina;
        //public int sleep;

        
        //TODO: esto funciona
        public Lobo(int food, int water, int stamina, int sleep, AnimalType type) : base(food, water, stamina, sleep, type)
        {
            
        }
       
        //public void MoverLobo()
        //{
        //    _coordenada = new Coordenada();

        //}
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
