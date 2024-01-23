using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Modelos;
public class Periodo: IValidavel
{
    public int Id { get; set; }
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }

    public Periodo(DateTime dataInicial, DateTime dataFinal)
    {
        DataInicial = dataInicial;
        DataFinal = dataFinal;
        Validar();
    }

    public bool Validar()
    {
        if(DataInicial > DataFinal)
        {
            Console.WriteLine("Erro: Data de ida não pode ser maior que a data de volta.");
            return false;
        }

        return true;
    }
}
