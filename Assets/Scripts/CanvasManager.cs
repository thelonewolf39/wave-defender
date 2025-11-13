using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private EnemyWaveSpawner gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetUserDefinedTimeBetweenWaves() != gameManager.GetCurrentTimeBetweenWave())
        {
            timeText.gameObject.SetActive(true);
        }

        moneyText.text = "Money: " + player.GetComponent<Turret>().money.ToString();
        healthText.text = "Health: " + player.GetComponent<Turret>().GetHealth().ToString();
        timeText.text = "Time: " + gameManager.GetCurrentTimeBetweenWave().ToString();
        if(gameManager.GetCurrentTimeBetweenWave() == gameManager.GetUserDefinedTimeBetweenWaves())
        {
            timeText.gameObject.SetActive(false);
        }
    }
}
