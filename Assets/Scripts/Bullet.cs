using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    //velocidad y tiempo de vida de la bala
    public float speed = 10f;
    public float maxLifeTime = 3f;
    //direccion de la bala
    public Vector3 targetVector;
    //Prefab del fragmento a instanciar cuando destruyes meteorito
    public GameObject fragmentPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //Destruye despues del tiempo especificado
        Destroy(gameObject, maxLifeTime);        
    }

    // Update is called once per frame
    void Update()
    {
        //movimiento de la bala
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    //OnTrigger en vez de OnCollision para evitar "retroceso" en la nave
    private void OnTriggerEnter(Collider collision)
    {
        //si da a un meteorito
        if(collision.gameObject.tag == "Enemy")
        {
            //aumenta el score y destruye tanto la bala como el meteorito
            IncreaseScore();
            Destroy(gameObject);
            Vector3 spawnPosition = collision.transform.position;
            Vector3 diag = new Vector3(0.5f,0,0);
            Destroy(collision.gameObject);
            
            //crear los dos fragmentos y les da direccion (targetVector var publica de Fragment)
            GameObject frag1 = Instantiate(fragmentPrefab, spawnPosition + transform.right, Quaternion.identity);
            Fragment frag1script = frag1.GetComponent<Fragment>();
            frag1script.targetVector = diag-transform.up;

            GameObject frag2 = Instantiate(fragmentPrefab, spawnPosition - transform.right, Quaternion.identity);
            Fragment frag2script = frag2.GetComponent<Fragment>();
            frag2script.targetVector = -diag-transform.up;
        }
        //si da a un fragmento
        else if(collision.gameObject.tag == "Enemy2")
        {
            IncreaseScore();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    //funcion para incrementar puntos
    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    //funcion auxiliar de IncreaseScore (actualiza el score en pantalla)
    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }
}
