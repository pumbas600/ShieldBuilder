using UnityEngine;

public class ColourManager : MonoBehaviour
{
    #region Singleton

    public static ColourManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion


    [NamedArray(typeof(Colour))]
    public ColourPalette[] colourSets = new ColourPalette[System.Enum.GetValues(typeof(Colour)).Length];

    public static Color32[] GetColourSet(Colour colour)
    {
        return instance.colourSets[(int)colour].palette;
    }
}

[System.Serializable]
public class ColourPalette
{
    public Color32[] palette = new Color32[4];
}

public enum Colour { 
    Yellow, Black, White, Red, Light_Blue, Navy_Blue, Brown, Orange, Light_Orange,
    Lime, Green, Lavender, Purple, Royal_Blue, Sky_Blue, Blue, Grey, Dark_Grey, }