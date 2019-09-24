using System;

namespace FoodDeliveryApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\t********* Welcome to BingeBox! **********\n");
            Console.WriteLine("Please Login to continue..\n");
            UserLogin L = new UserLogin(); // check credentials, return user object reference
            var CurrentUser = L.LoginFunction(); // login with reference
            Console.WriteLine($"Logged in successfully as {CurrentUser.username}");
            CurrentUser.ViewPastOrders();// View past orders



            RestaurantList.LoadRestaurant(); // display list of restaurants
            CurrentUser.restaurantChoice = RestaurantList.SelectRestaurant() - 1; // capture restaurant choice
            Console.Clear();
            CurrentUser.OrderFood();// load menu of the selected resaturant to order food
            CurrentUser.MyCart.ViewCart();// view cart


            Console.WriteLine("************** Happy Binging!!! ***************");
            Console.ReadLine();


        }


    }


}



