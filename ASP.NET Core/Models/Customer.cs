using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP_NET_Core.Models;

public class Order {
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerID { get; set; }
    public string CustomerName { get; set; }
    public string ShipCountry { get; set; }
    public string ShipCity { get; set; }
}

public class Customer {
    [Required]
    public int CustomerID { get; set; }

    [Required]
    [MaxLength(10)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(10)]
    public string LastName { get; set; }

    public DateTime HireDate { get; set; }

    public List<Order> Orders { get; set; }
}
