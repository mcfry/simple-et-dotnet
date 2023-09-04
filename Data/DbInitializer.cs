using ExerciseTimer.Models;

namespace ExerciseTimer.Data
{
  public static class DbInitializer
  {
    public static void Initialize(ExerciseContext context)
    {
      if (context.Exercises.Any())
      {
        // Delete and reseed
        // context.Exercises.RemoveRange(context.Exercises);
        // context.SaveChanges();

        // DB has been seeded
        return;
      }

      var pushups = new Exercise { Name = "Push ups" };
      var pullups = new Exercise { Name = "Pull ups" };
      var dips = new Exercise { Name = "Dips" };
      var plank = new Exercise { Name = "Plank" };
      var handstand = new Exercise { Name = "Handstand" };

      context.Exercises.Add(pushups);
      context.Exercises.Add(pullups);
      context.Exercises.Add(dips);
      context.Exercises.Add(plank);
      context.Exercises.Add(handstand);
      context.SaveChanges();

            // var pizzas = new Pizza[]
      // {
      //   new Pizza
      //   {
      //     Name = "Meat Lovers",
      //     Sauce = tomatoSauce,
      //     Toppings = new List<Topping>
      //     {
      //       pepperoniTopping,
      //       sausageTopping,
      //       hamTopping,
      //       chickenTopping
      //     }
      //   },
      //   new Pizza
      //   {
      //     Name = "Hawaiian",
      //     Sauce = tomatoSauce,
      //     Toppings = new List<Topping>
      //     {
      //         pineappleTopping,
      //         hamTopping
      //     }
      //   },
      //   new Pizza
      //   {
      //     Name="Alfredo Chicken",
      //     Sauce = alfredoSauce,
      //     Toppings = new List<Topping>
      //     {
      //         chickenTopping
      //     }
      //   }
      // };

      // context.Pizzas.AddRange(pizzas);
    }
  }
}