using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoService
{
    private readonly Contexto _contexto;

    public TecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Update(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Existe(int id)
    {
        return await _contexto.Tecnicos
            .AnyAsync(e => e.TecnicoId == id);
    }
    public async Task<bool> Eliminar(int id)
    {
        var tecnicos = await _contexto.Tecnicos
            .Where(e => e.TecnicoId == id)
            .ExecuteDeleteAsync();
        return tecnicos > 0;
    }
    public async Task<Tecnicos?> BuscarId(int id)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TecnicoId == id);
    }
    public async Task<Tecnicos?> BuscarNombre(string nombre)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Nombres == nombre);
    }
    public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
}
