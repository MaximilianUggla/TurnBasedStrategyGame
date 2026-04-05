public abstract class Faction
{
    ActionCards[] actionCards;
    SecretObjectiveCards[] secretObjectiveCards;
    PlanetCards[] planetCards;
    TechnologyCards[] technologyCards;
    UnitUppgradeCards[] unitUppgradeCards;
    int gameTokens;
    int shipTokens;
    int playTokens;
    int tier;

    void factionAbility();
}