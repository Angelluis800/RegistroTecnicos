using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;

namespace RegistroIncentivos.Services;

public class IncentivoService
{
    private readonly Contexto _contexto;

    public IncentivoService(Contexto contexto)
    {
        _contexto = contexto;
    }
    public async Task<bool> Guardar(Incentivos incentivo)
    {
        if (!await Existe(incentivo.IncentivoId))
            return await Insertar(incentivo);
        else
            return await Modificar(incentivo);
    }
    private async Task<bool> Insertar(Incentivos incentivo)
    {
        _contexto.Incentivos.Add(incentivo);
        return await _contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Incentivos incentivo)
    {
        _contexto.Incentivos.Update(incentivo);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(incentivo).State = EntityState.Detached; //Limpia y permite la modificacion
        return modificado;
    }
    private async Task<bool> Existe(int id)
    {
        return await _contexto.Incentivos
            .AnyAsync(e => e.IncentivoId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        var Incentivos = await _contexto.Incentivos
            .Where(e => e.IncentivoId == id)
            .ExecuteDeleteAsync();
        return Incentivos > 0;
    }
    public async Task<Incentivos?> BuscarId(int id)
    {
        return await _contexto.Incentivos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IncentivoId == id);
    }

    public async Task<bool> Existe(int id, string? descripcion)
    {
        return await _contexto.Incentivos
            .AnyAsync(t => t.IncentivoId != id && t.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }

    public async Task<List<Incentivos>> Listar(Expression<Func<Incentivos, bool>> criterio)
    {
        return await _contexto.Incentivos
            .Include(e => e.TiposTecnicos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
