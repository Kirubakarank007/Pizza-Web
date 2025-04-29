using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get;set; } = default!;
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            NewPizza = new Pizza();
            PizzaList = _service.GetPizzas();
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost() called");
            if (!ModelState.IsValid || NewPizza == null)
            {
                PizzaList = _service.GetPizzas();
                return Page();
            }

            _service.AddPizza(NewPizza);
             return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostEdit(int id){
            var pizza = _service.GetPizzaById(id);
                if (pizza == null)
                {
                    return NotFound();
                }
                NewPizza = pizza; // Fill the form fields with this pizza data
                PizzaList = _service.GetPizzas(); // Reload the list

                return Page(); 
        }

      public IActionResult OnPostSave()
        {
             Console.WriteLine("🔄 OnPostSave (Update) called");
            if (!ModelState.IsValid || NewPizza == null)
            {
                PizzaList = _service.GetPizzas(); // Keep list populated on error
                return Page();
            }

            if (NewPizza.Id == 0)
            {
                _service.AddPizza(NewPizza); // CREATE
            }
            else
            {
                _service.UpdatePizza(NewPizza); // UPDATE
            }

            return RedirectToPage(); // Refresh with updated list
        }

    }
}
