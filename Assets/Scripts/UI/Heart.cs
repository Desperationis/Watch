using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private Sprite fullHeart = null;

    [SerializeField]
    private Sprite halfHeart = null;

    [SerializeField]
    private Sprite emptyHeart = null;

    [SerializeField]
    private Image heartImage = null;


    public void SetHeartImage(HeartStatus status) {
        switch(status)  {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }

}

public enum HeartStatus 
{
    Empty = 0,
    Half = 1,
    Full = 2
}
