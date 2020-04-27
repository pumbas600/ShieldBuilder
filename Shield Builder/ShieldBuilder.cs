using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldBuilder : MonoBehaviour, IShieldBuilder
{
    public GameObject[] tabs;
    public Transform[] tabContents;
    private GameObject currentTab;

    public Image previewShield;
    public GameObject[] prefabs;

    public ColourSet templateColours;
    public List<Sprite> unlockedPatterns = new List<Sprite>();
    public List<ColourSet> unlockedColours = new List<ColourSet>();
    public Sprite currentPattern;
    public ColourSet currentColour;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();   
    }

    public void Initialise()
    {
        SetTab(0);

        foreach (Sprite pattern in unlockedPatterns)
        {
            InstantiatePattern(pattern);
        }

        foreach (ColourSet colour in unlockedColours)
        {
            InstantiateColour(colour);
        }
    }

    public void SetPattern(Sprite pattern)
    {
        //If they have the same name, then that patterns already set.
        if (currentPattern != null && currentPattern.name.Equals(pattern.name)) return;

        previewShield.sprite = SpriteColourModifier.MakeSprite(previewShield.sprite,
                                                               pattern,
                                                               currentColour,
                                                               templateColours);
        currentPattern = pattern;
    }

    public void SetColour(ColourSet colour)
    {
        //No need to waste resources trying to apply the same colour to the shield.
        if (currentColour.Equals(colour)) return;

        if (currentPattern != null)
        {
            previewShield.sprite = SpriteColourModifier.MakeSprite(previewShield.sprite,
                                                                   currentPattern,
                                                                   colour,
                                                                   templateColours);
        }
        currentColour = colour;
    }

    public void SetTab(int tab)
    {
        if (tab < tabs.Length)
        {
            if (currentTab != null) currentTab.SetActive(false);
            currentTab = tabs[tab];
            currentTab.SetActive(true);
        }
    }

    public void SwapColours()
    {       
        ColourSet swappedColours = (ColourSet)ScriptableObject.CreateInstance("ColourSet");
        swappedColours.light = currentColour.dark;
        swappedColours.dark = currentColour.light;
        SetColour(swappedColours);
    }

    public void SaveShield()
    {
        Sprite shield = previewShield.sprite;
        print(shield.rect);
        //Do something with the shield.
    }

    private void InstantiatePattern(Sprite pattern)
    {
        int tabIndex = (int)ShieldBuilderTab.PATTERNS;

        Transform parent = tabContents[tabIndex];
        GameObject shieldPattern = Instantiate(prefabs[tabIndex], parent);

        //Assign pattern
        shieldPattern.transform.GetChild(0).GetComponent<Image>().sprite = pattern;
        shieldPattern.GetComponentInChildren<TMP_Text>().text = pattern.name;
        shieldPattern.GetComponentInChildren<Button>().onClick.AddListener(() => SetPattern(pattern));
    }

    private void InstantiateColour(ColourSet colour)
    {
        int tabIndex = (int)ShieldBuilderTab.COLOURS;

        Transform parent = tabContents[tabIndex];
        GameObject colourSet = Instantiate(prefabs[tabIndex], parent);

        //Assign colours
        Image colourPalette = colourSet.transform.GetChild(0).GetComponent<Image>();
        colourPalette.sprite = SpriteColourModifier.MakeSprite(colourPalette.sprite,
                                                               colourPalette.sprite,
                                                               colour,
                                                               templateColours);
        colourSet.GetComponentInChildren<TMP_Text>().text = colour.name;
        colourSet.GetComponentInChildren<Button>().onClick.AddListener(() => SetColour(colour));
    }

}
