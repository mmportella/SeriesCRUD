using System;
using SeriesCRUD.Enum;

namespace SeriesCRUD.Classes
{
    class Serie : EntidadeBase
    {
        private Genero Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; }
        private int Ano { get; set; }
        private bool Excluido { get; set; }

        public Serie() { 
        }

        public Serie(string Id, Genero Genero, string Titulo, int Ano, string Descricao)
        {

            this.Id = SerieRepositorio.InteiroId(Id);
            this.Genero = Genero;
            this.Titulo = Titulo;
            this.Ano = Ano;
            this.Descricao = Descricao;
            this.Excluido = false;
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "    Título: " + this.Titulo + Environment.NewLine;
            retorno += "    Gênero: " + this.Genero + Environment.NewLine;
            retorno += " Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "       Ano: " + this.Ano;
            return retorno;
        }

        public string retornaTitulo()
        {
            return this.Titulo;
        }

        public string RetornaDescricao()
        {
            return this.Descricao;
        }

        public Genero RetornaGenero()
        {
            return this.Genero;
        }

        public int retornaId()
        {
            return this.Id;
        }

        public int retornaAno()
        {
            return this.Ano;
        }

        public bool retornaExcluido()
        {
            return this.Excluido;
        }
        public void Excluir()
        { 
            this.Excluido = true;
        }

    }
}