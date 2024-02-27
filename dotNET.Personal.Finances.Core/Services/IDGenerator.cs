namespace dotNET.Personal.Finances.Core.Services;

//Clase desarrollada para generar ID automaticamente
public class IDGenerator
{
    private static int count = 0;

    public int getNewID()
    {
        return count++;
    }
}