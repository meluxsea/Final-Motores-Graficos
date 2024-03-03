using UnityEngine;
using UnityEngine.SceneManagement;

public class CondicionVictoria : MonoBehaviour
{

    public GameObject panelVictory;

    public GameObject panelGameOver;

    private void Start()
    {
        panelVictory.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            panelVictory.SetActive(true);


            Invoke("ResetScene", 2f);
            
        }
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    
}

