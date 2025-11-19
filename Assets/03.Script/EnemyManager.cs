using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   public static EnemyManager Instance; //싱글톤 인스턴스
   [SerializeField] GameObject enemyPrefab; //적 프리펩
   [SerializeField] float spawnTime;   // 생성시간
   [SerializeField] Transform[] waypoints;  //이동 위치 배열

   public Transform[] Waypoints => waypoints;   //이동위치배열 프로퍼티

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
        while(true)
        {
            //적 프리펩으로 오브젝트를 생성하고 Enemy 스크립트 연결
            Enemy enemy = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
        }
   }
}
