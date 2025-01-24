#region First Project
class Point3D : ICloneable, IComparable<Point3D>
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point3D() : this(0, 0, 0) { }

    public Point3D(int x, int y) : this(x, y, 0) { }

    public Point3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString() => $"Point Coordinates: ({X}, {Y}, {Z})";

    public static bool operator ==(Point3D p1, Point3D p2)
        => p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;

    public static bool operator !=(Point3D p1, Point3D p2) => !(p1 == p2);

    public object Clone() => new Point3D(X, Y, Z);

    public int CompareTo(Point3D other)
    {
        if (X != other.X) return X.CompareTo(other.X);
        return Y.CompareTo(other.Y);
    }
}
#endregion
#region Second Project:
class Maths
{
    public static int Add(int a, int b) => a + b;
    public static int Subtract(int a, int b) => a - b;
    public static int Multiply(int a, int b) => a * b;
    public static double Divide(int a, int b) => b != 0 ? (double)a / b : throw new DivideByZeroException();
}
#endregion
#region Third Project:
abstract class Discount
{
    public string Name { get; set; }
    public abstract decimal CalculateDiscount(decimal price, int quantity);
}

class PercentageDiscount : Discount
{
    private readonly decimal Percentage;

    public PercentageDiscount(decimal percentage)
    {
        Name = "Percentage Discount";
        Percentage = percentage;
    }

    public override decimal CalculateDiscount(decimal price, int quantity)
        => price * quantity * (Percentage / 100);
}

class FlatDiscount : Discount
{
    private readonly decimal FlatAmount;

    public FlatDiscount(decimal flatAmount)
    {
        Name = "Flat Discount";
        FlatAmount = flatAmount;
    }

    public override decimal CalculateDiscount(decimal price, int quantity)
        => FlatAmount * Math.Min(quantity, 1);
}

class BuyOneGetOneDiscount : Discount
{
    public BuyOneGetOneDiscount()
    {
        Name = "Buy One Get One Discount";
    }

    public override decimal CalculateDiscount(decimal price, int quantity)
        => (price / 2) * (quantity / 2);
}

abstract class User
{
    public string Name { get; set; }
    public abstract Discount GetDiscount();
}

class RegularUser : User
{
    public override Discount GetDiscount() => new PercentageDiscount(5);
}

class PremiumUser : User
{
    public override Discount GetDiscount() => new FlatDiscount(100);
}

class GuestUser : User
{
    public override Discount GetDiscount() => null;
}
#endregion
internal class Program
{
    static void Main()
    {
        // First Project:
        Console.WriteLine("Enter coordinates for Point1 (x y z):");
        var input1 = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var p1 = new Point3D(input1[0], input1[1], input1[2]);

        Console.WriteLine("Enter coordinates for Point2 (x y z):");
        var input2 = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var p2 = new Point3D(input2[0], input2[1], input2[2]);

        Console.WriteLine(p1);
        Console.WriteLine(p2);
        Console.WriteLine(p1 == p2 ? "Points are equal" : "Points are not equal");

        var points = new[] { p1, p2, new Point3D(5, 5, 5) };
        Array.Sort(points);
        Console.WriteLine("Sorted Points:");
        foreach (var point in points) Console.WriteLine(point);

        // Second Project:
        Console.WriteLine("Math Operations:");
        Console.WriteLine($"Add: {Maths.Add(10, 5)}");
        Console.WriteLine($"Subtract: {Maths.Subtract(10, 5)}");
        Console.WriteLine($"Multiply: {Maths.Multiply(10, 5)}");
        Console.WriteLine($"Divide: {Maths.Divide(10, 5)}");

        // Third Project:
        Console.WriteLine("Enter user type (Regular, Premium, Guest):");
        var userType = Console.ReadLine();
        User user = userType switch
        {
            "Regular" => new RegularUser { Name = "Regular User" },
            "Premium" => new PremiumUser { Name = "Premium User" },
            "Guest" => new GuestUser { Name = "Guest User" },
            _ => throw new Exception("Invalid user type")
        };

        Console.WriteLine("Enter product price and quantity:");
        var productInput = Console.ReadLine().Split().Select(decimal.Parse).ToArray();
        var price = productInput[0];
        var quantity = (int)productInput[1];

        var discount = user.GetDiscount();
        var discountAmount = discount?.CalculateDiscount(price, quantity) ?? 0;
        Console.WriteLine($"Total Discount: {discountAmount:C}");
        Console.WriteLine($"Final Price: {(price * quantity) - discountAmount:C}");
    }
}
