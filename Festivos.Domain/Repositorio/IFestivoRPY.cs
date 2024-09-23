namespace Festivos.Domain.Repositorio
{
    public interface IFestivoRPY
    {
        public Task<bool> EsFestivo(DateTime date);
    }
}
