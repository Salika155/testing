using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepAndWolfs
{
    public class AI
    {
        public enum IAType
        {
            Mover,
            Comer,
            Beber,
            Dormir,
            COUNT
            //posible que si hago mover animal acorte y solo tenga que pasarle el tipo para saber si es un lobo o una oveja
        }

        private readonly IAType[] _iATypes = [IAType.Mover, IAType.Comer, IAType.Beber, IAType.Dormir];

        //Hacer funcion si oveja puede moverse, o si lobo puede moverse

        //world w = new world
        //utils.InitializeRandomWorld(w)
        //while (w.ContainsSheeps() || w.ContainsWater()
        //w.ExecuteTurns();

        //TODO: necesario para que funcione por turnos
        public void ExecuteTurns(Mundo mundo)
        {
            for (int i = 0; i < 30; i++)
            {
                foreach (Animal animal in mundo.GetAllAnimals())
                {
                    var actionDecided = DecideAnimalAccion(animal, mundo);
                    Console.WriteLine($"Animal {animal} decide: {actionDecided}");
                    HacerAccionCercanaAnimal(animal, mundo);
                    #region noseuse
                    //switch (actionDecided)
                    //{
                    //    case IAType.Mover:
                    //        Utils.MoveAnimal(animal, mundo);
                    //        break;
                    //    case IAType.Comer:
                    //        ComerHierbaCercanaAnimal(animal, mundo);
                    //        break;
                    //    case IAType.Beber:
                    //        BeberAguaCercanaAnimal(animal, mundo);
                    //        break;
                    //    case IAType.Dormir:
                    //        DormirAnimal(animal, mundo);
                    //        break;
                    //}
                    #endregion
                }
                mundo.ActualizarEstadoAnimalesPorTurno();
                mundo.EliminarAnimalesmuertos();
                Utils.DrawWorld(mundo);
                Thread.Sleep(500);
            }
        }

        //TODO: necesario para que funcione por turnos
        private static IAType DecideAnimalAccion(Animal animal, Mundo mundo)
        {
            if (animal.GetSaciedad() <= 50 && mundo.EstaCercaHierba(animal))
                return IAType.Comer;
            if (animal.GetHidratacion() <= 50 && mundo.EstaAguaCercaDelAnimal(animal))
                return IAType.Beber;
            if (animal.GetSueño() <= 50)
                return IAType.Dormir;
            return IAType.Mover;
        }

        public void HacerAccionCercanaAnimal(Animal animal, Mundo mundo)
        {
            IAType accionDecidida = DecideAnimalAccion(animal, mundo);

            if (accionDecidida == IAType.Mover)
                Utils.MoveAnimal(animal, mundo);
            else
            {
                Casilla? casillaCercana = EncontrarCasillaTipoCercana(animal, mundo, TerritorioType.HIERBA);
                switch (accionDecidida)
                {
                    case IAType.Comer:
                        if (accionDecidida == IAType.Comer)
                            ComerHierbaCercanaAnimal(animal, mundo);
                        break;
                    case IAType.Beber:
                        if (accionDecidida == IAType.Beber)
                            BeberAguaCercanaAnimal(animal, mundo);
                        break;
                    case IAType.Dormir:
                        if (accionDecidida == IAType.Dormir)
                            DormirAnimal(animal, mundo);
                        break;
                }
            }
        }

        //A PARTIR DE AQUI ES NUEVO
        public static Casilla? EncontrarCasillaTipoCercana(Animal animal, Mundo mundo, TerritorioType type)
        {
            Casilla? casillaTipoObjetivo = null;
            double minDistance = double.MaxValue;
            var _casillasArray = mundo.GetAllCasillas();
            for (int i = 0; i < _casillasArray.Length; i++)
            {
                Casilla casilla = _casillasArray[i];
                if (casilla.type == type)
                {
                    double distance = Utils.GetDistance(animal.GetCoordenada(), casilla.coordenada);
                    if (distance < 2 && distance < minDistance)
                    {
                        minDistance = distance;
                        casillaTipoObjetivo = casilla;
                    }
                }
            }
            return casillaTipoObjetivo;
        }

        public static void ComerHierbaCercanaAnimal(Animal animal, Mundo mundo)
        {
            Casilla? casillaHierba = EncontrarCasillaTipoCercana(animal, mundo, TerritorioType.HIERBA);
            if (casillaHierba != null && DecideAnimalAccion(animal, mundo) == IAType.Comer)
            {
                animal.SetSaciedad(animal.GetSaciedad() + 50);
            }
        }

        public static void BeberAguaCercanaAnimal(Animal animal, Mundo mundo)
        {
            Casilla? casillaAgua = EncontrarCasillaTipoCercana(animal, mundo, TerritorioType.AGUA);
            if (casillaAgua != null && DecideAnimalAccion(animal, mundo) == IAType.Beber)
            {
                animal.SetHidratacion(animal.GetHidratacion() + 50);
            }
        }

        public static void DormirAnimal(Animal animal, Mundo mundo)
        {
            Casilla? casillaDormir = EncontrarCasillaTipoCercana(animal, mundo, TerritorioType.TIERRA);
            if (DecideAnimalAccion(animal, mundo) == IAType.Dormir)
            {
                animal.SetSueño(animal.GetSueño() + 50);
            }
        }

        //public void MoveAnimal(Animal animal, Mundo mundo)
        //{
        //    // Implementa la lógica para mover al animal. Por ejemplo:
        //    var nuevaPosicion = Utils.GetNewPosition(animal.GetCoordenada());
        //    animal.SetCoordenada(nuevaPosicion); // Actualiza la posición del animal
        //    Console.WriteLine($"Animal moved to new position: {nuevaPosicion}");
        //}

        #region nosirve

        //esto ahora no lo uso
        //public void MoveAnimalType(IAType type, Animal animal)
        //{
        //    if (animal.GetSaciedad() <= 50)
        //        type = IAType.Comer;
        //    if (animal.GetHidratacion() <= 50)
        //        type = IAType.Beber;
        //    if (animal.GetSueño() <= 50)
        //        type = IAType.Dormir;
        //    else
        //        type = IAType.Mover;
        //}


        //esto ya no lo uso
        //public Casilla? EstaAguaCerca(Animal animal, Mundo mundo)
        //{
        //    Casilla? casillaAgua = null;
        //    double minDistance = double.MaxValue;
        //    var _casillasArray = mundo.GetAllCasillas();
        //    for (int i = 0; i < _casillasArray.Length; i++)
        //    {
        //        Casilla casilla = _casillasArray[i];
        //        if (casilla.type == TerritorioType.AGUA)
        //        {
        //            double distance = Utils.GetDistance(animal.GetCoordenada(), casilla.coordenada);
        //            if (distance < 2 && distance < minDistance)
        //            {
        //                minDistance = distance;
        //                casillaAgua = casilla;
        //            }
        //        }
        //    }
        //    return casillaAgua;
        //}

        ////esto ya no lo uso
        //public Casilla? EstaCercaHierba(Animal animal, Mundo mundo)
        //{
        //    Casilla? casillaHierba = null;
        //    double minDistance = double.MaxValue;
        //    var _casillasArray = mundo.GetAllCasillas();
        //    for (int i = 0; i < _casillasArray.Length; i++)
        //    {
        //        Casilla casilla = _casillasArray[i];
        //        if (casilla.type == TerritorioType.HIERBA)
        //        {
        //            double distance = Utils.GetDistance(animal.GetCoordenada(), casilla.coordenada);
        //            if (distance < 2 && distance < minDistance)
        //            {
        //                minDistance = distance;
        //                casillaHierba = casilla;
        //            }
        //        }
        //    }
        //    return casillaHierba;
        //}

        ////TODO: necesario para que funcione por turnos
        //public void ComerHierbaCercanaAnimal1(Animal animal, Mundo mundo)
        //{
        //    Casilla? casillaHierba = EstaCercaHierba(animal, mundo);
        //    if (casillaHierba != null && DecideAnimalAccion(animal, mundo) == IAType.Comer)
        //    {
        //        animal.SetSaciedad(animal.GetSaciedad() + 50);
        //    }
        //}

        //////TODO: necesario para que funcione por turnos
        //public void BeberAguaCercanaAnimal1(Animal animal, Mundo mundo)
        //{
        //    Casilla? casillaAgua = EstaAguaCerca(animal, mundo);
        //    if (casillaAgua != null && DecideAnimalAccion(animal, mundo) == IAType.Beber)
        //    {
        //        animal.SetHidratacion(animal.GetHidratacion() + 50);
        //    }
        //}

        //public void DormirAnimal1(Animal animal, Mundo mundo)
        //{
        //    Casilla? casillaDormir = EstaCercaHierba(animal, mundo);
        //    if (DecideAnimalAccion(animal, mundo) == IAType.Dormir)
        //    {
        //        animal.SetSueño(animal.GetSueño() + 50);
        //    }
        //}
        #endregion
    }
}
