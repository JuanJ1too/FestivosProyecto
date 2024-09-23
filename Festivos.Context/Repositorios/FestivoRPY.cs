using Festivos.API.Models;
using Festivos.Domain.Entidades;
using Festivos.Domain.Enum;
using Festivos.Domain.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Festivos.Context.MDB;

namespace Festivos.Context.Repositorios
{
    public class FestivoRPY : IFestivoRPY
    {
        private readonly FestivosContext _ctx;
        private Festivo[] _list;

        public FestivoRPY(FestivosContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> EsFestivo(DateTime fecha)
        {
            // Cargar la lista de festivos de la base de datos
            _list = await _ctx.Festivos.ToArrayAsync();
            DateTime fechaPascua;
            ProximoLClass? fechaResult;

            // Recorrer todos los festivos para verificar si coinciden con la fecha proporcionada
            foreach (var diafestivo in _list)
            {
                switch ((FestivosEnum)diafestivo.IdTipo)
                {
                    case FestivosEnum.Fijo:
                        // Festivo con fecha fija, no requiere procesamiento adicional
                        break;
                    case FestivosEnum.Puente:
                        // Calcular el próximo lunes para los festivos tipo "puente"
                        fechaResult = ProcedimientoDB.ProximoL(_ctx, fecha, diafestivo);
                        if (fechaResult != null)
                        {
                            diafestivo.Dia = fechaResult.ProximoLFecha.Day;
                            diafestivo.Mes = fechaResult.ProximoLFecha.Month;
                        }
                        break;
                    case FestivosEnum.DomingoPascua:
                        // Obtener la fecha del Domingo de Pascua
                        fechaPascua = ProcedimientoDB.PascuaDomingo(_ctx, fecha, diafestivo);
                        diafestivo.Dia = fechaPascua.Day;
                        diafestivo.Mes = fechaPascua.Month;
                        break;
                    case FestivosEnum.PuenteDomingoPascua:
                        // Calcular el próximo lunes después del Domingo de Pascua
                        fechaPascua = ProcedimientoDB.PascuaDomingo(_ctx, fecha, diafestivo);
                        fechaResult = ProcedimientoDB.ProximoLProcedimiento(_ctx, fecha, fechaPascua);
                        if (fechaResult != null)
                        {
                            diafestivo.Dia = fechaResult.ProximoLFecha.Day;
                            diafestivo.Mes = fechaResult.ProximoLFecha.Month;
                        }
                        break;
                }

                // Verificar si la fecha proporcionada coincide con algún festivo
                if (fecha.Day == diafestivo.Dia && fecha.Month == diafestivo.Mes)
                {
                    return true;
                }
            }

            // Si no se encontró ninguna coincidencia, retornar falso
            return false;
        }
    }
}
