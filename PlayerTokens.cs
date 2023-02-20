namespace CubaLibre.PlayerTokens
{
    //eFaction: the "e" is just for enum
    public enum eFaction
    {
        Govt,
        Syndicate,
        M26,
        DR
    }

    public enum PlayerTokenType
    {
        Troop,
        Police,
        Guerrilla,
        Base,
        Casino
    }

    //Represents any player token, be it troops, police, guerrillas, bases, or casinos.
    public class PlayerToken
    {
        public eFaction faction;
        public PlayerTokenType type;
        //isActive: used only for Guerrillas and Casinos. For Troops/Police, isActive is always false.
        //If a casino isActive, the casino is open.
        public bool isActive;

        public PlayerToken(eFaction faction, PlayerTokenType type, bool isActive)
        {
            this.faction = faction;
            this.type = type;
            this.isActive = isActive;
        }
    }

    //**************************INDIVIDUAL PLAYER TOKEN CLASSES***********************************

    public class Troop : PlayerToken
    {
        public Troop() : base(eFaction.Govt, PlayerTokenType.Troop, false){}
    }

    public class Police : PlayerToken
    {
        public Police() : base(eFaction.Govt, PlayerTokenType.Troop, false){}
    }

    public class M26Guerrilla : PlayerToken
    {
        public M26Guerrilla() : base(eFaction.M26, PlayerTokenType.Guerrilla, false){}
    }

    public class DRGuerrilla : PlayerToken
    {
        public DRGuerrilla() : base(eFaction.DR, PlayerTokenType.Guerrilla, false){}
    }

    public class SyndicateGuerrilla : PlayerToken
    {
        public SyndicateGuerrilla() : base(eFaction.Syndicate, PlayerTokenType.Guerrilla, false){}
    }

    public class GovtBase : PlayerToken
    {
        public GovtBase() : base(eFaction.Govt, PlayerTokenType.Base, false){}
    }

    public class M26Base : PlayerToken
    {
        public M26Base() : base(eFaction.M26, PlayerTokenType.Base, false){}
    }

    public class DRBase : PlayerToken
    {
        public DRBase() : base(eFaction.DR, PlayerTokenType.Base, false){}
    }

    public class Casino : PlayerToken
    {
        public Casino() : base(eFaction.Syndicate, PlayerTokenType.Base, true){}
    }
}