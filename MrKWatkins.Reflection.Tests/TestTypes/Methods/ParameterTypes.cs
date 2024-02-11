namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class ParameterTypes
{
    public static void ArrayParameter(int[] parameter)
    {
    }

    public static void MultidimensionalArrayParameter(int[,] parameter)
    {
    }

    public static void OutParameter(out int parameter) => parameter = 0;


    public static unsafe void PointerParameter(int* parameter)
    {
    }

    public static void RefParameter(ref int parameter)
    {
    }
}