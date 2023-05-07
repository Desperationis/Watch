using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab = null;

    [SerializeField]
    private Health playerHealth = null;

    private List<Heart> hearts = new List<Heart>();

    private void Awake() {
        playerHealth.AddListener(DrawHearts);
    }

    private void Start() {
        DrawHearts(playerHealth.health);
    }

    private void DrawHearts(int health) {
        ClearHearts();

        // Determine hearts to make total based on max health
        float maxHealthRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++) {
            CreateEmptyHeart();
        }

        // Fill in correct detail for each heart
        for(int i = 0; i < hearts.Count; i++) {
            int heartStatusRemainder = Mathf.Clamp(health - i*2, 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }

    }

    private void CreateEmptyHeart() {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Heart heart = newHeart.GetComponent<Heart>();
        heart.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heart);
    }

    private void ClearHearts() {
        // Clear all children gameobjects
        foreach(Transform t in transform) {
            Destroy(t.gameObject);
        }

        hearts = new List<Heart>();
    }
}
