namespace SheepAndWolfs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Mundo mundo = new(40, 30);
            //Utils.GenerateRandomWorld(mundo);

            //prueba casilla
            //Mundo mundo = new Mundo(3, 3);
            //mundo.CrearCasillas();
            //Console.WriteLine("Casillas creadas exitosamente.");

            //Mundo mundo = new Mundo(5, 5); // Un mundo pequeño para probar
            
            //mundo.AddAnimal(new Lobo("Lobo1"),0, 0); // Añadir un lobo en la posición 0,0
            //mundo.AddAnimal(new Oveja("Oveja1"), 4, 4); // Añadir una oveja en la posición 4,4
            //Utils.DrawWorld(mundo);      // Imprimir el tablero

            Mundo mundo = new Mundo(8, 8);
            //realmente esto no es necesario, pero no se por que
            Utils.GenerateRandomWorld(mundo);

            mundo.CreateWolfs(Utils.GetRandomNumber(3, 6));
            mundo.CreateSheeps(Utils.GetRandomNumber(3, 6));

            //otro enfoque
            int randomanimals = Utils.GetRandomNumber(3, 6);
            mundo.CreateAnimals(randomanimals, AnimalType.LOBO);
            mundo.CreateAnimals(randomanimals, AnimalType.OVEJA);



            Utils.DrawWorld(mundo);


        }
    }
}
