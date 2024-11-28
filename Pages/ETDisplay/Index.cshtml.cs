using DropDown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDropDown.Services;

namespace RazorDropDown.Pages.ETDisplay
{
    public class IndexModel : PageModel
    {
        private readonly EventTypeService1 _eventTypeService;
        public IndexModel(EventTypeService1 eventTypeService) {
           _eventTypeService = eventTypeService;
        }

        [BindProperty]
        public SelectViewModel VM { get; set; } = new SelectViewModel();


        public async Task<ActionResult> OnGet()
        {
            var listofEventTypes = await  _eventTypeService.GetEventTypesAsync();
            // create and assign a populated SelectViewModel to VM
            VM = new SelectViewModel
            {
                List = SelectViewModel.BuildList(listofEventTypes)
            };

            return Page();
        }
    }
}
