using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDamageIndicator : MonoBehaviour
{
    private float timeAccumulation = 0.0f;

    private Color DAMAGE_COLOR = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    private Color REGULAR_COLOR = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private bool flashRed = false;

    private bool recovering = false;

    [SerializeField]
    private float flashDuration = 0.2f;


    [SerializeField]
    private SpriteRenderer spriteRenderer;

    void Update() {
        if(recovering) {
            Color interColor = Color.Lerp(DAMAGE_COLOR, REGULAR_COLOR, timeAccumulation / flashDuration);

            spriteRenderer.color = interColor;

            timeAccumulation += Time.deltaTime;

            if(timeAccumulation >= flashDuration / 2.0f) {
                recovering = false;
                timeAccumulation = 0.0f;
                spriteRenderer.color = REGULAR_COLOR;
            }
        }
        else if(flashRed) {
            Color interColor = Color.Lerp(REGULAR_COLOR, DAMAGE_COLOR, timeAccumulation / flashDuration);

            spriteRenderer.color = interColor;

            timeAccumulation += Time.deltaTime;

            if(timeAccumulation >= flashDuration / 2.0f) {
                recovering = true;
                flashRed = false;
                timeAccumulation = 0.0f;
            }
        }
    } 

    // Called when Mob takes Damage
    public void FlashRed() {
        flashRed = true;
    }
}
