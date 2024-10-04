using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;

    public List<Sprite> spriteList; // Danh sách sprite trong Unity Inspector
    private Dictionary<string, Sprite> spriteDict;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //    InitializeSpriteDictionary();
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        Instance = this;
        InitializeSpriteDictionary();

    }

    private void InitializeSpriteDictionary()
    {
        spriteDict = new Dictionary<string, Sprite>();
        foreach (Sprite sprite in spriteList)
        {
            spriteDict[sprite.name] = sprite;
        }
    }

    public Sprite GetSpriteByName(string spriteName)
    {
        if (spriteDict.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite with name {spriteName} not found!");
            return null; // Hoặc bạn có thể trả về một sprite mặc định nếu không tìm thấy
        }
    }
}
