using System;
using System.Collections.Generic;


namespace FoodDeliveryApp
{
    public class UserLogin
    {
        string userName;
        string password;
        Dictionary<string, string> users = new Dictionary<string, string>();

        public UserLogin()
        {
            users.Add("User1", "Pass1");
            users.Add("User2", "Pass2");
            users.Add("User3", "Pass3");

        }
        public ActiveUser LoginFunction() // check credentials. Login if valid and return reference to active user.
        {
           bool f = false;
           
            Console.WriteLine("Enter username:");
            userName = Console.ReadLine();
                       
            if (users.ContainsKey(userName))
               
                {
                while (f == false)
                {
                    Console.WriteLine("Enter password:");
                    password = Console.ReadLine();
                    if (users[userName] == password)
                    {
                        Console.WriteLine("********* Logged in successfully! *********");
                        f = true;
                      
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Password! Try again");
                        Console.WriteLine($"Username: {userName}");
                    }

                }
            }
            else
            {
                Console.WriteLine("Incorrect username! Try again");
                this.LoginFunction();
            }

            return (new ActiveUser(userName, password));

        }

       

    }
}