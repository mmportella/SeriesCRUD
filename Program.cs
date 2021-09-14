using System;
using SeriesCRUD.Classes;
using SeriesCRUD.Enum;

namespace SeriesCRUD
{
    class Program
    {

        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        Console.WriteLine(" ");
                        Console.Write(" Pressione Enter para continuar... ");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        break;
                    case "2":
                        InserirSerie();
                        Console.WriteLine(" ");
                        Console.Write(" Pressione Enter para continuar... ");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        break;
                    case "3":
                        AtualizarSerie();
                        Console.WriteLine(" ");
                        Console.Write(" Pressione Enter para continuar... ");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        break;
                    case "4":
                        ExcluirSerie();
                        Console.WriteLine(" ");
                        Console.Write(" Pressione Enter para continuar... ");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        break;
                    case "5":
                        VisualizarSerie();
                        Console.WriteLine(" ");
                        Console.Write(" Pressione Enter para continuar... ");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();

            }
        }



        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ##########   Bem-Vindo! DIO Séries a seu dispor.   ##########");
            Console.WriteLine(" ##########             Menu de Opções              ##########");
            Console.WriteLine(" ");
            Console.WriteLine(" 1 - Listar Séries");
            Console.WriteLine(" 2 - Inserir Nova Série");
            Console.WriteLine(" 3 - Atualizar Série");
            Console.WriteLine(" 4 - Excluir Série");
            Console.WriteLine(" 5 - Visualizar Série");
            Console.WriteLine(" X - Sair");
            Console.WriteLine(" ");
            Console.Write(" Informe a Opção Desejada: ");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine(" ");
            return opcaoUsuario;
        }



        private static void ListarSeries()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ##########         LISTA DE SÉRIES DA DIO       ##########");
            Console.WriteLine(" ");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine(" Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine(" #ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
                Console.WriteLine(" ");
            }
        }



        private static void InserirSerie()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ##########         CADASTRO DE NOVA SÉRIE       ##########");
            Console.WriteLine(" ");

            // GENERO
            foreach (int i in Genero.GetValues(typeof(Genero)))
            {
                Console.WriteLine(" {0}-{1}", i, Genero.GetName(typeof(Genero), i));
            }
            int entradaGenero;
            bool taNaLista = false;
            do
            {
                Console.WriteLine(" ");
                Console.WriteLine(" Escolha o gênero dentre as opções acima.");
                Console.Write(" Escolha um código que está na lista e digite apenas o número: ");
                string strGen = Console.ReadLine();
                bool intValido = int.TryParse(strGen, out entradaGenero);
                if (intValido == true)
                {
                    if (entradaGenero > 0 && entradaGenero <= Genero.GetValues(typeof(Genero)).Length)
                    {
                        taNaLista = true;
                    }
                }
            } while (taNaLista == false);


            // TITULO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ##########         CADASTRO DE NOVA SÉRIE       ##########");
            Console.WriteLine(" ");
            Console.Write(" Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            // ANO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ##########         CADASTRO DE NOVA SÉRIE       ##########");
            Console.WriteLine(" ");
            Console.Write(" Digite o Ano de Início da Série: ");
            string strAno = Console.ReadLine();
            bool anoValido = int.TryParse(strAno, out int entradaAno);
            if (anoValido)
            {
            }
            else
            {
                while (anoValido == false)
                {
                    Console.Write(" Ano inválido. Digite um número válido: ");
                    strAno = Console.ReadLine();
                    anoValido = int.TryParse(strAno, out entradaAno);
                }
            }

            // DESCRICAO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ##########         CADASTRO DE NOVA SÉRIE       ##########");
            Console.WriteLine(" ");
            Console.Write(" Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            int Id = repositorio.ProximoId();

            Serie novaSerie = new Serie(Id: Id.ToString(), Genero: (Genero)entradaGenero, Titulo: entradaTitulo, Ano: entradaAno, Descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ##########         CADASTRO DE NOVA SÉRIE       ##########");
            Console.WriteLine(" ");
            Console.WriteLine(" Sua nova série: ");
            Console.WriteLine(" ");
            Visualizar(Id.ToString());
        }



        private static void AtualizarSerie()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######    ATUALIZAR CADASTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            Console.Write(" Digite o código da série que deseja atualizar: ");
            string indiceSerie = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine(" A série que você selecionou é: ");
            Console.WriteLine(" ");
            int vazia = Visualizar(indiceSerie);
            if (vazia == 0)
            {
                return;
            }         
            Console.WriteLine(" ");
            Console.WriteLine(" Deseja continuar?");
            Console.WriteLine(" ");
            Console.WriteLine(" 1 - Sim");
            Console.WriteLine(" 2 - Não");
            Console.WriteLine(" ");
            Console.Write("--> ");

            var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Atualizar(indiceSerie);
                        return;
                    default:
                        return;
                }
            
            
        }

        private static void Atualizar(string indiceSerie)
        {

            // GENERO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######    ATUALIZAR CADASTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            foreach (int i in Genero.GetValues(typeof(Genero)))
            {
                Console.WriteLine(" {0}-{1}", i, Genero.GetName(typeof(Genero), i));
            }
            int entradaGenero;
            bool taNaLista = false;
            do
            {
                Console.WriteLine(" ");
                Console.WriteLine(" Escolha um novo gênero para a série.");
                Console.Write(" Escolha um código que está na lista acima e digite apenas o número: ");
                string strGen = Console.ReadLine();
                bool intValido = int.TryParse(strGen, out entradaGenero);
                if (intValido == true)
                {
                    if (entradaGenero > 0 && entradaGenero <= Genero.GetValues(typeof(Genero)).Length)
                    {
                        taNaLista = true;
                    }
                }
            } while (taNaLista == false);


            // TITULO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######    ATUALIZAR CADASTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            Console.Write(" Digite o novo título da série: ");
            string entradaTitulo = Console.ReadLine();

            // ANO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######    ATUALIZAR CADASTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            Console.Write(" Digite o ano de início da série: ");
            string strAno = Console.ReadLine();
            bool anoValido = int.TryParse(strAno, out int entradaAno);
            if (anoValido)
            {
            }
            else
            {
                while (anoValido == false)
                {
                    Console.Write(" Ano inválido. Digite um número válido: ");
                    strAno = Console.ReadLine();
                    anoValido = int.TryParse(strAno, out entradaAno);
                }
            }

            // DESCRICAO
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######    ATUALIZAR CADASTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            Console.Write(" Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();


            Serie atualizaSerie = new Serie(Id: indiceSerie, Genero: (Genero)entradaGenero, Titulo: entradaTitulo, Ano: entradaAno, Descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);

            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######    ATUALIZAR CADASTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            Console.WriteLine(" Sua série atualizada: ");
            Console.WriteLine(" ");
            Visualizar(indiceSerie);
        }



        private static void ExcluirSerie()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########    Banco de Dados de Séries da DIO   ##########");
            Console.WriteLine(" ######      EXCLUIR REGISTRO DE SÉRIE EXISTENTE    #######");
            Console.WriteLine(" ");
            Console.Write(" Digite o id da série que deseja excluir: ");
            string indiceSerie = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine(" A série que você selecionou é: ");
            Console.WriteLine(" ");
            int vazia = Visualizar(indiceSerie);
            if (vazia == 0)
            {
                return;
            }
            Console.WriteLine(" ");
            Console.WriteLine(" Deseja continuar?");
            Console.WriteLine(" ");
            Console.WriteLine(" 1 - Sim");
            Console.WriteLine(" 2 - Não");
            Console.WriteLine(" ");
            Console.Write("--> ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    repositorio.Exclui(indiceSerie);
                    Console.WriteLine(" ");
                    Console.WriteLine(" Sua série foi marcada com a tag excluída.");
                    return;
                default:
                    return;
            }

        }



        private static void VisualizarSerie()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ##########   Banco de Dados de Séries da DIO    ##########");
            Console.WriteLine(" ######    VISUALIZAR REGISTRO DE SÉRIE EXISTENTE   #######");
            Console.WriteLine(" ");
            Console.Write(" Digite o código da série: ");
            string id = Console.ReadLine();
            Console.WriteLine(" ");
            if (repositorio.Lista().Count == 0)
            {
                Console.WriteLine(" O banco de dados está vazio.");
            }
            else
            {
                Visualizar(id);
            }
        }

        public static int Visualizar(string id)
        {
            try
            {
                var serie = repositorio.RetornaPorId(id);
                Console.WriteLine(serie);
                return 1;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(" Não encontramos essa série.");
                return 0;
            }
            catch (Exception)
            {
                Console.WriteLine(" Não encontramos essa série.");
                return 0;
            }
            finally
            {
            }
        }



    }
}