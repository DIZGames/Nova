using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour {

    [SerializeField]
    private Text currentHealth;
    [SerializeField]
    private Text currentArmor;
    [SerializeField]
    private Text currentEnergy;
    [SerializeField]
    private Text currentOxygen;

    [SerializeField]
    private Text maxHealth;
    [SerializeField]
    private Text maxArmor;
    [SerializeField]
    private Text maxEnergy;
    [SerializeField]
    private Text maxOxygen;

    public Player Player { set; get; }

    void Start() {
    }

	void Update () {

        currentHealth.text = Player.currentHealth.ToString();
        currentArmor.text = Player.currentArmor.ToString();
        currentEnergy.text = Player.currentEnergy.ToString();
        currentOxygen.text = Player.currentOxygen.ToString();

        maxHealth.text = Player.maxHealth.ToString();
        maxArmor.text = Player.maxArmor.ToString();
        maxEnergy.text = Player.maxEnergy.ToString();
        maxOxygen.text = Player.maxOxygen.ToString();

	}
}
