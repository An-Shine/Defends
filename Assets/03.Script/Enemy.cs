using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    int currentIndex;
    EnemyManager emi;   //자주사용할 인스턴스 간소화
    
    public void Init()
    {
        //간소화한 인스턴스 연결
        emi = EnemyManager.Instance;
        //인덱스를 0으로 시작
        currentIndex = 0;
        //위치는 시작 인덱스에서 해당하는 경로 위치로 지정
        transform.position = emi.Waypoints[currentIndex].position;
        
    }

    void Update()
    {   
        //이동 지점 배열의 인덱스 0부터 배열크기 -1까지
        if(currentIndex < emi.Waypoints.Length)        
        {
            //현재위치를 frame 처리시간비율로 계산한 속도만큼 올려줌
            //즉 1개의 프레임단위로 움직임처리
            transform.position = Vector3.MoveTowards(transform.position, emi.Waypoints[currentIndex].position, moveSpeed*Time.deltaTime);            

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
    public void OnDie()
    {
        //매니저에서 삭제처리
        emi.Destroy(this);
        
    }
    
}
