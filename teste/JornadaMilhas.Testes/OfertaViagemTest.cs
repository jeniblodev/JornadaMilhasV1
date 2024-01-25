using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Testes;

public class OfertaViagemTest
{
    [Fact]
    public void TestandoOfertaValida()
    {
        //cenário - arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 2, 25), new DateTime(2024, 2, 26));
        double preco = 100.0;

        //ação - act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //resultado - assert
        Assert.True(oferta.EhValido);
    }

    [Fact]
    public void TestandoOfertaComRotaNula()
    {
        //cenário
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 2, 25), new DateTime(2024, 2, 26));
        double preco = 100.0;

        //ação
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //resultado
        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }
}