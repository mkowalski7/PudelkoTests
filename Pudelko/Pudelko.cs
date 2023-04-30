using System.Collections;
using Pudelko.Enums;
using Pudelko.Utils;

namespace Pudelko;

public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable
{
    private double a;
    private double b;
    private double c;

    private UnitOfMeasure unitType;

    public Pudelko(double? a, double? b, double? c, UnitOfMeasure unitType = UnitOfMeasure.meter)
    {
        this.unitType = unitType;
        this.a = a ?? MeasureConverter.ConvertCentimetersToUnit(10, this.unitType);
        this.b = b ?? MeasureConverter.ConvertCentimetersToUnit(10, this.unitType);
        this.c = c ?? MeasureConverter.ConvertCentimetersToUnit(10, this.unitType);

        RunChecks();
    }

    public double A => MeasureConverter.ConvertToMeters(a, unitType);
    public double B => MeasureConverter.ConvertToMeters(b, unitType);
    public double C => MeasureConverter.ConvertToMeters(c, unitType);

    public double Objetosc => Math.Round(A * B * C, 9);
    public double Pole => Math.Round(2 * (A * C + A * B + B * C), 6);

    public bool Equals(Pudelko? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
    }

    public override bool Equals(object obj)
    {
        return obj is Pudelko pudelko ? Equals(pudelko) : base.Equals(obj);
    }

    public static bool operator ==(Pudelko p1, Pudelko p2) => p1.Equals(p2);
    public static bool operator !=(Pudelko p1, Pudelko p2) => !p1.Equals(p2);

    public static Pudelko operator +(Pudelko p1, Pudelko p2)
    {
        double[] p1Lengths = (double[])p1, p2Lengths = (double[])p2;
        Array.Sort(p1Lengths);
        Array.Sort(p2Lengths);
        return new Pudelko(
            p1Lengths[0] + p2Lengths[0],
            p1Lengths[1] + p2Lengths[1],
            p1Lengths[2] + p2Lengths[2]
        );
    }

    private void RunChecks()
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentOutOfRangeException(nameof(a), "a, b and c must be greater than 0");
        if (A > 10 || B > 10 || C > 10)
            throw new ArgumentOutOfRangeException(nameof(a), "a, b and c must be lower than 10 meters");
    }

    public string ToString(string? unit)
    {
        unit ??= "m";

        switch (unit)
        {
            case "m":
                return $"{a:0.000} m × {b:0.000} m × {c:0.000} m";
            case "cm":
                return $"{a * 100:0.0} cm × {b * 100:0.0} cm × {c * 100:0.0} cm";
            case "mm":
                return $"{a * 1000:0} mm × {b * 1000:0} mm × {c * 1000:0} mm";
        }

        return "";
    }

    public override int GetHashCode()
    {
        return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + unitType.GetHashCode();
    }

    public IEnumerator GetEnumerator()
    {
        return new PudelkoNum(this);
    }

    public static explicit operator double[](Pudelko p) => new double[] { p.A, p.B, p.C };

    public static implicit operator Pudelko(ValueTuple<int, int, int> values)
    {
        return new Pudelko(values.Item1, values.Item2, values.Item3, UnitOfMeasure.milimeter);
    }

    public double this[int index]
    {
        get
        {
            if (index < 3)
            {
                return ((double[])this)[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}

class PudelkoNum : IEnumerator<double>
{
    private readonly Pudelko pudelko;
    private int index = 0;

    public PudelkoNum(Pudelko pudelko)
    {
        this.pudelko = pudelko;
    }

    public double Current => pudelko[index++];

    object IEnumerator.Current => pudelko[index++];

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        return index < 3;
    }

    public void Reset()
    {
        index = 0;
    }
}