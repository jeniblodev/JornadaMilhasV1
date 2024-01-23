using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Modelos;
public class OfertaViagem: IValidavel
{
    public int Id { get; set; }
    public Rota Rota { get; set; }
    public Periodo Periodo { get; set; }
    public double Preco { get; set; }

    public OfertaViagem(Rota rota, Periodo periodo, double preco)
    {
        Rota = rota;
        Periodo = periodo;
        Preco = preco;
        Validar();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial.ToShortDateString()}, Data de Volta: {Periodo.DataFinal.ToShortDateString()}, Preço: {Preco:C}";
    }

    public bool Validar()
    {
        if (Rota == null || Periodo == null)
        {
            Console.WriteLine("A oferta de viagem não possui rota ou período válidos.");
            return false;
        } 
        else if (Preco <= 0)
        {
            Console.WriteLine("O preço da oferta de viagem deve ser maior que zero.");
            return false;
        }

        return true;
    }
}
