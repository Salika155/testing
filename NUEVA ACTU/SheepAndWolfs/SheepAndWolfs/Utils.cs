using SheepAndWolfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    public enum TerritorioType
    {
        TIERRA,
        HIERBA,
        AGUA,
        ROCA,
        COUNT
    }

    public class Utils
    {
        private static readonly Random random = new Random();

        //TODO: esto no lo he usado
        //GetCasillaAt
        public static Casilla? GetCasillaAt(Mundo mundo, int x, int y)
        {
            if (!Utils.IsValidCoordinates(x, y, mundo.GetWidth(), mundo.GetHeight()))
                return null;
            return mundo.GetCasillaAt(x, y);
        }

        //TODO: esto funciona
        public static int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        //TODO: esto funciona
        public static int IndexOfCasilla(int x, int y, int width)
        {
            if (x < 0 || y < 0 || width <= 0)
                return -1;

            return y * width + x;
        }

        //TODO: esto funciona
        public static TerritorioType GenerateRandomType()
        {
            //TerritorioType[] terreno = { TerritorioType.AGUA, TerritorioType.HIERBA, TerritorioType.VACIO };
            int index = random.Next(0,(int)TerritorioType.COUNT);
            //return terreno[index];
            //mejor opcion
            return (TerritorioType)index;
        }

        //TODO: esto funciona
        public static bool IsValidCoordinates(int x, int y, int width, int height)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        //TODO: esto funciona
        public static void DrawWorld(Mundo mundo)
        {
            for (int y = 0; y < mundo.GetHeight(); y++)
            {
                for (int x = 0; x < mundo.GetWidth(); x++)
                {
                    Animal? animal = mundo.GetAnimalAt(x, y); 
                    Casilla? casilla = mundo.GetCasillaAt(x, y);

                    if (animal != null)
                    {
                        if (animal is Oveja)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write(" O ");
                        }
                        else if (animal is Lobo)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write(" L ");
                        }
                    }
                    else if (casilla is not null)
                    {
                        switch (casilla.type)
                        {
                            case TerritorioType.TIERRA:
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(" T ");
                                break;
                            case TerritorioType.AGUA:
                                Console.BackgroundColor = ConsoleColor.Cyan;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" A ");
                                break;
                            case TerritorioType.HIERBA:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write(" H ");
                                break;
                            case TerritorioType.ROCA:
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(" R ");
                                break;   
                        }
                    }
                    else
                    {
                        Console.WriteLine(" ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        //TODO: esto no lo he usado
        public static bool EqualsToCoordenada(Coordenada coor, int x, int y)
        {
            if (coor == null || x < 0 || y < 0)
                return false;
            return coor.X == x && coor.Y == y;
        }

        //TODO: esto no lo he usado
        public static (int x, int y) GetCoordinatesFromIndex(int index, int width)
        {
            if (width == 0 || index < 0)
                return (int.MinValue, int.MinValue);
            return (index % width, index / width);
        }

        //TODO: esto no lo he usado
        public static void GenerateRandomWorld(Mundo mundo)
        {
            if (mundo == null)
                return;

            for (int y = 0; y < mundo.GetHeight(); y++)
            {
                for (int x = 0; x < mundo.GetWidth(); x++)
                {
                    TerritorioType type = GenerateRandomType();
                    Casilla? casilla = mundo.GetCasillaAt(x, y);
                    if (casilla != null)
                    {
                        casilla.type = type;
                    }
                }
            }
        }

        public static double GetDistanceBetweenAnimals(Animal a, Animal b)
        {
            return GetDistance(a.GetCoordenada(), b.GetCoordenada());
        }

        public static double GetDistance(Coordenada? c1, Coordenada? c2)
        {
            if (c1 is null || c2 is null)
                return double.MaxValue;
            int cx = c2.X - c1.X;
            int cy = c2.Y - c1.Y;
            return Math.Sqrt(cx * cx + cy * cy);
        }

        //repetido en mundo, este tengo que ver que hacer
        public static void MoveAnimal(Animal animal, Mundo mundo)
        {
            if (animal == null || mundo == null)
                return;

            int[] XMovs = { -1, 0, 1, 0 };
            int[] YMovs = { 0, -1, 0, 1 };

            int direction = Utils.GetRandomNumber(0, 4);
            var coorde = animal.GetCoordenada();

            int newX = coorde.X + XMovs[direction];
            int newY = coorde.Y + YMovs[direction];

            Console.WriteLine($"Intentando mover {animal} de ({coorde.X}, {coorde.Y}) a ({newX}, {newY})");

            if (IsValidCoordinates(newX, newY, mundo.GetWidth(), mundo.GetHeight()) &&
                mundo.CanAnimalMoveTo(animal, new Coordenada(newX, newY)))
            {
                coorde = new Coordenada(newX, newY);
                Console.WriteLine($"{animal} se movió a ({newX}, {newY})");
            }
            else
            {
                Console.WriteLine($"{animal} no pudo moverse a ({newX}, {newY})");
            }
            //mover al animal, utilizar getanimalat, y pasar por dos for o funcion si puede moverse para empezar a plantear el movimiento
        }
        
        //GetAnimalsArround
        //GetAnimalsSortedByDistance
        public static List<Animal> GetAnimalsSortedByDistance(Animal animal, List<Animal> animals)
        {
            if (animal == null || animals == null || animals.Count == 0)
                return new List<Animal>();

            for (int i = 0; i < animals.Count; i++)
            {
                for (int j = i + 1; j < animals.Count; j++)
                {
                    var distanceI = GetDistanceBetweenAnimals(animal, animals[i]);
                    var distanceJ = GetDistanceBetweenAnimals(animal, animals[j]);
                    if (distanceI > distanceJ)
                    {
                        Animal temp = animals[i];
                        animals[i] = animals[j];
                        animals[j] = temp;
                    }
                }
            }
            return animals;
        }

        //GetTerritoryType para saber que tipo es
        public static TerritorioType GetTerritorioType(Casilla casilla)
        {
            if (casilla.type == TerritorioType.AGUA)
                return TerritorioType.AGUA;
            if (casilla.type == TerritorioType.HIERBA)
                return TerritorioType.HIERBA;
            if (casilla.type == TerritorioType.ROCA)
                return TerritorioType.ROCA;
            else
                return TerritorioType.TIERRA;
        }
    }
}

//resetcolor
