using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Widget> widgets;
        List<Requirement> requirements;
        GetTestRecords(out widgets, out requirements);

        QualifyReward(widgets, requirements);
    }

    private static void GetTestRecords(out List<Widget> widgets, out List<Requirement> requirements)
    {
        //WidgetA 1x
        //WidgetB 3x
        widgets = new List<Widget>();
        Widget widgetA = new Widget() { Name = "Doritos", Category = "Chips", Quantity = 1 };
        Widget widgetB = new Widget() { Name = "Snickers", Category = null, Quantity = 3 };
        widgets.AddRange(new[] { widgetA, widgetB });

        //CategoryA 1x
        //WidgetB 3x
        requirements = new List<Requirement>();
        Requirement requirement1 = new Requirement() { WidgetName = null, Category = "Chips", Quantity = 1 };
        Requirement requirement2 = new Requirement() { WidgetName = "Snickers", Category = null, Quantity = 3 };
        requirements.AddRange(new[] { requirement1, requirement2 });
    }

    private static void QualifyReward(List<Widget> widgets, List<Requirement> requirements)
    {
        var qualifyWidgets = from t1 in widgets
                             join t2 in requirements
                             on t1.Name equals t2.WidgetName
                             where t1.Quantity >= t2.Quantity
                             select new { t1, t2 };

        var qualifyCategories = from t1 in widgets
                                join t2 in requirements
                                on t1.Category equals t2.Category
                                where t1.Quantity >= t2.Quantity
                                select new { t1, t2 };

        if (qualifyWidgets.Count() + qualifyCategories.Count() == requirements.Count())
            Console.WriteLine("Purchases fulfill requirements");

        Console.WriteLine("Product count: {0}, Category count: {1}", qualifyWidgets.Count(), qualifyCategories.Count());
    }
}

public class Widget
{
    public string Name { get; set; }
    public string Category { get; set; }
    public int Quantity { get; set; }
}

public class Requirement
{
    public string WidgetName { get; set; }
    public string Category { get; set; }
    public int Quantity { get; set; }
}