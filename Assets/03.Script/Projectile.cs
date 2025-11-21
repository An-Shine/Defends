using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;        //이동속도
    Transform target; //공격 목표
    float damage = 0f;  //데미지

    public void SetTarget(Transform tr)
    {
        //공격목표 지정
        target = tr;
    }    
    void Update()
    {
        if(target != null)
        {
            //방향설정
            Vector3 direction = (target.position - transform.position).normalized;
            //이동
            transform.position += moveSpeed * Time.deltaTime * direction;
        }
        else
        {
            //목표가 사라지면 삭제
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        //태그가 적이 아니면 리턴
        if(!collision.CompareTag("ENEMY")) return;

        //충돌한 오브젝트가 목표가 아니면 리턴
        if(collision.transform != target) return;

        //충돌한 적에게 데미지 주기
        collision.GetComponent<Enemy>().TakeDamage(damage);
        //발사체도 삭제
        Destroy(gameObject);
    }
}
