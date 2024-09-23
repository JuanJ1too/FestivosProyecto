using Festivos.Domain.Repositorio;
namespace Festivos.Application.Servicios
{
    public class FestivosService
    {
        private readonly IFestivoRPY _festivoRepository;
        public FestivosService(IFestivoRPY festivoRepository)
        {
            _festivoRepository = festivoRepository;
        }
        public async Task<bool> esFestivo(DateTime date)
        {
            var result = _festivoRepository.EsFestivo(date);
            return await result;
        }
    }
}