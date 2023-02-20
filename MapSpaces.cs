using System.Collections.Generic;
using CubaLibre.PlayerTokens;

namespace CubaLibre.MapSpaces
{
    public enum RegionType
    {
        Province,
        City,
        EC //Economic Center
    }

    //Consistently use these names for each faction throughout.
    public enum Control
    {
        Govt,
        Syndicate,
        M26,
        DR,
        Uncontrolled
    }

    //The int values here will be multiplied by the region's population when determining support.
    public enum Support
    {
        ActiveSupport       =   2,
        PassiveSupport      =   1,
        Neutral             =   0,
        PassiveOpposition   =   -1,
        ActiveOpposition    =   -2
    }

    public class MapSpace
    {
        //The following 3 attributes never change.

        //popOrEcon: stores population for Provinces and Cities; Economic value for ECs
        public int popOrEcon;
        public RegionType regionType;
        public List<MapSpace> adjacentSpaces;

        //The following 3 attributes will likely change on a regular basis.
        public Control control;
        public Support support;
        //tokens: contains all player tokens currently on the space.
        public List<PlayerToken> tokens;

        /*The constructor would include setting the adjacent MapSpaces
          as well, but all MapSpaces need to be created before adjacent ones can be set. This will be handled
          in the MapBuilder class.*/
        //Default constructor for Standard Deployment.
        public MapSpace(int popOrEcon, RegionType regionType, Support support)
        {
            this.popOrEcon = popOrEcon;
            this.regionType = regionType;
            this.support = support;
            this.adjacentSpaces = new List<MapSpace>();
        }

        //Alternative constructor for variable deployment
        public MapSpace(int popOrEcon, RegionType regionType, Support support, List<PlayerToken> tokens)
        {
            this.popOrEcon = popOrEcon;
            this.regionType = regionType;
            this.support = support;
            this.tokens = tokens;
            this.control = CalculateControl(tokens);
            this.adjacentSpaces = new List<MapSpace>();
        }

        /*Calculate and return control status based on which tokens are on the map space.
        Used for variable deployment and throughout the game.*/
        public static Control CalculateControl(List<PlayerToken> tokens)
        {
            int govtCount = tokens.Where(t => t.faction == eFaction.Govt).Count();
            int syndCount = tokens.Where(t => t.faction == eFaction.Syndicate).Count();
            int drCount = tokens.Where(t => t.faction == eFaction.DR).Count();
            int m26Count = tokens.Where(t => t.faction == eFaction.M26).Count();

            //A Faction Controls a Province or City if its pieces there exceed those of all other Factions combined.
            if(govtCount > syndCount + drCount + m26Count) { return Control.Govt; }
            else if(syndCount > govtCount + drCount + m26Count) { return Control.Syndicate; }
            else if(drCount > govtCount + syndCount + m26Count) { return Control.DR; }
            else if(m26Count > govtCount + syndCount + drCount) { return Control.M26; }
            else { return Control.Uncontrolled; }
        }
    }

    //******************************INDIVIDUAL MAP SPACE CLASSES************************************************
    /* Creating one of these map spaces without providing any information will initialize the space with Standard Deployment values. 
    For variable deployment, all you need to do is provide a List<PlayerToken> to each constructor.*/

    public class PinarDelRio : MapSpace
    {
        //Default constructor for Standard Deployment
        public PinarDelRio() : base(1, RegionType.Province, Support.ActiveSupport)
        {
            this.control = Control.Syndicate;
            this.tokens = new List<PlayerToken>() {new Casino()};
        }

        //Alternative constructor for Variable Deployment
        public PinarDelRio(List<PlayerToken> tokens) : base(1, RegionType.Province, Support.ActiveSupport, tokens){}
    }

    public class WestEC : MapSpace
    {
        //standard deployment
        public WestEC() : base(3, RegionType.EC, Support.Neutral)
        {
            this.control = Control.Uncontrolled;
            this.tokens = new List<PlayerToken>();
        }
        //variable deployment - for ECs, only call the regular base constructor since control is always Uncontrolled
        public WestEC(List<PlayerToken> tokens) : base(3, RegionType.EC, Support.Neutral)
        {
            this.tokens = tokens;
            this.control = Control.Uncontrolled;
        }
    }

    public class LaHabana : MapSpace
    {
        //Standard deployment
        public LaHabana() : base(1, RegionType.Province, Support.PassiveSupport)
        {
            this.control = Control.Uncontrolled;
            this.tokens = new List<PlayerToken>() {new Casino(), new M26Guerrilla()};
        }

        //Variable deployment
        public LaHabana(List<PlayerToken> tokens) : base(1, RegionType.Province, Support.PassiveSupport, tokens){}
    }

    public class Havana : MapSpace
    {
        //standard
        public Havana() : base(6, RegionType.City, Support.ActiveOpposition)
        {
            this.control = Control.Govt;
            this.tokens = GetHavanaTokens();
        }

        //In Standard Deployment, Havana has a lot of starting tokens - get them here.
        private List<PlayerToken> GetHavanaTokens()
        {
            List<PlayerToken> HavanaTokens = new List<PlayerToken>();
            //6 Troops
            for(int i = 0; i < 6; i++)
            {
                HavanaTokens.Add(new Troop());
            }
            //4 police
            for(int i = 0; i < 4; i++)
            {
                HavanaTokens.Add(new Police());
            }
            //1 casino
            HavanaTokens.Add(new Casino());
            //2 DR Guerrillas
            for(int i = 0; i < 2; i++)
            {
                HavanaTokens.Add(new DRGuerrilla());
            }

            return HavanaTokens;
        }

        //Variable deployment
        public Havana(List<PlayerToken> tokens) : base(6, RegionType.City, Support.ActiveSupport, tokens){}
    }

    public class Matanzas : MapSpace
    {
        //standard
        public Matanzas() : base(1, RegionType.Province, Support.PassiveOpposition)
        {
            this.control = Control.Uncontrolled;
            this.tokens = new List<PlayerToken>();
        }

        //variable
        public Matanzas(List<PlayerToken> tokens) : base(1, RegionType.Province, Support.PassiveOpposition, tokens){}
    }

    public class LasVillas : MapSpace
    {
        //standard
        public LasVillas() : base(2, RegionType.Province, Support.Neutral)
        {
            this.control = Control.Govt;
            this.tokens = new List<PlayerToken>() {new Troop(), new Troop(), new Troop()};
        }

        public LasVillas(List<PlayerToken> tokens) : base(1, RegionType.Province, Support.Neutral, tokens){}
    }

    public class CentralEC : MapSpace
    {
        //standard deployment
        public CentralEC() : base(3, RegionType.EC, Support.Neutral)
        {
            this.control = Control.Uncontrolled;
            this.tokens = new List<PlayerToken>();
        }
        //variable deployment - for ECs, only call the regular base constructor since control is always Uncontrolled
        public CentralEC(List<PlayerToken> tokens) : base(3, RegionType.EC, Support.Neutral)
        {
            this.tokens = tokens;
            this.control = Control.Uncontrolled;
        }
    }

    public class CamagueyProvince : MapSpace
    {
        //standard
        public CamagueyProvince() : base(1, RegionType.Province, Support.PassiveOpposition)
        {
            this.control = Control.DR;
            this.tokens = new List<PlayerToken>() {new DRGuerrilla()};
        }

        public CamagueyProvince(List<PlayerToken> tokens) : base(1, RegionType.Province, Support.PassiveOpposition, tokens){}
    }
    
    public class CamagueyCity : MapSpace
    {
        //standard
        public CamagueyCity() : base(1, RegionType.City, Support.PassiveSupport)
        {
            this.control = Control.Govt;
            this.tokens = new List<PlayerToken>() {new Troop(), new Police(), new Police()};
        }

        public CamagueyCity(List<PlayerToken> tokens) : base(1, RegionType.City, Support.PassiveSupport, tokens){}
    }

    public class Oriente : MapSpace
    {
        //standard
        public Oriente() : base(2, RegionType.Province, Support.PassiveOpposition)
        {
            this.control = Control.Uncontrolled;
            this.tokens = new List<PlayerToken>();
        }

        public Oriente(List<PlayerToken> tokens) : base(2, RegionType.Province, Support.PassiveOpposition, tokens){}
    }

    public class EastEC : MapSpace
    {
        //standard deployment
        public EastEC() : base(2, RegionType.EC, Support.Neutral)
        {
            this.control = Control.Uncontrolled;
            this.tokens = new List<PlayerToken>();
        }
        //variable deployment - for ECs, only call the regular base constructor since control is always Uncontrolled
        public EastEC(List<PlayerToken> tokens) : base(2, RegionType.EC, Support.Neutral)
        {
            this.tokens = tokens;
            this.control = Control.Uncontrolled;
        }
    }

    public class SierraMaestra : MapSpace
    {
        //standard
        public SierraMaestra() : base(1, RegionType.Province, Support.ActiveOpposition)
        {
            this.control = Control.M26;
            this.tokens = new List<PlayerToken>() {new M26Guerrilla(), new M26Guerrilla(), new M26Base()};
        }

        public SierraMaestra(List<PlayerToken> tokens) : base(1, RegionType.Province, Support.ActiveOpposition, tokens){}
    }

    public class SantiagoDeCuba : MapSpace
    {
        //standard
        public SantiagoDeCuba() : base(1, RegionType.City, Support.Neutral)
        {
            this.control = Control.Govt;
            this.tokens = new List<PlayerToken>() {new Troop(), new Troop(), new Police(), new Police(), new M26Guerrilla()};
        }

        public SantiagoDeCuba(List<PlayerToken> tokens) : base(1, RegionType.City, Support.Neutral, tokens){}
    }
}