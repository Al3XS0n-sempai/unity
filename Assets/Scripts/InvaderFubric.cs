using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InvaderFubric : MonoBehaviour {
    public Invader[] prefabs;
    public Bullet bulletPrefab;

    public int rows = 5;
    public int columns = 12;

    // true -> right
    // false -> left
    private bool _direction = true;
    public float speed = 0.4F;
    
    // atackRate in [0, 1]
    // percentage of shooting
    public float atackRate;
    public int _alive;

    private bool _started = false;

    public TMP_Text scoreText;
    private int _score = 0;

    private void Start() {
        _alive = this.rows * this.columns;        

        this.transform.position = new Vector3(0, 0, 0);
        Vector3 basePosition = new Vector3(0, 15 - rows * 3 - 0.5F, 0);

        for (int i = 1; i <= rows; ++i) {

            basePosition.x = -columns - 1;
            basePosition.y += 3;

            for (int j = 1; j <= columns; ++j) {
                Invader invader = Instantiate(this.prefabs[i - 1], this.transform);
                invader.killed += killInvader;
                Vector3 pos = basePosition;
                pos.x += 2 * j;
                invader.transform.localPosition = pos;
            }
        }

        // Enemy atack
        if (!_started) {
            InvokeRepeating(nameof(Atack), this.atackRate, this.atackRate);
        }
        _started = true;
    }

    private void Atack() {
        foreach (Transform invader in this.transform) {
            if (invader.gameObject.activeInHierarchy && Random.value <= this.atackRate / (this._alive + 1)) {
                Instantiate(this.bulletPrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void Update() {
        Vector3 _moveRight = (Vector3)Vector2.right * this.speed * Time.deltaTime;
        Vector3 _moveLeft = (Vector3)Vector2.left * this.speed * Time.deltaTime;

        this.transform.position += (_direction ? _moveRight : _moveLeft);

        foreach (Transform invader in this.transform) {
            if (_direction && invader.position.x >= 14.5) {
                _direction = false;
                moveDown();
            } else if (!_direction && invader.position.x <= -14.5) {
                _direction = true;
                moveDown();
            }
        }

        foreach (Transform invader in this.transform) {
            if (invader.position.y <= -9) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void moveDown() {
        Vector3 tmp = this.transform.position;
        tmp.y -= 0.5F;
        this.transform.position = tmp;
    }

    private void killInvader() {
        this._alive--;
        this._score += 10;

        scoreText.text = string.Format("Score: {0}", this._score);

        if (this._alive == 0) {
            Start();
        }
    }
}
