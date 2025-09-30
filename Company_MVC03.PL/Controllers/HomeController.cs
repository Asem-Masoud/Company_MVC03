using System.Diagnostics;
using System.Text;
using Company_MVC03.PL.Models;
using Company_MVC03.PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company_MVC03.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;
        private readonly ITransientServices transientServices01;
        private readonly ITransientServices transientServices02;
        private readonly ISingletonServices singletonServices01;
        private readonly ISingletonServices singletonServices02;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransientServices transientServices01,
            ITransientServices transientServices02,
            ISingletonServices singletonServices01,
            ISingletonServices singletonServices02
            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.transientServices01 = transientServices01;
            this.transientServices02 = transientServices02;
            this.singletonServices01 = singletonServices01;
            this.singletonServices02 = singletonServices02;
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedService01 :: {scopedService01.GetGuid()}\n");
            builder.Append($"scopedService02 :: {scopedService02.GetGuid()}\n\n");
            builder.Append($"transientServices01 :: {transientServices01.GetGuid()}\n");
            builder.Append($"transientServices02 :: {transientServices02.GetGuid()}\n\n");
            builder.Append($"singletonServices01 :: {singletonServices01.GetGuid()}\n");
            builder.Append($"singletonServices02 :: {singletonServices02.GetGuid()}\n\n");

            return builder.ToString();
        }

        public IActionResult Index()
        {
            return View();
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
