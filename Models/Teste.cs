using System;

namespace TesteAPI.Models
{
    public class Teste
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public bool Ativo { get; set; }
        public int OpcaoId { get; set; }
        public Opcao Opcao { get; set; }
    }
}