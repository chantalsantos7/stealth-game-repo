using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            Invoke("EnableEndScreen", 2f);
            //activate end screen
            //set game time to 0
        }
    }

    private void EnableEndScreen()
    {
        GameManager.Instance.uiManager.EnableEndScreen();
    }
}
