namespace LineDietXF.Enumerations
{
    // NOTE:: must update WeightLogicHelpers extension methods ToSettingsName() and ToSentenceUsageName() if new units are added
    // NOTE:: must update WeightLogicHelpers.ConvertWeightUnits() if new units are added
    // NOTE:: if a new one is added, search for all references to this enum and update relevant cases
    public enum WeightUnitEnum
    {
        ImperialPounds,
        Kilograms,
        StonesAndPounds
    }
}