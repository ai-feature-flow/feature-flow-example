public class Orders
{
    public decimal Total(Order order) => order.Lines.Sum(l => l.Price) / order.Lines.Count;
}
