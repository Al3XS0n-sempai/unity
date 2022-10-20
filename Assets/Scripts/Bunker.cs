using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour {

    // Count of hits
    public int lives = 5;
    private int _damage = 0;

    private  List<GameObject> _hitSprites = new List<GameObject>();

    private void Start() {
        foreach(Transform sprite in this.transform) {
            if (sprite == null) {
                continue;
            }
            _hitSprites.Add(sprite.gameObject);
        }
        Debug.Log(_hitSprites.Count);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _damage++;

        if (_damage == lives) {
            Destroy(this.gameObject);
        }
        _hitSprites[_damage - 1].SetActive(true);
    }
}
