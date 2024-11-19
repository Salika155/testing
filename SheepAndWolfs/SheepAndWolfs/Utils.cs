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
            Console.Clear();

            for (int y = 0; y < mundo.GetHeight(); y++)
            {
                for (int x = 0; x < mundo.GetWidth(); x++)
                {
                    Animal? animal = mundo.GetAnimalAt(x, y, AnimalType.ANIMAL); 
                    Casilla? casilla = mundo.GetCasillaAt(x, y); 

                    if (animal is Oveja)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" O ");
                    }
                    else if (animal is Lobo)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" L ");
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
                        Console.Write("   ");
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


        //public Animal CreateAnimal(Animal animal)
        //{
        //    return animal;
        //}

        //public static Animal? GetAnimalAt(Mundo mundo, int x, int y, AnimalType type)
        //{
        //    return mundo.GetAnimalAt(x, y, type);
        //}
    }
}

//resetcolor
