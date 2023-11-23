using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Skor : MonoBehaviour
{
    public int skor = 0;
    public TextMeshPro scoreText;
    public TextMeshProUGUI scoreUI;
    public int timer = 60;
    public int hedefSayi = 10;
    public TextMeshProUGUI timerText, winFailText;
    public GameObject endPanel;
    public Transform pota;
    public AudioSource basketSesi;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        if(timer > 0)
        {
            timer--;
            StartCoroutine(Timer());
            timerText.text = timer.ToString();
        }
        else if(timer == 0)
        {
            //skoru kontrol et ve oyunu bitir
            if(skor >= hedefSayi)
            {
                //kazandın
                winFailText.text = "Kazandin!!";
                winFailText.color = Color.green;

                //ikinci sahneye geçiş
                 yield return new WaitForSeconds(2);
                SceneManager.LoadScene("SampleScene2");
            }
            else if(skor < hedefSayi)
            {
                //kaybettin
                winFailText.text = "Kaybettin!!";
                winFailText.color = Color.red;
            }
            yield return new WaitForSeconds(2);
            endPanel.SetActive(true);
            //Time.timeScale = 0;
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Basketball")
        {            
            if(other.GetComponent<BasketCheck>().alt == true &&
                other.GetComponent<BasketCheck>().ust == true)
            {
                skor++;
                scoreText.text = skor.ToString();
                scoreUI.text = "Amazing!!";
                scoreText.color = Color.green;
                Invoke(nameof(BakctoBlack), 1);
                pota.position = new Vector3(Random.Range(-6.0f,-4.0f),0,Random.Range(-8.0f,-7.0f));
                if (basketSesi != null)
            {
                basketSesi.Play();
            }
            }
            
        }
    }

    void BakctoBlack()
    {
        scoreText.color = Color.black;
        scoreUI.text = "";
    }
}
