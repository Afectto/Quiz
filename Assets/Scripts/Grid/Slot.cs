using System;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _itemSkin;
    private string _identifier;
    public string Identifier => _identifier;

    public static Action<Transform, bool> OnClickSlotAnimation;
    private void Start()
    {
        QuestGameObserver.OnCheckAnswer += IsCorrectAnswer;
    }

    private void IsCorrectAnswer(string text, bool obj)
    {
        if(text != _identifier) return;
        
        OnClickSlotAnimation.Invoke(_itemSkin.transform, obj);
    }

    public void ChangeItemSkin(Sprite skin)
    {
        _itemSkin.sprite = skin;
    }

    public void ChangeBackgroundColor(Color color)
    {
        _background.color = color;
    }

    public void ChangIdentifier(string identifier)
    {
        _identifier = identifier;
    }
    
    private void OnDestroy()
    {
        QuestGameObserver.OnCheckAnswer -= IsCorrectAnswer;
    }
}
