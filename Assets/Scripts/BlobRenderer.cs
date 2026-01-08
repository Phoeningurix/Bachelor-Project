using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float happiness = 0;
    public Sprite spriteHappyBlob;
    public Sprite spriteNeutralBlob;
    public Sprite spriteSadBlob;
    
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Blob can now experience emotions");
    }

    //When does this happen?
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Set Sprite
    public void setSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (happiness > 0)
        {
            setSprite(spriteHappyBlob);
        } else if (happiness < 0)
        {
            setSprite(spriteSadBlob);
        }
        else
        {
            setSprite(spriteNeutralBlob);
        }
    }
}
