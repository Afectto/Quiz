using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private string _identifier;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Vector3 _rotateSprite;
    
    public Sprite Sprite => _sprite;

    public string Identifier => _identifier;
    public Vector3 RotateSprite => _rotateSprite;
}