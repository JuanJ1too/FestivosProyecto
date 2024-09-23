using Festivos.API.Models;
using Festivos.Domain.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Festivos.Context.MDB
{
    public static class ProcedimientoDB
    {
        public static ProximoLClass? ProximoL(FestivosContext ctx,DateTime date,Festivo festivo)
        {
            var festivoFecha = new DateTime(date.Year,festivo.Mes,festivo.Dia);
             return
             ctx.Database
            .SqlQueryRaw<ProximoLClass>("EXEC ObtenerProximoLunes @p0", new SqlParameter("@p0", festivoFecha))
            .AsEnumerable()
            .FirstOrDefault();
        }

        public static ProximoLClass? ProximoLProcedimiento(FestivosContext ctx, DateTime date, DateTime festivo)
        {
            var festivoFecha = new DateTime(date.Year, festivo.Month, festivo.Day);
            return
            ctx.Database
           .SqlQueryRaw<ProximoLClass>("EXEC ObtenerProximoLunes @p0", new SqlParameter("@p0", festivoFecha))
           .AsEnumerable()
           .FirstOrDefault();
        }

        public static DateTime PascuaDomingo(FestivosContext ctx, DateTime date,Festivo festivo)
        {
            SqlParameter? outputParam;
            SqlParameter? yearParam;
            DateTime fechaPascua;
            outputParam = new SqlParameter("@PASCUADATE", System.Data.SqlDbType.Date)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            yearParam = new SqlParameter("@Year", date.Year);
            ctx.Database.ExecuteSqlRaw("EXEC SundayPascua @Year, @PASCUADATE OUTPUT",
                             yearParam, outputParam);
            fechaPascua = (DateTime)outputParam.Value;
            fechaPascua = fechaPascua.AddDays(festivo.DiasPascua);
            return fechaPascua;
        }
    }
}
