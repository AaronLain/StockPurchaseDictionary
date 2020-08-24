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
            // create a new dictionary with the name and the product of shares and price
            var consolidatedDict = new Dictionary<string, double>();

            var prod = shares * price;

            var roundPrice = Math.Round(prod, 2);

            consolidatedDict.Add(name, roundPrice);

            return consolidatedDict;
        }

        static void Main(string[] args)
        {
            var stocks = new Dictionary<string, string>();
            stocks.Add("GE", "General Electric");
            stocks.Add("AAPL", "Apple");
            stocks.Add("IBM", "IBM");

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

            // perform math and round the doubles to two decimals, returns dictionary, adds to list
            var stockDictionary = new List<Dictionary<string, double>>();

            foreach (var purchase in purchases)
            {
                stockDictionary.Add(Consolidator(purchase.ticker, purchase.shares, purchase.price));
            }
            // flattens list of dictionaries,
            var stockTotals = stockDictionary.SelectMany(item => item)
                 // then groups them together
                .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
                // creates new dictionary with single key value and sum of all products
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Sum());

            //iterate and print out the stocks full name, and the consolidated prices of shares
            foreach (var item in stockTotals)
            {
                foreach (var stock in stocks)
                {
                    if (item.Key == stock.Key)
                    {
                        WriteLine($"{stock.Value} {item.Value}");
                    }
                }
            }
        }
    }
}
