using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   public static EnemyManager Instance; //싱글톤 인스턴스
   [SerializeField] GameObject enemyPrefab; //적 프리펩
   [SerializeField] float spawnTime;   // 생성시간
   [SerializeField] Transform[] waypoints;  //이동 위치 배열
   [SerializeField] Transform canvasTransform; //UI 를 표시할 캔버스의 transform
   [SerializeField] GameObject enemyHpSliderPrefab;
   List<Enemy> enemyList;   //생성된 적 리스트

   public Transform[] Waypoints => waypoints;   //이동위치배열 프로퍼티
   public List<Enemy> EnemyList => enemyList;   //적 리스트 프로퍼티

   void Awake()
   {
        if(Instance == null)
        {
            Instance = this;
        }
   }

   //Start 는 코루틴으로 사용할 수 있다
   IEnumerator Start()
   {
        //생성된 적 리스트 초기화
        enemyList = new List<Enemy>();
        while(true)
        {
            //적 프리펩으로 오브젝트를 생성하고 Enemy 스크립트 연결
            Enemy enemy = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            enemy.Init();
            //적을 리스트에 넣기
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }
   }

   public void DestroyEnemy(Enemy enemy, int gold, bool isArriveGoal)
   {
    if(isArriveGoal)
    {
        //도착했다면 유저에게 데미지
    }
    else
    {
        //아니면 골드 증가
    }
    //적 리스트에서 지정한 적 지우기
    enemyList.Remove(enemy);
    Destroy(enemy.gameObject);
   }

   void SpawnEnemyHPSlider(Enemy enemy)
   {
    //슬라이더 클론 생성
    GameObject sliderClone = Instantiate(enemyHpSliderPrefab, canvasTransform);
    //크기 지정
    sliderClone.transform.localScale = Vector3.one;
    //위치 지정
    sliderClone.GetComponent<SliderPosAuto>().Setup(enemy.transform);
    //값 지정


   }
}
