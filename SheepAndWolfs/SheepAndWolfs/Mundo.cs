using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    public class Mundo
    {
        #region intentos de elementos y planteamientos (no funcionan)
        //mejor en array
        //private List<Casilla> _casilla;
        //private (int, int)[] _casillas = new (int, int)[] { };
        //private (Coordenada, TerritorioType)[] _casillas = new (Coordenada, TerritorioType)[] { };
        #endregion

        //TODO: esto funciona
        private Casilla[] _casillas = [];
        //lista de lobos y ovejas obligatorio -> como hago animales no es necesario
        private int _width;
        private int _height;
        private List<Animal> _animals;


        //TODO: esto funciona
        public Mundo(int width, int height)
        {
            this._width = width;
            this._height = height;
            //this._casilla = new List<Casilla>();
            this._casillas = new Casilla[width * height];
            this._animals = new List<Animal>();

            CrearCasillas();
        }

        #region intento de crearcasilla desde utils copiando la funcion que hay en mundo
        //esto es por el crearcasilla que existe en mundo, la idea es pasarlo a utils
        //public void CrearCasillas()
        //{
        //    for (int y = 0; y < _height; y++)
        //    {
        //        for (int x = 0; x < _width; x++)
        //        {
        //            int index = Utils.IndexOfCasilla(x, y, _width);
        //            _casillas[index] = new Casilla(new Coordenada(x, y));
        //        }
        //    }
        //}
        #endregion

        //TODO: esto funciona
        public int GetWidth() => _width;

        //TODO: esto funciona
        public int GetHeight() => _height;

        //TODO: esto funciona
        public Casilla? GetCasillaAt(int x, int y)
        {
            #region codigo viejo comentado de getcasillaat
            //if ((x < 0 || x >=  _width) || (y < 0 || y >= _height))
            //    return null;
            //for(int i = 0; i < _casilla.Count; i++)
            //{
            //    Casilla? casilla = _casilla[i];
            //    if (casilla.coordenada.EqualsToCoordenada(x, y))
            //        return casilla;
            //return _casilla[IndexOfCasilla(x, y)]; -> esta era la corta buena hasta que movi la funcion indexofcasilla a utils
            //}
            //return null;
            #endregion

            if (!Utils.IsValidCoordinates(x, y, _width, _height))
                return null;

            int index = Utils.IndexOfCasilla(x, y, _width);
            return index >= 0 && index < _casillas.Length ? _casillas[index] : null;
        }

        //TODO: esto funciona
        public Animal? GetAnimalAt(int x, int y, AnimalType type)
        {
            if (!Utils.IsValidCoordinates(x, y, _width, _height))
                return null;
            for (int i = 0; i < _animals.Count; i++)
            {
                Animal? animal = _animals[i];
                if (animal.coordenada!.EqualsToCoordenada(x, y) && animal.type == type)
                    return animal;
            }
            return null;
        }

        //TODO: esto funciona
        public void AddAnimal(Animal animal, int x, int y)
        {
            if (animal == null || Utils.IsValidCoordinates(x, y, _width, _height) is false)
                return;

            animal.coordenada = new Coordenada(x, y); // Asignar coordenadas al animal
            _animals.Add(animal);
        }

        //hace falta un removeAnimal para cuando un lobo se coma una oveja o un animal se muera por atributos
        public void RemoveAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        //Aqui creo que se puede hacer un createAnimals que cree los animales y los añada a la lista de animales
        //TODO: ESTO tengo que volver a hacerlo
        public void CreateAnimals(int count, AnimalType type)
        {
            for (int i = 0; i < count; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y, AnimalType.LOBO) == null)
                {
                    Lobo lobo = new Lobo($"Lobo{i}");
                    lobo.coordenada = new Coordenada(x, y);
                    AddAnimal(lobo, x, y);
                }
                else if (GetAnimalAt(x, y, AnimalType.OVEJA) == null)
                {
                    Oveja oveja = new Oveja($"Oveja{i}");
                    oveja.coordenada = new Coordenada(x, y);
                    AddAnimal(oveja, x, y);
                }
            }
        }

        //TODO: esto funciona
        public void CreateWolfs(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y, AnimalType.LOBO) == null) // Evitar duplicados
                {
                    Lobo lobo = new Lobo($"Lobo{i}");
                    lobo.coordenada = new Coordenada(x, y);
                    AddAnimal(lobo, x, y);
                }
            }
        }

        //TODO: esto funciona
        public void CreateSheeps(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y, AnimalType.OVEJA) == null) // Evitar duplicados
                {
                    Oveja oveja = new Oveja($"Oveja{i}");
                    oveja.coordenada = new Coordenada(x, y);
                    AddAnimal(oveja, x, y);
                }
            }
        }

        //TODO: esto funciona
        public void CrearCasillas()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int index = Utils.IndexOfCasilla(x, y, _width);
                    _casillas[index] = new Casilla(new Coordenada(x, y))
                    {
                        type = Utils.GenerateRandomType()
                    };
                }
            }
        }

        //----------------------------------------------------------------------------------

        //TODO: esto no lo he usado
        public AnimalType GetAnimalTypeAt(int x, int y, AnimalType type)
        {
            Animal? animal = GetAnimalAt(x, y, type);
            return animal?.type ?? AnimalType.ANIMAL;
        }

        //TODO: esto no lo he usado
        public int IndexOfAnimal(Animal animal)
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                if (_animals[i].type == animal.type && _animals[i].Nombre == animal.Nombre)
                    return i;
            }
            return -1;
        }

        //TODO: esto no lo he usado
        public int CountAnimals() => _animals.Count;
        

        //TODO: esto no lo he usado
        public int CountAnimalsType(AnimalType type)
        {
            if (_animals.Count == 0)
                return 0;

            int aux = 0;
            for (int i = 0; i < _animals.Count; i++)
                if (_animals[i].type == type)
                    aux += 1;
            return aux;
        }

        //TODO: esto no lo he usado
        public bool ContainsCasilla(int x, int y)
        {
            //if ((x < 0 || x >= _width) || (y < 0 || y >= _height))
            //    return false;
            //if (x < 0)
            //y, width, height
            return Utils.IndexOfCasilla(x, y, _width) != -1;
        }

        //TODO: esto no lo he usado
        public int CountCasilla() => _casillas.Length;
        
        //TODO: esto no lo he usado
        public void ImprimirCasillas()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int index = Utils.IndexOfCasilla(x, y, _width);
                    if (_casillas[index] != null)
                        Console.Write($"[{_casillas[index].coordenada.X},{_casillas[index].coordenada.Y}] ");
                    else
                        Console.Write("[null] ");
                }
                Console.WriteLine(); // Nueva línea para cada fila
            }
        }

        //TODO: esto no lo he usado
        internal void CreateRocks(int v)
        {
            throw new NotImplementedException();
        }

        //TODO: esto no lo he usado
        internal void CreateWaters(int v)
        {
            throw new NotImplementedException();
        }

        //public int IndexOfCasilla(int x, int y)
        //{
        //    if (x < 0 || y < 0 || _width <= 0)
        //        return -1;
        //    //for (int i = 0; i < _casilla.Count; ++i)
        //    //{
        //    //    Casilla casilla = _casilla[i];
        //    //    if (casilla.coordenada.EqualsToCoordenada(x, y))
        //    //        return i;
        //    //}
        //    //return -1;
        //    return y * _width + x;
        //    //dado un index para hallar x e y divido el index entre el ancho como divisor y el cociente sera la y, y el resto la x
        //}


        //public AnimalType GetAnimalTypeAt(int x, int y)
        //{
        //    Animal animal = GetAnimalAt(x, y);
        //    return animal.animalType;
        //}

        //public int IndexOfOveja(Oveja oveja)
        //{
        //    for (int i = 0; i < _ovejas.Count; i++)
        //    {
        //        if (_ovejas[i].Nombre == oveja.Nombre)
        //            return i;
        //    }
        //    return -1;
        //}

        //public int IndexOfLobo()
        //{
        //    for (int i = 0; i < _ovejas.Count; i++)
        //    {
        //        if (_ovejas[i].Nombre == oveja.Nombre)
        //            return i;
        //    }
        //    return -1;
        //}

        //int index = y * width + x
        //index / ancho = cociente la y y resto la x

    }
}
