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

    private Player player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	void Update () {

        currentHealth.text = player.currentHealth.ToString();
        currentArmor.text = player.currentArmor.ToString();
        currentEnergy.text = player.currentEnergy.ToString();
        currentOxygen.text = player.currentOxygen.ToString();

        maxHealth.text = player.maxHealth.ToString();
        maxArmor.text = player.maxArmor.ToString();
        maxEnergy.text = player.maxEnergy.ToString();
        maxOxygen.text = player.maxOxygen.ToString();

	}
}
