----------------------------------------------------
# video 5.3 - Implementando o filtro

* Executamos nosso teste e ele não passou pois não foi implementado o método de recuperar desconto, vamos fazer isso

```
public IEnumerable<OfertaViagem> RecuperaOfertas(Func<OfertaViagem, bool> filtro)
    {
        return ofertaViagem
            .Where(filtro);
    }

    public OfertaViagem? RecuperaMaiorDesconto(Func<OfertaViagem, bool> filtro) => 
        ofertaViagem
            .Where(filtro)
            .OrderBy(o => o.Preco)
            .FirstOrDefault();

```

```
        var ofertaInativa = new OfertaViagem(rota, fakerPeriodo.Generate(), 70.0)
        {
            Desconto = 40.0,
            Ativa = false
        };
```

```
.Where(o => o.Ativa)
```



----------------------------------------------------
# video 5.2 - Gerando valores de teste

* Podemos automatizar esse processo utilizando a biblioteca bogus para gerar os dados fakes que vamos utilizar nos testes

* instalar biblioteca
* preencher os dados no teste

```
[Fact]
    public void RetornaOfertaId29QuandoDestinoSaoPaulo()
    {
        //arrange
        var fakerPeriodo = new Faker<Periodo>()
            //construtor personalizado
            .CustomInstantiator(f => {
                DateTime inicio = f.Date.Soon();
                return new Periodo(inicio, inicio.AddDays(30));
        });
        var rota = new Rota("Sbrubles", "São Paulo");

        var ofertaVencedora = new OfertaViagem(rota, fakerPeriodo.Generate(), 80.0)
        {
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
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

        //act
        var ofertaViagem = gerenciador.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(ofertaViagem);
        Assert.Equal(40.0, ofertaViagem.Preco, 0.00001);
    }
```


----------------------------------------------------
# video 5.1 - Recuperando oferta por desconto

* Já estamos trabalhando com testes automatizados no nosso jornada milhas, mas agora temos uma nova situção pra lidar: precisamos adicionar a funcionalidade de busca por oferta com maior desconto no projeto - que foi solicitado junto com a inclusão do desconto.

* Então, já sabemos o que fazer, vamos criar primeiro o nosso teste com o que precisamos da funcionalidade

* O que precisamos testar? Precisamos buscar em uma lista com várias ofertas, uma oferta específica de acordo com uma condição

então podemos começar testando uma situação em que essa lista de ofertas está vazia.

```
[Fact]
public void RetornaOfertaNulaQuandoListaEstaVazia()
{
    //arrange
    var lista = new List<OfertaViagem>();
    var gerenciador = new GerenciadorDeOfertas(lista);
    Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

    //act
    var ofertaViagem = gerenciador.RecuperaMaiorDesconto(filtro);

    //assert
    Assert.Null(ofertaViagem);
}
```
GERENCIADOR
```
public IEnumerable<OfertaViagem> RecuperaOfertas(Func<OfertaViagem, bool> filtro)
{
    return ofertaViagem
        .Where(filtro);
}

public OfertaViagem? RecuperaMaiorDesconto(Func<OfertaViagem, bool> filtro) =>
    ofertaViagem
        .FirstOrDefault();
```


* beleza, conseguimos verificar então agora com o filtro o cenário de lista vazia, utilizando o tdd, e agora precisamos testar uma situação especifica onde dado uma condição, teremos exatamente a oferta esperada, então vamos escrever o teste baseada na oferta específica de id = 29, destino = são paulo e desconto = 40

```
[Fact]
// id: 29, destino = são paulo, desconto = 40
public void RetornaOfertaId29QuandoDestinoSaoPaulo()
{
    //arrange
    var lista = new List<OfertaViagem>();
    var gerenciador = new GerenciadorDeOfertas(lista);
    Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

    //act
    var ofertaViagem = gerenciador.RecuperaMaiorDesconto(filtro);

    //assert
    Assert.NotNull(ofertaViagem);
    Assert.Equal(40.0, ofertaViagem.Preco, 0.00001);
}
```

* para executar esse teste precisaríamos adicionar dados nessa lista, poderíamos criar uma lista com os dados manualmente, mas será que existe uma maneira de automatizar essa geração de dados para testes?
