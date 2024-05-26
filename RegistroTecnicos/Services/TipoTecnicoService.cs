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
    public async Task<bool> Guardar(TiposTecnicos tipo)
    {
        if (!await Existe(tipo.TipoId))
            return await Insertar(tipo);
        else
            return await Modificar(tipo);
    }
    private async Task<bool> Insertar(TiposTecnicos tipo)
    {
        _contexto.TiposTecnicos.Add(tipo);
        return await _contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(TiposTecnicos tipo)
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
    public async Task<TiposTecnicos?> BuscarId(int id)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TipoId == id);
    }

    public async Task<bool> Existe(int id, string? descripcion)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.TipoId != id && t.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _contexto.TiposTecnicos
            .Include(t => t.Incentivos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<Dictionary<int, decimal>> CalcularMontosTotalesIncentivosPorTipo()
    {
        var tipos = await _contexto.TiposTecnicos.Include(t => t.Incentivos).ToListAsync();
        var montosTotalesPorTipo = new Dictionary<int, decimal>();

        foreach (var tipo in tipos)
        {
            decimal montoTotal = tipo.Incentivos.Sum(i => i.Monto);
            montosTotalesPorTipo.Add(tipo.TipoId, montoTotal);
        }

        return montosTotalesPorTipo;
    }

    private async Task<decimal> CalcularMontoTotalIncentivos(int tipoId)
    {
        var tipo = await _contexto.TiposTecnicos.FindAsync(tipoId);
        if (tipo == null)
        {
            return 0;
        }

        decimal montoTotal = 0;

        // Suma el monto de todos los incentivos relacionados con este tipo de técnico
        foreach (var incentivo in tipo.Incentivos)
        {
            montoTotal += incentivo.Monto;
        }

        return montoTotal;
    }
}
