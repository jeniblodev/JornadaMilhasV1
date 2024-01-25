using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Testes;

public class OfertaViagemTest
{
    [Fact]
    public void TestandoOfertaValida()
    {
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 2, 25), new DateTime(2024, 2, 26));
        double preco = 100.0;

        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        Assert.True(oferta.EhValido);
    }
}