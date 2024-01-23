using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Dados;
internal class DAL
{
    private readonly JornadaMilhasContext context;

    public DAL(JornadaMilhasContext context)
    {
        context = context;
    }

    public List<OfertaViagem> ObterTodasOfertasViagem()
    {
        return context.OfertasViagem.ToList();
    }

    public OfertaViagem ObterOfertaViagemPorId(int id)
    {
        return context.OfertasViagem.Find(id);
    }

    public void AdicionarOfertaViagem(OfertaViagem oferta)
    {
        context.OfertasViagem.Add(oferta);
        context.SaveChanges();
    }

    public void AtualizarOfertaViagem(OfertaViagem oferta)
    {
        context.OfertasViagem.Update(oferta);
        context.SaveChanges();
    }

    public void RemoverOfertaViagem(OfertaViagem oferta)
    {
        var ofertaViagem = context.OfertasViagem.Find(oferta.Id);
        if (ofertaViagem != null)
        {
            context.OfertasViagem.Remove(ofertaViagem);
            context.SaveChanges();
        }
    }
}
