using ClassifiedsBlazor.Entities;
using ClassifiedsBlazor.Services;
using ClassifiedsBlazor.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassifiedsBlazor.Pages
{
    public class DetailModel : PageModel
    {
        public Advert? Advert { set; get; }
        private IAdvertService _advertService;

        public DetailModel(IAdvertService advertService)
        {
            _advertService = advertService;
        }
  
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Advert = await _advertService.FindById(id);
            return Page();
        }

    }
}
