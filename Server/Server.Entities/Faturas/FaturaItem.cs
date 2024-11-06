using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Server.Entities
{
    public class FaturaItem
    {
        public int FaturaItemId { get; set; }

        public int FaturaId { get; set; }

        [Range(10, int.MaxValue, ErrorMessage = "A ordem deve ser múltiplo de 10.")]
        public int Ordem { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public double Valor { get; set; }

        [Required]
        public string Descricao { get; set; }

        public virtual Fatura Fatura { get; set; }

        public FaturaItem()
        {
        }

        public string ValidarFatura()
        {
            StringBuilder Result = new("");

            if (this.Ordem % 10 != 0)
                Result.Append("A ordem deve ser multiplo de 10;");

            if (this.Descricao is null)
                Result.Append("Descrição obrigatoria!;");

            if (this.Descricao is { Length: > 20 })
                Result.Append("Descrição deve ser inferior á 20 caracteres;");


            return Result.ToString();
        }

    }
}
