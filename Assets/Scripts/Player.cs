using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public Bullet bulletPrefab;
    public float speed = 3.0F;

    private float _coolDown = 0.3F;

    public TMP_Text livesText;
    public int lives = 3;

    private void Start() {
        livesText.text = string.Format("Lives: {0}", this.lives);
    }

    private void Update() {
        if (_coolDown > 0.0F) {
            _coolDown = Mathf.Max(0, _coolDown - Time.deltaTime);
            return;
        }

        if (Input.GetKey(KeyCode.A)) {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D)) {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space)) {
            Shoot();
            _coolDown = 0.3F;
        }
    }

    private void Shoot() {
        Instantiate(this.bulletPrefab, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        this.lives -= 1;

        if (lives != 0) {
            livesText.text = string.Format("Lives: {0}", this.lives);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
