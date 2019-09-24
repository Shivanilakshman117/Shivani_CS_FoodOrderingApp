using System.Collections.Generic;


namespace FoodDeliveryApp
{
    public class FoodItemDetails
    {
        public string itemName, sno;
        private float itemprice;
        public float ItemPrice
        {
            get
            { return itemprice; }

            set
            {
                if (value > 0 && value < 500)
                    itemprice = value;
                else itemprice = 10;
            }
        }


        public FoodItemDetails(string itemName, float itemPrice, int count, string cuisine)
        {
            this.itemName = itemName;
            this.ItemPrice = itemPrice;
            this.sno = cuisine + (count + 1);


        }

    }
    public class Menu
    {
        public string cuisine;
        public List<FoodItemDetails> FoodItem = new List<FoodItemDetails>();


    }

    public class SouthIndianCuisine : Menu
    {
        public SouthIndianCuisine()
        {
            cuisine = "South Indian";
            FoodItem.Add(new FoodItemDetails("Idly", 50, FoodItem.Count, "SI"));
            FoodItem.Add(new FoodItemDetails("Dosa", 60, FoodItem.Count, "SI"));
            FoodItem.Add(new FoodItemDetails("Pongal", 70, FoodItem.Count, "SI"));
            FoodItem.Add(new FoodItemDetails("Chappati Kurma", 80, FoodItem.Count, "SI"));
            FoodItem.Add(new FoodItemDetails("Idiappam", 60, FoodItem.Count, "SI"));
            FoodItem.Add(new FoodItemDetails("Sambhar Rice", 60, FoodItem.Count, "SI"));
        }
    }

    public class NorthIndianCuisine : Menu
    {
        public NorthIndianCuisine()
        {
            cuisine = "North Indian";
            FoodItem.Add(new FoodItemDetails("Channa Bature", 150, FoodItem.Count, "NI"));
            FoodItem.Add(new FoodItemDetails("Butter Naan & Paneer Butter Masala", 200, FoodItem.Count, "NI"));
            FoodItem.Add(new FoodItemDetails("Phulka & Malai Kofta", 210, FoodItem.Count, "NI"));
            FoodItem.Add(new FoodItemDetails("North Indian Thali", 300, FoodItem.Count, "NI"));
            FoodItem.Add(new FoodItemDetails("Paneer Tikka", 190, FoodItem.Count, "NI"));
            FoodItem.Add(new FoodItemDetails("Rajma Chawal", 160, FoodItem.Count, "NI"));
        }
    }

    public class ChineseCuisine : Menu
    {
        public ChineseCuisine()
        {
            cuisine = "Chinese";
            FoodItem.Add(new FoodItemDetails("Spring Rolls", 50, FoodItem.Count, "C"));
            FoodItem.Add(new FoodItemDetails("Schezwan Noodles", 160, FoodItem.Count, "C"));
            FoodItem.Add(new FoodItemDetails("Shitake Fried Rice with Water Chestnuts", 270, FoodItem.Count, "C"));
            FoodItem.Add(new FoodItemDetails("Dim Sums", 180, FoodItem.Count, "C"));
            FoodItem.Add(new FoodItemDetails("Chilli Corn Masala", 60, FoodItem.Count, "C"));
            FoodItem.Add(new FoodItemDetails("Honey Chilly Potato", 60, FoodItem.Count, "C"));
        }
    }
}
