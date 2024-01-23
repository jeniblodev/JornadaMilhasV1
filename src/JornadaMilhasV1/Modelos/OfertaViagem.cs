using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Modelos;
public class OfertaViagem
{
    public int Id { get; set; }
    public Rota Rota { get; set; }
    public DateTime DataIda { get; set; }
    public DateTime DataVolta { get; set; }
    public double Preco { get; set; }
    private bool valido = false;

    public OfertaViagem(Rota rota, DateTime dataIda, DateTime dataVolta, double preco)
    {
        Rota = rota;
        DataIda = dataIda;
        DataVolta = dataVolta;
        Preco = preco;
        this.EhValido();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {DataIda.ToShortDateString()}, Data de Volta: {DataVolta.ToShortDateString()}, Preço: {Preco:C}";
    }

    public bool EhValido()
    {
        if (this.Rota == null)
        {
            throw new FormatException("A rota não pode ser nula.");
        }
        else if ((this.Rota.Origem is null) || this.Rota.Origem.Equals(string.Empty))
        {
            throw new FormatException("A rota não pode possuir uma origem nula ou vazia.");
        }
        else if ((this.Rota.Destino is null) || this.Rota.Destino.Equals(string.Empty))
        {
            throw new FormatException("A rota não pode possuir um destino nulo ou vazio.");
        }
        else if (this.DataIda > this.DataVolta)
        {
            throw new FormatException("Data de ida não pode ser maior que a data de volta.");
        }
        else if (this.Preco <= 0)
        {
            throw new FormatException("Preço da oferta não pode ser menor que zero.");
        }
        else
            valido = true;

        return valido;
    }
}
