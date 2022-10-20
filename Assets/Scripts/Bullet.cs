using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector3 direction;

    public float speed = 0.0F;

    private void Update() {
        this.transform.position += direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(this.gameObject);
    }
}
