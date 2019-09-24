using System;
using System.Collections.Generic;


namespace FoodDeliveryApp

{ 
    public static class RestaurantList
    {
        public static List<Restaurant> MyRestaurants = new List<Restaurant>();
       

        public static void LoadRestaurant()
        {
            Restaurant A2B = new Restaurant("A2B", "South Indian", "North Indian");
            Restaurant Sangeetha = new Restaurant("Sangeetha", "South Indian", "North Indian", "Chinese");
            Restaurant KimLing = new Restaurant("Kim Ling","Chinese");
        }
        public static int SelectRestaurant()
        {
            bool flag = false;
            int choice = 0;
            do
            {


                Console.WriteLine("\nPlease choose a restaurant from the following list:");

                foreach (var res in MyRestaurants)
                {
                    Console.WriteLine($"{MyRestaurants.IndexOf(res) + 1}. {res.restaurantName}");
                    // Console.WriteLine("Test");

                }

                if (int.TryParse(Console.ReadLine(), out int ch))
                {
                    if (ch < 0 || ch > MyRestaurants.Count)
                    {
                        Console.WriteLine("Invalid entry!");
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                        choice = ch;

                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number!\n");
                    flag = false;
                }
                
            } while (flag == false);

            return choice;



        }
    }


    public class Restaurant
    {
        public string restaurantName { get; set; }
        public List<Menu> restaurantItems = new List<Menu>();
    


        public Restaurant(string restaurantName, params string[] cuisine)
        {
            this.restaurantName = restaurantName;
            RestaurantList.MyRestaurants.Add(this);
           
            foreach (var c in cuisine)
            {
                switch (c)
                {
                    case "South Indian": restaurantItems.Add(new SouthIndianCuisine()); break;
                    case "North Indian": restaurantItems.Add(new NorthIndianCuisine()); break;
                    case "Chinese": restaurantItems.Add(new ChineseCuisine()); break;

                }
            }

        }


    }
}



