using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace StockPurchaseDictionary
{
    class Program
    {
        static Dictionary<string, double> Consolidator(string name, int shares, double price)
        {
            var consolidatedDict = new Dictionary<string, double>();

            var prod = shares * price;

            consolidatedDict.Add(name, prod);

            return consolidatedDict;
        }

        static void Main(string[] args)
        {
            var stocks = new Dictionary<string, string>();
            stocks.Add("GE", "General Electric");
            stocks.Add("AAPL", "Apple");
            stocks.Add("IBM", "IBM");

            string GM = stocks["GE"];
            WriteLine($"{GM}");

            var purchases = new List<(string ticker, int shares, double price)>();

            purchases.Add((ticker: "GE", shares: 150, price: 6.21));
            purchases.Add((ticker: "GE", shares: 32, price: 7.87));
            purchases.Add((ticker: "GE", shares: 80, price: 9.02));

            purchases.Add((ticker: "AAPL", shares: 150, price: 523.21));
            purchases.Add((ticker: "AAPL", shares: 32, price: 517.79));
            purchases.Add((ticker: "AAPL", shares: 80, price: 516.03));

            purchases.Add((ticker: "IBM", shares: 150, price: 124.21));
            purchases.Add((ticker: "IBM", shares: 32, price: 119.87));
            purchases.Add((ticker: "IBM", shares: 80, price: 129.52));

            var stockDictionary = new List<Dictionary<string, double>>();

            foreach (var purchase in purchases)
            {
                stockDictionary.Add(Consolidator(purchase.ticker, purchase.shares, purchase.price));
            }

            var stockTotals = stockDictionary.SelectMany(item => item)
                // above flattens list of dictionaries, then groups them together
                .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
                // create new dictionary with single key value and sum of all products
                .ToDictionary(k => k.Key, k => k.Sum());

            foreach (var item in stockTotals)
            {
                WriteLine($"{item.Key} {item.Value}");

            }


        }
    }
}
