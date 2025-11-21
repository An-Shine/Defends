using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpViewer : MonoBehaviour
{
    Enemy enemy;
    Slider slider;

    public void Setup(Enemy enemy)
    {
        this.enemy = enemy;
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = enemy.CurrentHP / enemy.MaxHp;
    }
}
