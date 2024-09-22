using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //constantes de fuerzas para transform y rigidBody
    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    //variables para el efecto de espacio infinito
    public float xBorderLimit;
    public float yBorderLimit;

    //pistola y prefab de balas a instanciar
    public GameObject gun, bulletPrefab;

    //permite movimiento por fuerzas
    private Rigidbody _rigid;

    //puntos del jugador (usado por Bullet)
    public static int SCORE = 0;

    // Start is called before the first frame update
    void Start()
    {
        //inicializa rigidBody y calcula los bordes de la pantalla
         _rigid = GetComponent<Rigidbody>();
        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        //calcula movimiento
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;
        
        //movimiento
        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        //si presionas la Q la nave frena
        if(Input.GetKey(KeyCode.Q)){
            _rigid.AddForce(-_rigid.velocity * 0.9f);
        }

        //codigo para espacio infinito
        var newPos = transform.position;
        if(newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit+1;
        else if(newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit-1;
        else if(newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit+1;
        else if(newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit-1;
        transform.position = newPos;

        //si das al espacio, dispara
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //instancia bala y le da direccion (donde apunta la pistola en la punta de la nave)
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balascript = bullet.GetComponent<Bullet>();
            balascript.targetVector = transform.right;
        }
    }
    //si colisiona con un meteorito o fragmento, resetea escena y score
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
