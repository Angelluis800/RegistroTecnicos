using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TipoTecnicoService
{
    private readonly Contexto _contexto;

    public TipoTecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }
    public async Task<bool> Guardar(TipoTecnicos tipo)
    {
        if (!await Existe(tipo.TipoId))
            return await Insertar(tipo);
        else
            return await Modificar(tipo);
    }
    private async Task<bool> Insertar(TipoTecnicos tipo)
    {
        _contexto.TiposTecnicos.Add(tipo);
        return await _contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(TipoTecnicos tipo)
    {
        _contexto.TiposTecnicos.Update(tipo);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(tipo).State = EntityState.Detached; //Limpia y permite la modificacion
        return modificado;
    }
    private async Task<bool> Existe(int id)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(e => e.TipoId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        var tecnicos = await _contexto.TiposTecnicos
            .Where(e => e.TipoId == id)
            .ExecuteDeleteAsync();
        return tecnicos > 0;
    }
    public async Task<TipoTecnicos?> BuscarId(int id)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TipoId == id);
    }

    public async Task<bool> Existe(int id, string? descripcion)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.TipoId != id && t.Descripcion.ToLower().Equals(descripcion));
    }

    public async Task<List<TipoTecnicos>> Listar(Expression<Func<TipoTecnicos, bool>> criterio)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
