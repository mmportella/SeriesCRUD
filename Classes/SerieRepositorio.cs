using SeriesCRUD.Interfaces;
using System;
using System.Collections.Generic;

namespace SeriesCRUD.Classes
{
    class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();

        public void Atualiza(string id, Serie objeto)
        {
            listaSerie[InteiroId(id)] = objeto;
        }

        public void Exclui(string id)
        {
            listaSerie[InteiroId(id)].Excluir();
        }

        public void Insere(Serie objeto)
        {
            listaSerie.Add(objeto);
        }

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }



        public Serie RetornaPorId(string id)
        {
            return listaSerie[InteiroId(id)];
        }


        public static int InteiroId(string id)
        {
            bool valido;
            int intId;
            do
            {
                valido = int.TryParse(id, out intId);
                if (valido == true)
                {
                }
                else
                {
                    while (valido == false)
                    {
                        Console.WriteLine(" ");
                        Console.Write("Digite um ID válido (digite um número): ");
                        Console.ReadLine();
                        valido = int.TryParse(id, out intId);
                    }
                }
            } while (valido == false);
            return intId;
        }


    }
}