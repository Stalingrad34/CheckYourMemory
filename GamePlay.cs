using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{   
    public GameObject[] positionCard = new GameObject[18];
    public GameObject[] prefabsCard = new GameObject[9];

    public static int numberOfCards = 6;
    private GameObject[] playCards = new GameObject[numberOfCards];
    public Material[] koloda;
    public static RaycastHit2D[] selectedCards = new RaycastHit2D[2];
    private int count;
    private int pars;

    public static Text minutesTime;
    public static Text secondsTime;
    public float minutes;
    public float seconds;
    private float param;
   
    void Start()
    {
        //Рандомно определяем из общей колоды карты, которые будут префабами в дальнейшем.
        for (int i = 0; i < numberOfCards/2; i++)
        {           
            prefabsCard[i].GetComponent<SpriteRenderer>().material = koloda[Random.Range(0, koloda.Length)];                      
        }

        //Определяем игровые карты из созданных префабов.
        for (int i = 0; i < numberOfCards / 2; i++)
        {
            playCards[i*2] = prefabsCard[i];
            playCards[(i *2)+ 1] = prefabsCard[i];           
        }

        //Перемешиваем игровые карты.
        for (int i = playCards.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, (i + 1));

            var tmp = playCards[j];
            playCards[j] = playCards[i];
            playCards[i] = tmp;
        }

        //Расставляем игровые карты по позициям на игровом поле.
        for (int i = 0; i < playCards.Length; i++)
        {
            GameObject card = Instantiate(playCards[i], positionCard[i].transform.position, Quaternion.identity) as GameObject;
            card.GetComponent<Animator>().SetBool("Open", true);            
        }

        //Переворачиваем карты после показа их игроку.
        Invoke("HideCards", numberOfCards/6);

        //Игровое время
        minutesTime = GameObject.Find("Minutes").GetComponent<Text>();
        secondsTime = GameObject.Find("Seconds").GetComponent<Text>();
    }


    void Update()
    {
        //Выбор карты игроком
        if (Input.GetMouseButtonDown(0) && count < 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit.collider.gameObject.name != "Background"&& hit.collider.GetComponent<Animator>().GetBool("Open") == false)
            {
                hit.collider.GetComponent<Animator>().SetBool("Open", true);
                selectedCards[count] = hit;
                count++;                            
            }
        }       

        //После выбора двух карт делаем проверку.
        if (count == 2)
        {
            Invoke("Check", 1);
        }

        //Если открыты все пары:
        if (pars == numberOfCards/2)
        {           
            SceneManager.LoadScene("GameOver");
        }

        //Игровое время
        param -= Time.deltaTime;
        if (param <= 0)
        {
            param = 1;
            seconds++;
        }

        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }

        minutesTime.text = "" + minutes;
        secondsTime.text = "" + seconds;       
    }

    void Check()
    {
        if (selectedCards[0].collider.GetComponent<SpriteRenderer>().material.name == selectedCards[1].collider.GetComponent<SpriteRenderer>().material.name)
        {
            pars++;           
            count = 0;
            selectedCards = new RaycastHit2D[2];
            

        }
        else
        {
            
            selectedCards[0].collider.GetComponent<Animator>().SetBool("Open", false);
            selectedCards[1].collider.GetComponent<Animator>().SetBool("Open", false);
            count = 0;
            selectedCards = new RaycastHit2D[2];

        }
    }
    void HideCards()
    {
        GameObject[] openCards = GameObject.FindGameObjectsWithTag("PlayCard");
        //foreach (var item in openCards)
        //{
        //    item.GetComponent<Animator>().SetBool("Open", false);
        //}
        for (int i = 0; i < openCards.Length; i++)
        {
            openCards[i].GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
