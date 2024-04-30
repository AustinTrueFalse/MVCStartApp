using Microsoft.EntityFrameworkCore;

namespace MVCStartApp.Models.Db
{
    public class RequestRepository : IRequestRepository
    {
        // ссылка на контекст
        private readonly RequestContext _context;

        // Метод-конструктор для инициализации
        public RequestRepository(RequestContext context)
        {
            _context = context;
        }

        public async Task Log(Request request)
        {

            // Добавление лога
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            // Получим всех активных пользователей
            return await _context.Requests.ToArrayAsync();
        }
    }
}
