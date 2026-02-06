using Horoscopo.Core.Entities;
using Horoscopo.Core.Repository;
using Horoscopo.Core.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Horoscopo.Core.Repository
{
    public class SignoRepository : ISignoRepository
    {
        private readonly AppDbContext _context;
        public SignoRepository(AppDbContext context) => _context = context;

        public async Task<bool> HistorialGuardarAsync(Historial historial)
        {
            try
            {
                await _context.Historiales.AddAsync(historial);
                int filasAfectadas = await _context.SaveChangesAsync();

                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignoRepository > HistorialGuardarAsync > {ex.Message}");
                return false;
            }
        }

        public async Task<List<Historial>> HistorialObtenerAsync()
        {
            try
            {
                return await _context.Historiales
                .OrderByDescending(h => h.FechaConsulta)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignoRepository > HistorialObtenerAsync > {ex.Message}");
                return new List<Historial>();
            }
           
        }
    }
}