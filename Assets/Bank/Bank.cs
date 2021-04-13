using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour {
  [SerializeField] int startingBalance = 150;
  [SerializeField] int currentBalance = 150;
  [SerializeField] TextMeshProUGUI displayBalance;

  public int CurrentBalance { get { return currentBalance; } }


  void Awake() {
    currentBalance = startingBalance;

    UpdateDisplay();
  }
  public void Deposit(int amount) {
    currentBalance += Mathf.Abs(amount);

    UpdateDisplay();
  }

  public void Withdraw(int amount) {
    currentBalance -= Mathf.Abs(amount);

    if (currentBalance < 0) {
      ReloadScene();
    }

    UpdateDisplay();
  }
  void UpdateDisplay() {
    if (currentBalance > 1000) {
      displayBalance.text = "VICTORY: " + currentBalance;
    } else {
      displayBalance.text = "Gold: " + currentBalance;
    }
  }

  void ReloadScene() {
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currentScene.buildIndex);
  }

}
