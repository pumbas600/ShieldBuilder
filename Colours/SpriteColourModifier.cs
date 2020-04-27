using UnityEngine;

public static  class SpriteColourModifier
{
    public static Sprite MakeSprite(Sprite original,
                                    Sprite template,
                                    ColourSet colourSet,
                                    ColourSet templateColourSet)
    {
        Texture2D originalTexture = original.texture;
        Texture2D templateTexture = template.texture;

        //If read / write isn't enabled in the sprite import setting it'll throw an error when you try and get / set
        //pixels in the sprites.
        if (!originalTexture.isReadable || !templateTexture.isReadable)
        {
            Debug.LogWarning("You must enable read / write for the sprites in the sprite import settings to use SpriteColourModifier. Returned original");
            return original;
        }

        if (originalTexture.width != templateTexture.width || originalTexture.height != templateTexture.height)
        {
            Debug.LogWarning("The original sprite must be the same size as the template sprite to use SpriteColourModifier. Returned original");
            return original;
        }

        Color32[] colours         = colourSet.GetColoursAsSingleArray();
        Color32[] templateColours = templateColourSet.GetColoursAsSingleArray();

        //This process will not work if these arrays aren't the same length
        if (templateColours.Length != colours.Length) return original;

        Texture2D newTexture = new Texture2D(templateTexture.width, templateTexture.height);
        newTexture.filterMode = FilterMode.Point;

        for (int x = 0; x < templateTexture.width; x++)
        {
            for (int y = 0; y < templateTexture.height; y++)
            {
                if (templateTexture.GetPixel(x, y).a == 0)
                {
                    if (originalTexture.GetPixel(x, y).a == 0) newTexture.SetPixel(x, y, Color.clear);
                    else newTexture.SetPixel(x, y, originalTexture.GetPixel(x, y));
                    continue;
                }

                bool changedPixel = false;
                for (int i = 0; i < templateColours.Length; i++)
                {
                    Color32 templateColour = templateColours[i];
                    if (templateTexture.GetPixel(x, y).Equals(templateColour))
                    {
                        changedPixel = true;
                        newTexture.SetPixel(x, y, colours[i]);
                        //No need to keep checking if the colour matches once one has been found
                        break;
                    }
                }
                //This is just in case there is a border (or something like that) around both the template and the original
                //As because there isn't an alpha pixel on the template, the intial if statement want call.
                if (!changedPixel) newTexture.SetPixel(x, y, originalTexture.GetPixel(x, y));
            }
        }
        //Apply the changes to the texture
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, template.rect, template.pivot, 1, 0, SpriteMeshType.Tight);    
        return newSprite;
    }
}
