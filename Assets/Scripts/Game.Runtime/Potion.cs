using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int PotionSelect = 1;
    public Sprite[] PotionSprite;
    void Start()
    {
        PotionSelect = Random.Range(1, 1); 
        
        // Change sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = PotionSprite[PotionSelect-1];
    }

    void Update()
    {
    }

}
