using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private float animationSpeed = 0.5F;
    public Sprite[] animationSprites;

    private SpriteRenderer _renderer;
    private int _curFrame = 0;

    public System.Action killed;

    public void Start() {
        this._renderer = GetComponent<SpriteRenderer>();

        InvokeRepeating(nameof(Animation), this.animationSpeed, this.animationSpeed);
    }

    private void Animation() {
        _curFrame = (_curFrame + 1) % this.animationSprites.Length;

        this._renderer.sprite = animationSprites[_curFrame];
        this._renderer.sortingOrder = 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        this.killed.Invoke();
        Destroy(this.gameObject);
    }
}
