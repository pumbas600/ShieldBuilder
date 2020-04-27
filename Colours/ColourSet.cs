using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Colour Set", menuName = "Shields/Colour Set")]
public class ColourSet : ScriptableObject
{
    public new string name;
    public Colour light;
    public Colour dark;

    public Color32[] GetColoursAsSingleArray()
    {
        return Utilities.CombineTwoArrays(ColourManager.GetColourSet(light),
                                          ColourManager.GetColourSet(dark));
    }

    [InspectorButton]
    public void SetName()
    {
        name = light.ToString().Replace('_', ' ') + " & " +
               dark.ToString().Replace('_', ' ');
    }
}
