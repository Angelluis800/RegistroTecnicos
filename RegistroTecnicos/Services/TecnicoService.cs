using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using System.Linq;

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
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(tecnico).State = EntityState.Detached; //Limpia y permite la modificacion
        return modificado;
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

    public async Task<bool> EliminarIncentivoDeTecnico(int tecnicoId, int incentivoId)
    {
        var tecnico = await _contexto.Tecnicos.FindAsync(tecnicoId);
        if (tecnico == null)
        {
            return false;
        }

        var incentivo = await _contexto.Incentivos.FirstOrDefaultAsync(i => i.IncentivoId == incentivoId && i.TecnicoId == tecnicoId);

        if (incentivo == null)
        {
            return false;
        }

        _contexto.Incentivos.Remove(incentivo);
        return await _contexto.SaveChangesAsync() > 0;
    }


    public async Task<Tecnicos?> BuscarId(int id)
    {
        return await _contexto.Tecnicos
            .Include(e => e.TiposTecnicos)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TecnicoId == id);
    }

    public async Task<bool> Existe(int id, string? nombre)
    {
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != id && t.Nombres.ToLower().Equals(nombre.ToLower()));
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await _contexto.Tecnicos
            .Include(e => e.TiposTecnicos)
            .Include(e => e.Incentivos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
