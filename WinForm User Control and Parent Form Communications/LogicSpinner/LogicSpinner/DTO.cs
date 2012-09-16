using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicSpinner
{
    public class Reward
    {
        public int Id { get; set; }
        public int Priority { get; set; } //1-100; default is 50
        public string Type { get; set; } //Percent, Quantity, Units
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean IsActive { get; set; }
        public string ProductCsv { get; set; }
        public string Descrition { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    public class Receipt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ReceiptItem> ReceiptItems { get; set; }        
    }

    public class ReceiptItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
    }

    public class ReceiptItemName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
