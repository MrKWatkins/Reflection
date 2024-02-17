namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MethodParameterTypes
{
    public static void ArrayParameter(int[] parameter)
    {
    }

    public static void InParameter(in string parameter)
    {
    }

    public static void MultidimensionalArrayParameter(int[,] parameter)
    {
    }

    public static void NormalParameter(string parameter)
    {
    }

    public static void OutParameter(out int parameter) => parameter = 0;

    public static void ParamsParameter(params string[] parameters)
    {
    }

    public static unsafe void PointerParameter(int* parameter)
    {
    }

    public static void RefParameter(ref int parameter)
    {
    }
}