using System.Collections.Generic;
using CubaLibre.MapSpaces;
using CubaLibre.PlayerTokens;

namespace CubaLibre.Map
{
    public class MapBuilder
    {
        //Stores all map spaces.
        public static Dictionary<string, MapSpace> GameMap;

        /* Set up the map based on the default set up rules. Populate a dictionary of 
           MapSpace objects accessed by the space name e.g. dict["Havana"]
           returns a MapSpace with all info for Havana. Then, initialize all adjacent MapSpace values.*/
        public static Dictionary<string, MapSpace> StandardDeployment()
        {
            GameMap = new Dictionary<string, MapSpace>();
            GameMap.Add("Pinar del Rio", new PinarDelRio());
            GameMap.Add("West EC", new WestEC());
            GameMap.Add("La Habana", new LaHabana());
            GameMap.Add("Havana", new Havana());
            GameMap.Add("Matanzas", new Matanzas());
            GameMap.Add("Las Villas", new LasVillas());
            GameMap.Add("Central EC", new CentralEC());
            GameMap.Add("Camaguey Province", new CamagueyProvince());
            GameMap.Add("Camaguey City", new CamagueyCity());
            GameMap.Add("Oriente", new Oriente());
            GameMap.Add("East EC", new EastEC());
            GameMap.Add("Sierra Maestra", new SierraMaestra());
            GameMap.Add("Santiago de Cuba", new SantiagoDeCuba());

            InitializeAdjacentSpaces(GameMap);

            //TODO: Once player classes are created, adjust player piece counts according to how many have been placed on the map.
            return GameMap;
        }

        /*Set up the map based on Variable deployment. This requires a list of player tokens for each province.
        Then, initialize all adjacent spaces.*/
        public static Dictionary<string, MapSpace> VariableDeployment(List<List<PlayerToken>> tokens)
        {
            GameMap = new Dictionary<string, MapSpace>();
            GameMap.Add("Pinar del Rio", new PinarDelRio(tokens[0]));
            GameMap.Add("West EC", new WestEC(tokens[1]));
            GameMap.Add("La Habana", new LaHabana(tokens[2]));
            GameMap.Add("Havana", new Havana(tokens[3]));
            GameMap.Add("Matanzas", new Matanzas(tokens[4]));
            GameMap.Add("Las Villas", new LasVillas(tokens[5]));
            GameMap.Add("Central EC", new CentralEC(tokens[6]));
            GameMap.Add("Camaguey Province", new CamagueyProvince(tokens[7]));
            GameMap.Add("Camaguey City", new CamagueyCity(tokens[8]));
            GameMap.Add("Oriente", new Oriente(tokens[9]));
            GameMap.Add("East EC", new EastEC(tokens[10]));
            GameMap.Add("Sierra Maestra", new SierraMaestra(tokens[11]));
            GameMap.Add("Santiago de Cuba", new SantiagoDeCuba(tokens[12]));

            InitializeAdjacentSpaces(GameMap);

            return GameMap;
        }

        //Assign all adjacent spaces.
        private static void InitializeAdjacentSpaces(Dictionary<string, MapSpace> map)
        {
            map["Pinar del Rio"].adjacentSpaces.AddRange(new MapSpace[]{map["West EC"], map["La Habana"]});
            map["West EC"].adjacentSpaces.AddRange(new MapSpace[]{map["Pinar del Rio"], map["La Habana"]});
            map["La Habana"].adjacentSpaces.AddRange(new MapSpace[]{map["Pinar del Rio"], map["West EC"], map["Havana"], map["Matanzas"]});
            map["Havana"].adjacentSpaces.AddRange(new MapSpace[]{map["La Habana"]});
            map["Matanzas"].adjacentSpaces.AddRange(new MapSpace[]{map["La Habana"], map["Las Villas"]});
            map["Las Villas"].adjacentSpaces.AddRange(new MapSpace[]{map["Matanzas"], map["Central EC"], map["Camaguey Province"]});
            map["Central EC"].adjacentSpaces.AddRange(new MapSpace[]{map["Las Villas"], map["Camaguey Province"]});
            map["Camaguey Province"].adjacentSpaces.AddRange(new MapSpace[]{map["Las Villas"], map["Central EC"], map["Camaguey City"], map["Oriente"]});
            map["Camaguey City"].adjacentSpaces.AddRange(new MapSpace[]{map["Camaguey Province"]});
            map["Oriente"].adjacentSpaces.AddRange(new MapSpace[]{map["Camaguey Province"], map["East EC"], map["Sierra Maestra"]});
            map["East EC"].adjacentSpaces.AddRange(new MapSpace[]{map["Oriente"], map["Sierra Maestra"]});
            map["Sierra Maestra"].adjacentSpaces.AddRange(new MapSpace[]{map["Oriente"], map["East EC"], map["Santiago de Cuba"]});
            map["Santiago de Cuba"].adjacentSpaces.AddRange(new MapSpace[]{map["Sierra Maestra"]});
        }
    }
}