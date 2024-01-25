using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Testes;

public class OfetaViagemTest
{
    [Fact]
    public void OfertaViagemConstrutorDeveCriarOferta()
    {
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 1, 1), new DateTime(2024, 1, 5));
        double preco = 100.0;

        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        Assert.Equal(rota, oferta.Rota);
        Assert.Equal(periodo, oferta.Periodo);
        Assert.Equal(preco, oferta.Preco);
    }
}