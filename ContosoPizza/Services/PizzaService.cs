using ContosoPizza.Data;
using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly PizzaContext _context = default!;

        public PizzaService(PizzaContext context) 
        {
            _context = context;
        }
        
        public IList<Pizza> GetPizzas()
        {
            if(_context.Pizzas != null)
            {
                return _context.Pizzas.ToList();
            }
            return new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            if (_context.Pizzas != null)
            {
                _context.Pizzas.Add(pizza);
                _context.SaveChanges();
            }
        }

        public void DeletePizza(int id)
        {
            if (_context.Pizzas != null)
            {
                var pizza = _context.Pizzas.Find(id);
                if (pizza != null)
                {
                    _context.Pizzas.Remove(pizza);
                    _context.SaveChanges();
                }
            }            
        } 

        public Pizza? GetPizzaById(int id)
        {
              if (_context.Pizzas != null)
                {
                    return _context.Pizzas.Find(id);
                }
                return null;
        }
        public void UpdatePizza(Pizza updatedPizza)
        {
            if(_context.Pizzas !=null){
            var existingPizza = _context.Pizzas.Find(updatedPizza.Id);
            if(existingPizza!=null)
                {
                    existingPizza.Name = updatedPizza.Name;
                    existingPizza.Size = updatedPizza.Size;
                    existingPizza.Price = updatedPizza.Price;
                    existingPizza.IsGlutenFree = updatedPizza.IsGlutenFree;
                }
                    _context.SaveChanges();

            }
        }


    }
}
