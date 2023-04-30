using Pudelko.Enums;

namespace Pudelko.Utils;

public static class MeasureConverter
{
    public static double ConvertCentimetersToUnit(double value, UnitOfMeasure unit)
    {
        return unit switch
        {
            UnitOfMeasure.milimeter => value * 10,
            UnitOfMeasure.meter => value / 100,
            _ => value
        };
    }

    public static double ConvertToMeters(double value, UnitOfMeasure unit)
    {
        return unit switch
        {
            UnitOfMeasure.milimeter => value / 1000,
            UnitOfMeasure.centimeter => value / 100,
            _ => value
        };
    }

    public static double ConvertToCentimeters(double value, UnitOfMeasure unit)
    {
        return unit switch
        {
            UnitOfMeasure.milimeter => value / 10,
            UnitOfMeasure.meter => value * 100,
            _ => value
        };
    }
    
    public static double ConvertToMilimeters(double value, UnitOfMeasure unit)
    {
        return unit switch
        {
            UnitOfMeasure.centimeter => value * 10,
            UnitOfMeasure.meter => value * 1000,
            _ => value
        };
    }
}