using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        [Required]
        [DataMember]
        public string Codigo { get; private set; }
        [Required]
        [DataMember]
        public string Nome { get; private set; }
        [Required]
        [DataMember]
        public decimal Preco { get; private set; }

        public Produto(string codigo, string nome, decimal preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
        }
    }

    public class Cadastro : BaseModel
    {
        public Cadastro()
        {
        }

        public virtual Pedido Pedido { get; set; }

        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [StringLength(50, MinimumLength = 5,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [DataMember]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string Telefone { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string Endereco { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string Complemento { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string Municipio { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string UF { get; set; } = "";
        [Required(ErrorMessage = "'{0}' é obrigatório")]
        [DataMember]
        public string CEP { get; set; } = "";

        public void Update(Cadastro novoCadastro)
        {
            Nome = novoCadastro.Nome;
            Email = novoCadastro.Email;
            Telefone = novoCadastro.Telefone;
            Endereco = novoCadastro.Endereco;
            Complemento = novoCadastro.Complemento;
            Bairro = novoCadastro.Bairro;
            Municipio = novoCadastro.Municipio;
            UF = novoCadastro.UF;
            CEP = novoCadastro.CEP;
        }
    }

    public class ItemPedido : BaseModel
    {
        [Required]
        public Pedido Pedido { get; private set; }
        [Required]
        [DataMember]
        public Produto Produto { get; private set; }
        [Required]
        [DataMember]
        public int Quantidade { get; private set; }
        [Required]
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemPedido()
        {

        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AtualizaQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }
    }

    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Cadastro = new Cadastro();
        }

        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        [DataMember]
        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
        [Required]
        [DataMember]
        public virtual Cadastro Cadastro { get; private set; }
    }
}
