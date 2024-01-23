using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Modelos;
public class Rota
{
    public string Origem { get; set; }
    public string Destino { get; set; }

    public Rota(string origem, string destino)
    {
        Origem = origem;
        Destino = destino;
    }
}
