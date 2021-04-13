using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour {
  [SerializeField] int maxHitPoints = 5;
  [Tooltip("Adds amount to maxHP when enemy dies")]
  [SerializeField] int difficulty = 1;

  int currentHitPoints = 0;

  Enemy enemy;

  void OnEnable() {
    currentHitPoints = maxHitPoints;
  }

  void Start() {
    enemy = GetComponent<Enemy>();
  }

  void OnParticleCollision(GameObject other) {
    currentHitPoints--;
    if (currentHitPoints < 1) {
      StartDeathSequence();
    }
  }

  void StartDeathSequence() {
    enemy.RewardGold();
    maxHitPoints += difficulty;
    gameObject.SetActive(false);
  }
}
