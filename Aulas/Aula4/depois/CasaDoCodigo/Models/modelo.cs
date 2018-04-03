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

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 5,
            ErrorMessage = "Nome deve ter entre 5 e 50 caracteres")]
        [DataMember]
        public string Nome { get; set; } = "";
        [Required]
        [DataMember]
        public string Email { get; set; } = "";
        [Required]
        [DataMember]
        public string Telefone { get; set; } = "";
        [Required]
        [DataMember]
        public string Endereco { get; set; } = "";
        [Required]
        [DataMember]
        public string Complemento { get; set; } = "";
        [Required]
        [DataMember]
        public string Bairro { get; set; } = "";
        [Required]
        [DataMember]
        public string Municipio { get; set; } = "";
        [Required]
        [DataMember]
        public string UF { get; set; } = "";
        [Required]
        [DataMember]
        public string CEP { get; set; } = "";

        public void Update(Cadastro novoCadastro)
        {
            Nome = this.Nome;
            Email = this.Email;
            Telefone = this.Telefone;
            Endereco = this.Endereco;
            Complemento = this.Complemento;
            Bairro = this.Bairro;
            Municipio = this.Municipio;
            UF = this.UF;
            CEP = this.CEP;
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
