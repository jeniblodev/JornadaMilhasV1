using JornadaMilhasV1.Dados;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;

var context = new JornadaMilhasContext();
var ofertasDAL = new DAL(context);

while (true)
{
    ExibirMenu();

    Console.WriteLine("Boas vindas ao Jornada Milhas. Escolha uma opção:");
    string opcao = Console.ReadLine()!;

    switch (opcao)
    {
        case "1":
            Console.WriteLine("-- Cadastro de ofertas --");
            Console.WriteLine("Informe a cidade de origem: ");
            string origem = Console.ReadLine();

            Console.WriteLine("Informe a cidade de destino: ");
            string destino = Console.ReadLine();

            Console.WriteLine("Informe a data de ida (DD/MM/AAAA): ");
            DateTime dataIda;
            if (!DateTime.TryParse(Console.ReadLine(), out dataIda))
            {
                Console.WriteLine("Data de ida inválida.");
                return;
            }

            Console.WriteLine("Informe a data de volta (DD/MM/AAAA): ");
            DateTime dataVolta;
            if (!DateTime.TryParse(Console.ReadLine(), out dataVolta))
            {
                Console.WriteLine("Data de volta inválida.");
                return;
            }

            Console.WriteLine("Informe o preço: ");
            double preco;
            if (!double.TryParse(Console.ReadLine(), out preco))
            {
                Console.WriteLine("Formato de preço inválido.");
                return;
            }

            OfertaViagem ofertaCadastrada = new OfertaViagem(new Rota(origem, destino), dataIda, dataVolta, preco);

            Console.WriteLine("\nOferta cadastrada com sucesso.");
            ofertasDAL.AdicionarOfertaViagem(ofertaCadastrada);
            break;
        case "2":
            foreach (var oferta in ofertasDAL.ObterTodasOfertasViagem())
            {
                Console.WriteLine($"Origem: {oferta.Rota}, Destino: {oferta.Rota}, Data de Ida: {oferta.DataIda.ToShortDateString()}, Data de Volta: {oferta.DataVolta.ToShortDateString()}, Preço: {oferta.Preco:C}");
            }
            break;
        case "3":
            Console.WriteLine("Ofertas com maior desconto:");
            return;
        case "4":
            Console.WriteLine("Obrigada por utilizar o Jornada Milhas. Até mais!");
            return;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}

static void ExibirMenu()
{
    Console.WriteLine("-------- Painel Administrativo - Jornada Milhas --------");
    Console.WriteLine("1. Cadastrar Ofertas");
    Console.WriteLine("2. Mostrar Todas as Ofertas");
    Console.WriteLine("3. Exibir maiores descontos");
    Console.WriteLine("4. Sair");
}