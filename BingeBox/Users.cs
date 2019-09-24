using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;


namespace FoodDeliveryApp
{
    public class ActiveUser
    {

        public string username { get; set; }
        string password { get; set; }

        public int restaurantChoice;
        public Cart MyCart;
        List<string> items = new List<string>();
        List<int> quantity = new List<int>();
        List<float> price = new List<float>();
        public ActiveUser(string username, string password)
        {
            this.username = username;
            this.password = password;

        }

        public void ViewPastOrders()
        {


        check: Console.WriteLine("Would you like to view past orders?(y/n)");

            char.TryParse(Console.ReadLine(), out char c);
            if (c == 'y' || c == 'Y')
            {
                try
                {
                    Process.Start("notepad.exe", "AllOrders" + username + ".txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if(c=='n'||c=='N')
            {
                return;
               
            }
            else 
            {
                
                Console.WriteLine("Please enter only Y/y/N/n");

                goto check;
            }



        }
        public void OrderFood()
        {

            char ch;
            string choice;
            var selectedRestaurant = RestaurantList.MyRestaurants.ElementAt(restaurantChoice);


            Console.WriteLine($"\tName:{selectedRestaurant.restaurantName}\n\tHere is our Menu...\n");
            Console.WriteLine("\t{0,15} {1,-45} {1,5}\n", "Item #", "Item", "Price");

            foreach (var item in selectedRestaurant.restaurantItems)
            {
                Console.WriteLine("\t" + item.cuisine + "\n");
                foreach (var fp in item.FoodItem)
                {
                    Console.WriteLine("\t{0,1}{1,-15}{2,-45}{3,5}\n", fp.sno, ".", fp.itemName, fp.ItemPrice);

                }

            }

            bool f = false;


        menuDisplay: do
            {
                f = false;
                Console.WriteLine("\n Enter item number \n");
                choice = Console.ReadLine();

                foreach (var item in selectedRestaurant.restaurantItems)
                {

                    foreach (var fp in item.FoodItem)
                    {

                        if (string.Equals(choice, fp.sno, StringComparison.OrdinalIgnoreCase))
                        {
                            this.items.Add(fp.itemName);
                        qCheck:
                            {
                                Console.WriteLine("\n Enter quantity \n");
                                int.TryParse(Console.ReadLine(), out int qty);

                                if (qty >= 1 && qty < 10)
                                {
                                    this.quantity.Add(qty);
                                }
                                else if (qty > 10)
                                {
                                    Console.WriteLine("\nThats too much of food! Please order less than 10 units!");

                                    goto qCheck;
                                }
                                else if (qty < 1)
                                {
                                    Console.WriteLine("\n This is not a quantitative value! Quantity has to be at least 1! ");

                                    goto qCheck;

                                }

                            }
                            this.price.Add(fp.ItemPrice);
                            f = true;
                            break;


                        }

                    }

                }

                if (f == false)
                {
                    Console.WriteLine("Invalid Item! Please enter item # from the above menu.");

                    goto menuDisplay;
                }

            check:
                {
                    Console.WriteLine("Want to eat more? (y/n)");
                    // Console.WriteLine("\n {0}", f);
                    char.TryParse(Console.ReadLine(), out char c);
                    if (c == 'y' || c == 'Y' || c == 'n' || c == 'N')
                    {
                        ch = c;
                    }
                    else
                    {
                        Console.WriteLine("Please enter only Y/y/N/n");
                        ch = ' ';
                        goto check;
                    }
                }

            } while (ch == 'y' || ch == 'Y');

            MyCart = new Cart(selectedRestaurant, items, quantity, price, username);
            Console.Clear();

        }

    }

    public class Cart
    {
        public string userName;
        public string restaurantName;
        List<string> items = new List<string>();
        List<int> quantity = new List<int>();
        List<float> price = new List<float>();
        float total = 0, afterDiscount = 0;


        public DateTime orderTime;
        Restaurant r;

        public Cart(Restaurant selectedRestaurant, List<string> items, List<int> quantity, List<float> price, String userName)
        {
            r = selectedRestaurant;
            this.userName = userName;
            this.restaurantName = selectedRestaurant.restaurantName;
            this.orderTime = DateTime.Now;
            this.items = items;
            this.quantity = quantity;
            this.price = price;


        }
        public void ViewCart()
        {
            if (items.Count > 0)
            {
                Console.WriteLine($"Order ID : {orderTime}\n");
                Console.WriteLine($"Restaurant : {restaurantName}");
                Console.WriteLine("\t{0,5}.{1,-40}{2,5} {3,5} {4,5}\n", "S.no", "Item name", "Qty", "Price", "Total");
                Console.WriteLine("\t---------------------------------------------------------------------\n\n");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine("\t{0,5}. {1,-40}{2,5} {3,5} {4,5}\n", i + 1, items[i], quantity[i], price[i], (price[i] * quantity[i]));


                }
                total = 0;
                for (int i = 0; i < items.Count; i++)
                    total += (price[i] * quantity[i]);
                Console.WriteLine("\t_____________________________________________________________________\n");
                Console.WriteLine($"\t\tGrand Total = Rs. {total}");
                Console.WriteLine("\t_____________________________________________________________________\n");
                PlaceOrder();
            }
            else
            {
                Console.WriteLine("Cart is empty! Take a break, add some food! Press enter to continue..");
                Console.ReadLine();
                AddItem();
            }
          

        }

        public void PlaceOrder()
        {
            bool f = false;
            do
            {

                Console.WriteLine("\n Confirm order? \n 1. Yes 2.No, Edit order");
                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        {

                            ApplyCoupon();
                            break;
                        }
                    case 2:
                        {
                            if(items.Count>0)
                            EditOrder();
                            else Console.WriteLine("Cart is empty!");
                            break;
                        }


                    default:
                        {
                            Console.WriteLine("Please choose among the given options!");
                            f = true;
                            break;

                        }
                }
            } while (f == true);

        }


        void EditOrder()
        {
            Console.WriteLine("Edit Mode");

            bool f = false;
            do
            {

                Console.WriteLine("Choose an action\n 1. Add new item\t2.Change Quantity\t3.Delete item\t4.I'm Good!\n");
                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        {
                            AddItem();
                            break;
                        }
                    case 2:
                        {
                            ChangeQuantity();
                            break;
                        }

                    case 3:
                        {
                            DeleteItem();
                            break;
                        }
                    case 4:
                        {
                            PlaceOrder();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please choose among the given options!");
                            f = true;
                            break;

                        }
                }
            } while (f == true);
        }

        void PrintBill()
        {
            string FILE_NAME = "BillReady.txt";
            string ALL_ORDERS = "AllSoru" + userName + ".txt";

            if (File.Exists(FILE_NAME))
            {
                File.Delete(FILE_NAME);
            }

            using (FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew))
            {
                using (StreamWriter w = new StreamWriter(fs))
                {
                    w.WriteLine($"\t\t\t\t\t BingeBox- Invoice");
                    w.WriteLine($"\n\n \tRestaurant Name : {restaurantName} \n");
                    w.WriteLine($"\tOrder ID : {orderTime}\n\n\n");
                    w.WriteLine("\n\n\t{0,10}. {1,-45}{2,10}  {3,10}  {4,10}\n", "S.no", "Item name", "Qty", "Price", "Total");
                    w.WriteLine("\t--------------------------------------------------------------------------------------------------\n\n");
                    for (int i = 0; i < items.Count; i++)
                    {
                        w.WriteLine("\t{0,10}. {1,-45}{2,10}{3,10}{4,10}\n", i + 1, items[i], quantity[i], price[i], (price[i] * quantity[i]));
                    }
                    w.WriteLine("\t________________________________________________________________________________________________\n");
                    w.WriteLine($"\n\t Total = Rs.{total}");
                    w.WriteLine("\t________________________________________________________________________________________________\n");
                    if (afterDiscount > 0)
                    w.WriteLine($"Amount to be paid after discount:Rs.{afterDiscount}");

                }
                fs.Close();

            }


            using (FileStream allFiles = new FileStream(ALL_ORDERS, FileMode.Append, FileAccess.Write))
            {

                using (StreamWriter allW = new StreamWriter(allFiles))
                {
                    string content = File.ReadAllText(FILE_NAME);
                    allW.WriteLine(content);
                }
            }

            try
            {
                Process.Start("notepad.exe", FILE_NAME);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



        }

        void ChangeQuantity()
        {
            bool f = true;
            do
            {

                Console.WriteLine("\t{0}. {1,-45}{2,5}{3,10}\n", "S.no", "Item name", "Qty", "Price");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine("\t{0,5}. {1,-45}{2,5}{3,10}\n", i + 1, items[i], quantity[i], (price[i] * quantity[i]));
                    total += (price[i] * quantity[i]);
                }
                Console.WriteLine("\nEnter S.no to update quantity : ");
                int.TryParse(Console.ReadLine(), out int updateSno);
                if (updateSno > 0 && updateSno <= items.Count)
                {

                qCheck:
                    {
                        Console.WriteLine("Update quantity :");
                        int.TryParse(Console.ReadLine(), out int quantity);

                        if (quantity >= 1 && quantity < 10)
                        {
                            this.quantity[updateSno - 1] = quantity;
                            f = false;
                        }
                        else if (quantity > 10)
                        {
                            Console.WriteLine("\nThats too much of food! order less than 10 units!");

                            goto qCheck;
                        }
                        else if (quantity < 1)
                        {
                            Console.WriteLine("\n Invalid entry! Quantity has to be at least 1! ");

                            goto qCheck;

                        }

                    }

                    


                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter valid S.no\n");
                    f = true;
                }
            } while (f == true);


            ViewCart();

        }
        void DeleteItem()
        {
            bool f = true;
            do
            {

                Console.WriteLine("\t{0}. {1,-45}{2,5}{3,10}\n", "S.no", "Item name", "Qty", "Price");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine("\t{0,5}. {1,-45}{2,5}{3,10}\n", i + 1, items[i], quantity[i], (price[i] * quantity[i]));
                    total += (price[i] * quantity[i]);
                }
                Console.WriteLine("\nEnter S.no to delete: ");
                int.TryParse(Console.ReadLine(), out int updateSno);
                if (updateSno > 0 && updateSno <= items.Count)
                {
                    //total = total - (price[updateSno - 1] * quantity[updateSno - 1]);
                    items.RemoveAt(updateSno - 1);
                    quantity.RemoveAt(updateSno - 1);
                    price.RemoveAt(updateSno - 1);
                    f = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter valid S.no\n");
                    f = true;
                }
            } while (f == true);

            ViewCart();

        }

        void AddItem()
        {
            char ch;
            string choice;



            Console.WriteLine($"\tName:{r.restaurantName}\n\tHere is our Menu...\n");
            Console.WriteLine("\t{0,15} {1,-45} {1,5}\n", "Item #", "Item", "Price");

            foreach (var item in r.restaurantItems)
            {
                Console.WriteLine("\t" + item.cuisine + "\n");
                foreach (var fp in item.FoodItem)
                {
                    Console.WriteLine("\t{0,1}{1,-15}{2,-45}{3,5}\n", fp.sno, ".", fp.itemName, fp.ItemPrice);

                }

            }

            bool f = false;


        menu: do
            {
                Console.WriteLine("\n Enter item number \n");
                choice = Console.ReadLine();

                foreach (var item in r.restaurantItems)
                {
                    foreach (var fp in item.FoodItem)
                    {
                        if (string.Equals(choice, fp.sno, StringComparison.OrdinalIgnoreCase))
                        {
                            this.items.Add(fp.itemName);
                           
                          qCheck:
                            {
                                Console.WriteLine("\n Enter quantity \n");
                                int.TryParse(Console.ReadLine(), out int quantity);

                                if (quantity >= 1 && quantity < 10)
                                {
                                    this.quantity.Add(quantity);
                                    this.price.Add(fp.ItemPrice);
                                    f = true;
                                }
                                else if (quantity > 10)
                                {
                                    Console.WriteLine("\nThats too much of food! order less than 10 units!");

                                    goto qCheck;
                                }
                                else if (quantity < 1)
                                {
                                    Console.WriteLine("\n Invalid entry! Quantity has to be at least 1! ");

                                    goto qCheck;

                                }

                            }
                           
                        }



                    }

                }

                if (f == false)
                {
                    Console.WriteLine("Invalid item number!");

                    goto menu;
                }
                Console.WriteLine("Want to eat more? (y/n)");
                char.TryParse(Console.ReadLine(), out char c);
                ch = c;
            } while (ch == 'y');

            ViewCart();
        }
        void ApplyCoupon()
        {
            
        check: Console.WriteLine("Do you have a coupon?(y/n)");

            char.TryParse(Console.ReadLine(), out char c);
            if (c == 'y' || c == 'Y')
            {
                Dictionary<string, int> Coupons = new Dictionary<string, int>();
                Coupons.Add("Soru10", 10);
                Coupons.Add("Soru50", 50);
                Console.WriteLine("Enter code:");
                string code = Console.ReadLine();
                if (Coupons.ContainsKey(code))
                {
                    Console.WriteLine("\n Coupon Added!");
                    afterDiscount = total - (Coupons[code] * total * 0.01F);
                    Console.WriteLine($" Amount to be paid :{afterDiscount}");
                    PrintBill();

                }
                else
                {
                    Console.WriteLine("Sorry, the code is invalid!");
                    PlaceOrder();
                }
            }
            else if (c == 'n' || c == 'N')
            {
                PrintBill();

            }
            else
            {

                Console.WriteLine("Enter only Y/y/N/n. Summa thatta koodadhu!");

                goto check;
            }

          
        }

    }
}


