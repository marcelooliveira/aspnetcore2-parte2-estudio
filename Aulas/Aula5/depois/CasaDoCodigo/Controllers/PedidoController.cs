using Caelum.Stella.CSharp.Http;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public IActionResult Carrossel()
        {
            return ViewComCarrinho(produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            return ViewComCarrinho(pedidoRepository.GetCarrinhoViewModel());
        }

        public IActionResult Cadastro()
        {
            Pedido pedido = pedidoRepository.GetPedido();
            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }
            else
            {
                return ViewComCarrinho(pedido.Cadastro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                return ViewComCarrinho(pedidoRepository.UpdateCadastro(cadastro));
            }
            else
            {
                return RedirectToAction("Cadastro");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateItemPedidoResponse PostQuantidade([FromBody]ItemPedido input)
        {
            return pedidoRepository.UpdateQuantidade(input);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public Endereco GetEndereco(string cep)
        {
            return new ViaCEP().GetEndereco(cep);
        }

        private ViewResult ViewComCarrinho(object model)
        {
            SetQtdProdutos();
            return View(model);
        }

        private void SetQtdProdutos()
        {
            ViewData["QtdProdutos"] = pedidoRepository.GetPedido().Itens.Count();
        }
    }
}
