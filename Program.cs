using System;
using CubaLibre.MapSpaces;
using CubaLibre.Map;

PinarDelRio pdr = new PinarDelRio();
Control control = MapSpace.CalculateControl(pdr.tokens);
Console.WriteLine(control);
Dictionary<string, MapSpace> map = MapBuilder.StandardDeployment();
foreach(KeyValuePair<string, MapSpace> space in map)
{
    foreach(var t in space.Value.tokens)
    {
        Console.WriteLine(t);
    }
}

