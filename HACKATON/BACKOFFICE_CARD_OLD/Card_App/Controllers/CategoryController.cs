using BCP.CROSS.MODELS.Category;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Card_App.Contratcts;
using Card_App.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Card_App.Controllers
{

    public class CategoryController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILoggerManager _logger;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private readonly IManagerSecrypt _secrypt;
        private readonly ICategoryService _categoryService;
        private readonly string PublicToken;
        private readonly string AppUserId;
        private readonly string Channel;

        public CategoryController(IAuthService authService, ILoggerManager logger, IOptions<SecryptOptions> securitySettigns, IManagerSecrypt secrypt, ICategoryService categoryService)
        {
            _logger = logger;
            _authService = authService;
            _securitySettigns = securitySettigns;
            _secrypt = secrypt;
            _categoryService = categoryService;
            PublicToken = _secrypt.Desencriptar(_securitySettigns.Value.PublicToken);
            AppUserId = _securitySettigns.Value.AppUserId;
        }
        public async Task<IActionResult> Category()
        {
            ListAllCategoryRequest request = new ListAllCategoryRequest();
            request.publicToken = PublicToken;
            request.appUserId = AppUserId;
            CategoryViewModel CategoryViewModel = new();

            var CategoryResponse = await _categoryService.ObtieneListCategoryAsync(request);

            CategoryViewModel.CategoryResponse = CategoryResponse;

            return View(CategoryViewModel);
        }

    }
   
}
