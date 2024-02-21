using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField] 
    private Transform shootTransform;

    [SerializeField] 
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // 마우스 이동 제어
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        // 미사일 발사 함수 호출
        if (GameManager.insatance.isGameOver == false){
            Shoot();
        }
    }


    // 미사일 발사 함수
    void Shoot(){
        if (Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.insatance.SetGameOver();
            Destroy(gameObject);
        } else if(other.gameObject.tag == "Coin"){
            GameManager.insatance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade(){
        if (weaponIndex < weapons.Length-1){
            weaponIndex += 1;
        }
    }
}
