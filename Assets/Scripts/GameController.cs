using UnityEngine;

public class GameController : MonoBehaviour
{

    public PlayerMove player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerHealth <= 0)
        {
            Application.Quit();
        }
    }
}
