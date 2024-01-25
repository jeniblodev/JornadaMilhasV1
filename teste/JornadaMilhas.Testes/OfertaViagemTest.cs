using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Testes;

public class OfertaViagemTest
{
    [Fact]
    public void TestandoOfertaValida()
    {
        //arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 2, 25), new DateTime(2024, 2, 26));
        double preco = 100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.True(oferta.EhValido);
    }

    [Fact]
    public void TestandoOfertaComRotaNula()
    {
        //arrange
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 2, 25), new DateTime(2024, 2, 26));
        double preco = 100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Fact]
    public void TestandoPrecoNegativo()
    {
        //arrange
        Rota rota = new Rota("Origem1", "Destino1");
        Periodo periodo = new Periodo(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));
        double preco = -250.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("O pre�o da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }


}