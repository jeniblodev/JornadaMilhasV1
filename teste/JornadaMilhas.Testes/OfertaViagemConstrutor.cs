using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Testes;

public class OfertaViagemConstrutor
{
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
    /*[InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1)]
    [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0)]
    [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500)]*/
    public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
    {
        //arrange
        Rota rota = new Rota(origem, destino);
        Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Equal(validacao, oferta.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoRotaNula()
    {
        //arrange
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 2, 25), new DateTime(2024, 2, 26));
        double preco = 100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoPeriodoNulo()
    {
        //arrange
        Rota rota = new Rota("origem", "destino");
        Periodo periodo = null;
        double preco = 100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Fact]
    //Construtor_PrecoNegativo_RetornaMensagemDeErroDePrecoInvalido
    public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenosQueZero()
    {
        //arrange
        Rota rota = new Rota("Origem1", "Destino1");
        Periodo periodo = new Periodo(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));
        double preco = -250.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }

    [Fact]
    public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
    {
        //arrange
        Rota rota = new Rota("OrigemA", "DestinoB");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.00;
        double desconto = 20.00;
        double precoComDesconto = precoOriginal - desconto;
        OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, precoOriginal);

        //act
        ofertaViagem.Desconto = desconto;

        //assert
        Assert.Equal(precoComDesconto, ofertaViagem.Preco);
        // Assert.Equal(desconto, ofertaViagem.Desconto); // lógico
    }

    [Fact]
    public void RetornaDescontoMaximoQuandoValorDescontoMaiorQuePreco()
    {
        //arrange
        Rota rota = new Rota("OrigemA", "DestinoB");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.00;
        double desconto = 120.00;
        double precoComDesconto = 30.00;
        OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, precoOriginal);

        //act
        ofertaViagem.Desconto = desconto;

        //assert
        Assert.Equal(precoComDesconto, ofertaViagem.Preco, 0.001);
        // Assert.Equal(desconto, ofertaViagem.Desconto); // lógico
    }

    [Fact]
    public void RetornaTresErrosDeValidacaoQuandoPeriodoRotaEPrecoSaoInvalidos()
    {
        //arrange
        int qtdeEsperada = 3;
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 06, 01), new DateTime(2024, 05, 10));
        double precoOriginal = -100.00;

        //act
        OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, precoOriginal);

        //assert
        Assert.Equal(qtdeEsperada, ofertaViagem.Erros.Count());
    }


}