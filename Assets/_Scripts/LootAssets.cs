using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAssets : MonoBehaviour
{
    public static LootAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite metalScrapsSprite;
    public Sprite coinSprite;
    public Sprite weddingRingSprite;
    public Sprite cellPhoneSprite;
    public Sprite sunGlassesSprite;
    public Sprite necklaceSprite;
    public Sprite braceletSprite;
}
