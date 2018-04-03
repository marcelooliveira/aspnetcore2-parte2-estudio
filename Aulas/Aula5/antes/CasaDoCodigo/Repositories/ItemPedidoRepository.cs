using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido Get(ItemPedido itemPedido);
        void Remove(ItemPedido itemPedidoDB);
    }

    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public ItemPedido Get(ItemPedido itemPedido)
        {
            return dbSet
                .Where(i => i.Id == itemPedido.Id)
                .SingleOrDefault();
        }

        public void Remove(ItemPedido itemPedidoDB)
        {
            dbSet.Remove(itemPedidoDB);
            contexto.SaveChanges();
        }
    }
}
