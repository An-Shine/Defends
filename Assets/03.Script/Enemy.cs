using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float maxHp = 5.0f;    //최대체력
    [SerializeField] int gold = 10;
    bool isDie; //사망상태
    float currentHp;    //현재 체력
    int currentIndex;   //현재경로 인덱스
    Animator anim;  //애니메이션 제어용 애니메이터
    SpriteRenderer spriteRenderer; //이미지 반전용 스프라이터렌더러

    EnemyManager emi;   //자주사용할 인스턴스 간소화

    
    public float MaxHp => maxHp;            //최대체력프로퍼티
    public float CurrentHP => currentHp;    //현재체력프로퍼티

    
    public void Init()
    {
        //간소화한 인스턴스 연결
        emi = EnemyManager.Instance;
        //인덱스를 0으로 시작
        currentIndex = 0;
        //위치는 시작 인덱스에서 해당하는 경로 위치로 지정
        transform.position = emi.Waypoints[currentIndex].position;
        //애니메이터 연결
        anim = GetComponent<Animator>();
        //SpriteRenderer 연결
        spriteRenderer = GetComponent<SpriteRenderer>();
        //현재체력을 최대치로 초기화
        currentHp = maxHp;
        //살아있는상태로 시작
        isDie = false;
        
    }

    void Update()
    {   
        //이동 지점 배열의 인덱스 0부터 배열크기 -1까지
        if(currentIndex < emi.Waypoints.Length)        
        {
            //현재위치를 frame 처리시간비율로 계산한 속도만큼 올려줌
            //즉 1개의 프레임단위로 움직임처리
            transform.position = Vector3.MoveTowards(transform.position, emi.Waypoints[currentIndex].position, moveSpeed*Time.deltaTime);           

            //현재 오브젝트가 어느방향으로 이동하는지 검사
            //MoveToward 에서 target - current 값의 x가 0보다 큰지 로 판단
            Vector3 direction = emi.Waypoints[currentIndex].position - transform.position;

            //0보다 크면 오른쪽으로 가는것이므로 SpriteRenderer 의 FlipX 를 true
            //위로 올라가는경우에도 오른쪽을 보게끔
            spriteRenderer.flipX = (direction.x > 0) || (direction.y >0);

            //현재위치가 이동지점의 위치라면 
            if(Vector3.Distance(emi.Waypoints[currentIndex].position,transform.position)==0f)
            {                 
            //배열 인덱스 +1 해서 다음포인트로 이동하도록
                currentIndex++;
            }
        }

        else
        {
            {
                //목표에 도달했으면 삭제처리
                OnDie();
            }
        }
    }
    
    public void OnDie(bool isArriveGoal = false)
    {
        //매니저에서 삭제처리
        emi.DestroyEnemy(this, gold, isArriveGoal);
        
    }

    public void TakeDamage(float damage)
    {
        //죽은상태에서는 더이상 데미지 안받음
        if(isDie) return;
        //데미지만큼 현재 체력 감소
        currentHp -= damage;        
        //체력이 0인지 검사
        if(currentHp <= 0)
        {
            //죽은상태로 만들고 삭제처리
            isDie = true;
            OnDie();
        }

    }
    
}
