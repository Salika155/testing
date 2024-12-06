using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        //esto tiene que ir en private, y deberia de crear una copia de cada uno
        public Casilla[] GetAllCasillas() => _casillas;
        public List<Animal> GetAllAnimals() => _animals;

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
                if (animal.GetCoordenada()!.EqualsToCoordenada(x, y))
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

            animal.SetCoordenada(x, y); // Asignar coordenadas al animal
            _animals.Add(animal);
        }

        //hace falta un removeAnimal para cuando un lobo se coma una oveja o un animal se muera por atributos
        public void RemoveAnimal(Animal animal)
        {
            if (animal == null)
                return;
            _animals.Remove(animal);
        }

        public void MoveAnimal(Animal? animal)
        {
            if (animal == null)
                return;

            int[] XMovs = [-1, 0, 1, 0 ];
            int[] YMovs = [0, -1, 0, 1];

            int direction = Utils.GetRandomNumber(0, 4);

            int newX = animal.GetCoordenada().X + XMovs[direction];
            int newY = animal.GetCoordenada().Y + YMovs[direction];

            if (Utils.IsValidCoordinates(newX, newY, GetWidth(), GetHeight()) &&
                CanAnimalMoveTo(animal, new Coordenada(newX, newY)))
            {
                animal.SetCoordenada(newX, newY);
            }
            //mover al animal, utilizar getanimalat, y pasar por dos for o funcion si puede moverse para empezar a plantear el movimiento
        }

        //CanAnimalMove? -> esto esta mal por el animaltype animal
        public bool CanAnimalMoveTo(Animal? animal, Coordenada coor)
        {
            if (animal == null || coor == null)
                return false;

            Casilla? targetCasilla = GetCasillaAt(coor.X, coor.Y);
            Animal? targetAnimal = GetAnimalAt(coor.X, coor.Y);

            if (targetCasilla == null || targetCasilla.type == TerritorioType.ROCA ||
                targetCasilla.type == TerritorioType.AGUA)
                return false;
            return true;
        }

        //Aqui creo que se puede hacer un createAnimals que cree los animales y los añada a la lista de animales
        //TODO: esto funciona
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
                        int countlobo = 0;
                        //instancia a utils para los campos comida para generarle valores aleatorios
                        int food = Utils.GetRandomNumber(50, 500);
                        int water = Utils.GetRandomNumber(50, 500);
                        int stamina = Utils.GetRandomNumber(50, 500);
                        int sleep = Utils.GetRandomNumber(50, 500);
                        string name = "Lobo " + countlobo++;
                        int velocidad = 100;
                        Lobo lobo = new Lobo(food, water, stamina, sleep, AnimalType.LOBO, name, velocidad);
                        lobo.SetCoordenada(x, y);
                        AddAnimal(lobo, x, y);
                    }
                    else if (type == AnimalType.OVEJA)
                    {
                        int countoveja = 0;
                        //instancia a utils para los campos comida para generarle valores aleatorios
                        int food = Utils.GetRandomNumber(50, 500);
                        int water = Utils.GetRandomNumber(50, 500);
                        int stamina = Utils.GetRandomNumber(50, 500);
                        int sleep = Utils.GetRandomNumber(50, 500);
                        string name = "Oveja " + countoveja++;
                        int velocidad = 50;
                        Oveja oveja = new Oveja(food, water, stamina, sleep, AnimalType.OVEJA, name, velocidad);
                        oveja.SetCoordenada(x, y);
                        AddAnimal(oveja, x, y);
                    }
                }
            }
        }

        //TODO: esto funciona
        public void CreateWolfs(int count)
        {
            int countlobo = count;

            for (int i = 0; i <= countlobo; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y) == null)  // Evitar duplicados -> aqui hay que comprobar si la casilla a la que se mueve es roca o agua
                {
                    Lobo lobo = new Lobo(100, 100, 100, 100, AnimalType.LOBO, "lobo" + CountAnimalsType(AnimalType.LOBO), 100);
                    //aqui le paso las cosas que le ponga en constructor como parametros
                    lobo.SetCoordenada(x, y);
                    AddAnimal(lobo, x, y);
                }
            }
        }

        //TODO: esto funciona
        public void CreateSheeps(int count)
        {
            int countoveja = count;
            for (int i = 0; i <= countoveja; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y) == null) // Evitar duplicados
                {
                    Oveja oveja = new Oveja(100, 100, 100, 100, AnimalType.OVEJA, "oveja " + CountAnimalsType(AnimalType.OVEJA), 50);
                    oveja.SetCoordenada(x, y);
                    AddAnimal(oveja, x, y);
                }
            }
        }

        //TODO: esto lo he usado
        public int CountAnimals() => _animals.Count;


        //TODO: esto lo he usado
        public int CountAnimalsType(AnimalType type)
        {
            if (_animals.Count == 0)
                return 0;

            int aux = 0;
            for (int i = 0; i < _animals.Count; i++)
                if (_animals[i].GetType() == type)
                    aux += 1;
            return aux;
        }

        //Para actualizar los atributos a cada turno que pasa a los animales
        public void ActualizarEstadoAnimalesPorTurno()
        {
            foreach (var animales in _animals)
            {
                animales.SetSaciedad(animales.GetSaciedad() - 10);
                animales.SetHidratacion(animales.GetHidratacion() - 10);
                animales.SetSueño(animales.GetSueño() - 10);

                if (animales.GetSaciedad() <= 0)
                    animales.SetSaciedad(0);
                if (animales.GetHidratacion() <= 0)
                    animales.SetHidratacion(0);
                if (animales.GetSueño() <= 0)
                    animales.SetSueño(0);
            }
        }

        public void EliminarAnimalesmuertos()
        {
            for (int i = _animals.Count - 1; i >= 0; i--)
            {
                Animal animal = _animals[i];
                if (animal.GetSaciedad() <= 0 || animal.GetHidratacion() <= 0 || animal.GetSueño() <= 0)
                    RemoveAnimalAt(i);
            }
        }

        //Experimento  con CreateAnimals ---------------------------------------------------------------------



        public void CreateAnimals2(int count, AnimalType type)
        {
            for (int i = 0; i < count; i++)
            {
                int x = Utils.GetRandomNumber(0, _width);
                int y = Utils.GetRandomNumber(0, _height);

                if (GetAnimalAt(x, y) == null) // Evitar duplicados
                {
                    Animal animal;
                    if (type == AnimalType.LOBO)
                    {
                        animal = CreateWolf2();
                    }
                    else if (type == AnimalType.OVEJA)
                    {
                        animal = CreateSheep2();
                    }
                    else
                    {
                        throw new ArgumentException("Tipo de animal no soportado");
                    }
                    animal.SetCoordenada(x, y);
                    AddAnimal(animal, x, y);
                }
            }
        }

        private Lobo CreateWolf2()
        {
            int food = Utils.GetRandomNumber(50, 500);
            int water = Utils.GetRandomNumber(50, 500);
            int stamina = Utils.GetRandomNumber(50, 500);
            int sleep = Utils.GetRandomNumber(50, 500);
            string name = "Lobo " + CountAnimalsType(AnimalType.LOBO);
            int velocidad = 100;

            return new Lobo(food, water, stamina, sleep, AnimalType.LOBO, name, velocidad);
        }

        private Oveja CreateSheep2()
        {
            int food = Utils.GetRandomNumber(50, 500);
            int water = Utils.GetRandomNumber(50, 500);
            int stamina = Utils.GetRandomNumber(50, 500);
            int sleep = Utils.GetRandomNumber(50, 500);
            string name = "Oveja " + CountAnimalsType(AnimalType.OVEJA);
            int velocidad = 50;

            return new Oveja(food, water, stamina, sleep, AnimalType.OVEJA, name, velocidad);
        }




        //----------------------------------------------------------------------------------

        //TODO: esto no lo he usado
        //ES POSIBLE QUE ME HAGA FALTA PARA COMPROBAR EL ANIMAL EN LA CASILLA Y QUE LOS LOBOS ATAQUEN
        public AnimalType GetAnimalTypeAt(int x, int y, AnimalType type)
        {
            Animal? animal = GetAnimalAt(x, y);
            return animal?.GetType() ?? AnimalType.ANIMAL;
        }

        //TODO: esto no lo he usado
        public int IndexOfAnimal(Animal animal)
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                if (_animals[i].GetType() == animal.GetType() 
                    && _animals[i].GetCoordenada() == animal.GetCoordenada())
                    return i;
            }
            return -1;
        }

        

        //TODO: esto no lo he usado
        public bool ContainsCasilla(int x, int y)
        {
            return Utils.IndexOfCasilla(x, y, _width) != -1;
        }

        //TODO: esto no lo he usado
        public int CountCasilla() => _casillas.Length;
 
        public bool EstaCercaHierba(Animal animal)
        {
            foreach (var casilla in _casillas)
            {
                if (casilla.type == TerritorioType.HIERBA)
                {
                    if (Utils.IsValidCoordinates(casilla.coordenada.X, casilla.coordenada.Y, GetWidth(), GetHeight()))
                    {
                        if (Utils.GetDistance(animal.GetCoordenada(), casilla.coordenada) <= 2)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool EstaAguaCercaDelAnimal(Animal animal)
        {
            foreach (var casilla in _casillas)
            {
                if (casilla.type == TerritorioType.AGUA)
                {
                    if (Utils.IsValidCoordinates(casilla.coordenada.X, casilla.coordenada.Y, GetWidth(), GetHeight()))
                    {
                        if (Utils.GetDistance(animal.GetCoordenada(), casilla.coordenada) <= 2)
                            return true;
                    }
                }
            }
            return false;
        }

        

        

        //metodo para hallar casillas alrededor de un animal, las casillas son arrays


        //TODO: esto no lo he usado
        //public void ImprimirCasillas()
        //{
        //    for (int y = 0; y < _height; y++)
        //    {
        //        for (int x = 0; x < _width; x++)
        //        {
        //            int index = Utils.IndexOfCasilla(x, y, _width);
        //            if (_casillas[index] != null)
        //                Console.Write($"[{_casillas[index].coordenada.X},{_casillas[index].coordenada.Y}] ");
        //            else
        //                Console.Write("[null] ");
        //        }
        //        Console.WriteLine(); // Nueva línea para cada fila
        //    }
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


        //int index = y * width + x
        //index / ancho = cociente la y y resto la x
    }    
}
