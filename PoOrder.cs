public class PoOrder
{
    public int ponumber { get; set; }
    public string name { get; set; }
    public string sku { get; set; }
    public double price { get; set; }
    public int quantity { get; set; }
    public string messagetype { get; set; }
    public ShipTo shipTo { get; set; }
    public BillTo billTo { get; set; }
}
public class ShipTo
{
    public string name { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zip { get; set; }
}

public class BillTo
{
    public string name { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zip { get; set; }
}