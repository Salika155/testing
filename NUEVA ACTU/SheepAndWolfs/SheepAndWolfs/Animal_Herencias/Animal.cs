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
    public abstract class Animal
    {
        //TODO: esto funciona

        //public AnimalType? type;
        //public Coordenada? coordenada;
        ////food, water, stamina, type y sleep
        //public int saciedad;
        //public int hidratacion;
        //public int resistencia;
        //public int sueño;
        //public string nombre;
        //public double velocidad;

        private AnimalType? _type;
        private Coordenada? _coordenada;
        private int _saciedad;
        private int _hidratacion;
        private int _resistencia;
        private int _sueño;
        private string? _nombre;
        private int _velocidad;

        //TODO: esto funciona
        public Animal(int food, int water, int stamina, int sleep, AnimalType type, string? name, int speed)
        {
            this._saciedad = food;
            this._hidratacion = water;
            this._resistencia = stamina;
            this._sueño = sleep;
            this._type = type;
            this._nombre = name;
            this._velocidad = speed;
        }

        public virtual int GetSaciedad() => _saciedad;
        public virtual void SetSaciedad(int value) => _saciedad = value; 
        public virtual int GetHidratacion() => _hidratacion;
        public virtual void SetHidratacion(int value) => _hidratacion = value;
        public virtual int GetResistencia() => _resistencia;
        public virtual void SetResistencia(int value) => _resistencia = value;
        public virtual int GetSueño() => _sueño;
        public virtual void SetSueño(int value) => _sueño = value;
        public virtual string? GetNombre() => _nombre;
        public virtual int GetVelocidad() => _velocidad;
        public virtual AnimalType? GetType() => _type;
        public virtual Coordenada? GetCoordenada() => _coordenada;
        public void SetCoordenada(int x, int y) => _coordenada = new Coordenada(x, y);

        //crearanimales en el constructor

        //animals va a tener los metodos de moverse, pero dudo si deberia tener el metodo de comer animales, ya que eso
        //solo lo haran los lobos, al igual que la hierba las ovejas
        public virtual void Mover()
        {
            Console.WriteLine($"{_nombre} se mueve a una velocidad de {GetVelocidad()}.");
        }
    }
}
