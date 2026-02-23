using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;

    public Sprite[] lvSprites;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    Sprite GetLVSprite(int lv) // 레벨별 스프라이트를 제공해준다. 0레벨은 기본
    {
        return lvSprites[lv];
    }
}
