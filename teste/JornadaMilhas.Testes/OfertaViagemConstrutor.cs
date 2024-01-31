using Bogus;
using JornadaMilhasV1.Gerencidor;
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


    [Fact]
    public void RetornaOfertaNulaQuandoListaEstaVazia()
    {
        //arrange
        // dada uma lista fixa de ofertas com informações diferentes, quando o gerenciador filtrá-las por condições específicas espero que a oferta X seja a primeira
        var lista = new List<OfertaViagem>(); // <= ofertas
        /*
         * oferta 29, destino SP, ativa, preco, desconto <-
         * oferta 30, destino SP, inativa = false, preco, desconto = 12
         * oferta 31, destino RJ, ativa
         * oferta 45, destino SP, inativa
         */
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

        //act
        var ofertaViagem = gerenciador.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.Null(ofertaViagem);
    }

    [Fact]
    public void RetornaOfertaId29QuandoDestinoSaoPaulo()
    {
        //arrange
        // dada uma lista fixa de ofertas com informações diferentes, quando o gerenciador filtrá-las por condições específicas espero que a oferta X seja a primeira
        var fakerPeriodo = new Faker<Periodo>()
            .CustomInstantiator(f => {
                DateTime inicio = f.Date.Soon();
                return new Periodo(inicio, inicio.AddDays(30));
        });
        var rota = new Rota("Sbrubles", "São Paulo");

        var ofertaVencedora = new OfertaViagem(rota, fakerPeriodo.Generate(), 80.0)
        {
            Id = 29,
            Desconto = 40.0,
            Ativa = true
        }; 

        var fakerOferta = new Faker<OfertaViagem>()
            .CustomInstantiator(f => new OfertaViagem(
                rota: rota, 
                periodo: fakerPeriodo.Generate(),
                preco: 100.0 * f.Random.Int(1, 100) )
            )
            .RuleFor(o => o.Desconto, f => 40.0 )  
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Ativa, f => true);


        var lista = fakerOferta.Generate(200);
        lista.Add(ofertaVencedora);
        /*
         * oferta 29, destino SP, ativa, preco, desconto <-
         * oferta 30, destino SP, inativa = false, preco, desconto = 12
         * oferta 31, destino RJ, ativa
         * oferta 45, destino SP, inativa
         */
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

        //act
        var ofertaViagem = gerenciador.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(ofertaViagem);
        Assert.Equal(40.0, ofertaViagem.Preco, 0.00001);
    }

}