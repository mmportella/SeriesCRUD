using System.Collections.Generic;

namespace SeriesCRUD.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(string id);
        void Insere(T entidade);
        void Exclui(string id);
        void Atualiza(string id, T entidade);
        int ProximoId();
    }
}