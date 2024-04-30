using Microsoft.AspNetCore.Mvc;
using MVCStartApp.Models;
using MVCStartApp.Models.Db;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace MVCStartApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _repoBlog;
        private readonly IRequestRepository _repoRequest;
        private readonly ILogger<HomeController> _logger;
        private readonly HttpContext context;

        public HomeController(ILogger<HomeController> logger, IBlogRepository repoBlog, IRequestRepository repoRequest)
        {
            _logger = logger;
            _repoBlog = repoBlog;
            _repoRequest = repoRequest;
        }



        public async Task<IActionResult> Index()
        {
           
            // Добавим создание нового лога
            var newRequest = new Request()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Url = Request.Host.Value
            };

            // Добавим в базу
            await _repoRequest.Log(newRequest);

            // Выведем результат
            Console.WriteLine($"Log with id {newRequest.Id}, was successfully added on {newRequest.Date}");

            return View();

            
        }



        public async Task<IActionResult> Authors()
        {
            var authors = await _repoBlog.GetUsers();
            return View(authors);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}