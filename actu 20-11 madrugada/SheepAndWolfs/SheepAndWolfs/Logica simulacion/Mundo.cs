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
        public Animal? GetAnimalAt(int x, int y)
        {
            if (!Utils.IsValidCoordinates(x, y, _width, _height))
                return null;
            for (int i = 0; i < _animals.Count; i++)
            {
                Animal? animal = _animals[i];
                if (animal.coordenada!.EqualsToCoordenada(x, y))
                    return animal;
            }
            return null;
        }

        public Animal? RemoveAnimalAt(int index)
        {
            if (index < 0 || index >= _animals.Count)
                return null;
            Animal animal = _animals[index];
            _animals.RemoveAt(index);
            return animal;
        }

        public bool IsCasillaOcupada(int x, int y)
        {
            return (GetAnimalAt(x, y) != null || GetAnimalAt(x, y) != null)? false : true;
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
            //for (int i = 0; i < _animals.Count; i++)
            //    if (animal.Vida == 0)
            //        _animals.RemoveAt(animal);


        }

        public void RemoveDeadAnimals()
        {
            _animals.RemoveAll(animal => animal.food <= 0 || animal.water <= 0 || animal.stamina <= 0);
        }

        public void MoveAnimal(Animal animal, Mundo mundo)
        {
            if (animal == null || mundo == null)
                return;

            int[] XMovs = { -1, 0, 1, 0 };
            int[] YMovs = { 0, -1, 0, 1 };

            int direction = Utils.GetRandomNumber(0, 4);

            int newX = animal.coordenada.X + XMovs[direction];
            int newY = animal.coordenada.Y + YMovs[direction];

            if (Utils.IsValidCoordinates(newX, newY, mundo.GetWidth(), mundo.GetHeight()) &&
                CanAnimalMoveTo(animal, new Coordenada(newX, newY)))
            {
                animal.coordenada = new Coordenada(newX, newY);
            }
            //mover al animal, utilizar getanimalat, y pasar por dos for o funcion si puede moverse para empezar a plantear el movimiento
        }

        //CanAnimalMove? -> esto esta mal por el animaltype animal
        public bool CanAnimalMoveTo(Animal animal, Coordenada coor)
        {
            if (animal == null || coor == null)
                return false;

            Casilla? targetCasilla = GetCasillaAt(coor.X, coor.Y);
            Animal? targetAnimal = GetAnimalAt(coor.X, coor.Y);

            if (targetCasilla == null || targetCasilla.type == TerritorioType.ROCA ||
                targetCasilla.type == TerritorioType.AGUA)
                return false;
            if (animal.type == AnimalType.LOBO)
            {
                if (targetAnimal.type == AnimalType.OVEJA)
                    return true;
                if (targetAnimal.type == AnimalType.LOBO)
                    return false;
            }
            else if (animal.type == AnimalType.OVEJA)
            {
                if (targetAnimal != null)
                    return false;
            }
            return true;
        }


        //Aqui creo que se puede hacer un createAnimals que cree los animales y los añada a la lista de animales
        //TODO: ESTO tengo que volver a hacerlo
        public void CreateAnimals(int count, AnimalType type)
        {
            for (int i = 0; i < count; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y) == null)
                {
                    if (type == AnimalType.LOBO)
                    {
                        Lobo lobo = new Lobo(100, 100, 100, 100, AnimalType.LOBO);
                        lobo.coordenada = new Coordenada(x, y);
                        AddAnimal(lobo, x, y);
                    }
                    else if (type == AnimalType.OVEJA)
                    {
                        Oveja oveja = new Oveja(100, 100, 100, 100, AnimalType.OVEJA);
                        oveja.coordenada = new Coordenada(x, y);
                        AddAnimal(oveja, x, y);
                    }
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

                if (GetAnimalAt(x, y) == null)  // Evitar duplicados -> aqui hay que comprobar si la casilla a la que se mueve es roca o agua
                {
                    Lobo lobo = new Lobo(100, 100, 100, 100, AnimalType.LOBO);
                    //aqui le paso las cosas que le ponga en constructor como parametros
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

                if (GetAnimalAt(x, y) == null) // Evitar duplicados
                {
                    Oveja oveja = new(100, 100, 100, 100, AnimalType.OVEJA)
                    {
                        coordenada = new Coordenada(x, y)
                    };
                    AddAnimal(oveja, x, y);
                }
            }
        }

        //----------------------------------------------------------------------------------

        //TODO: esto no lo he usado
        public AnimalType GetAnimalTypeAt(int x, int y, AnimalType type)
        {
            Animal? animal = GetAnimalAt(x, y);
            return animal?.type ?? AnimalType.ANIMAL;
        }



        //TODO: esto no lo he usado
        public int IndexOfAnimal(Animal animal)
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                if (_animals[i].type == animal.type && _animals[i].coordenada == animal.coordenada)
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

        ////TODO: esto no lo he usado
        //internal void CreateRocks(int v)
        //{
        //    throw new NotImplementedException();
        //}

        ////TODO: esto no lo he usado
        //internal void CreateWaters(int v)
        //{
        //    throw new NotImplementedException();
        //}

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
