using JornadaMilhasV1.Validador;

namespace JornadaMilhasV1.Modelos;

public class OfertaViagem: Valida
{
    public const double DESCONTO_MAXIMO = 0.7;
    public int Id { get; set; }
    public Rota Rota { get; set; }
    public Periodo Periodo { get; set; }
    public double Preco { get; set; }
    private double desconto;
    public double Desconto
    {
        get { return desconto; }
        set
        {
            desconto = value;
            if (desconto >= Preco)
            {
                Preco *= (1 - DESCONTO_MAXIMO);
                /*Preco *= 0.3;*/
            } else
            {
                Preco -= desconto;
            }
            
        }
    }

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

    protected override void Validar()
    {
        if (Periodo != null && !Periodo.EhValido)
        {
            Erros.RegistrarErro(Periodo.Erros.Sumario);
        } 
        
         if (Rota == null || Periodo == null)
        {
            Erros.RegistrarErro("A oferta de viagem não possui rota ou período válidos.");
        } 
        
        if (Preco <= 0)
        {
            Erros.RegistrarErro("O preço da oferta de viagem deve ser maior que zero.");
        }
    }
}
