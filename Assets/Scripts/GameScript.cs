using System;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
    public GameObject mainCam;
    public GameObject prefObstacle;
    public GameObject prefExplosion;
    public Text status;
    public Text gameOverMsg;
    public Text counter;
    public float obsSpd = 20f;
    public float moveSpd = 16f;
    public float damagePercentage = 25.0f;
    public int range_h = 32;
    public float range_v = 5;

    ShakeCamera shaker;
    Transform obsTrans;
    GameObject obstacle;
    GameObject explosion;
    Vector3 axis_x = new Vector3(1, 0, 0);
    Vector3 axis_y = new Vector3(0, 1, 0);
    Vector3 axis_z = new Vector3(0, 0, 1);
    bool obsExists = false;
    int obsCount = 0;
    float damage = 0f;
    float obsRange;
    float expStart;

    System.Random random = new System.Random();

    // Use this for initialization
    void Start()
    {
        shaker = mainCam.GetComponent<ShakeCamera>();
        var x = random.Next(-range_h, range_h);
        obstacle = CreateObstacle(new Vector3(x, 32, 100));
        obsTrans = obstacle.GetComponent<Transform>();
        obsExists = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!obsExists)
        {
            var x = random.Next(-range_h, range_h);
            obstacle = CreateObstacle(new Vector3(x, 32, 100));
            obsTrans = obstacle.GetComponent<Transform>();
            obsExists = true;
        }

        obsTrans.position += Vector3.back * obsSpd * Time.deltaTime;

        if (obsTrans.position.z < -64)
        {
            counter.text = String.Format("counter: {0}", ++obsCount);
            Destroy(obstacle);
            obsExists = false;
        }

        if ((expStart - Time.deltaTime) >= 0.05f)
        {
            Destroy(explosion);
        }

        //バンク
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && damage < 100) //左バンク
        {
            transform.Rotate(axis_x, -30f);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && damage < 100) //右バンク
        {
            transform.Rotate(axis_x, 30f);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.LeftArrow) && damage < 100) //左バンク回復
        {
            transform.Rotate(axis_x, 30f);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.RightArrow) && damage < 100) //右バンク回復
        {
            transform.Rotate(axis_x, -30f);
        }

        //移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //左
        {
            if (-range_h < transform.position.x)
                transform.position += Vector3.left * moveSpd * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //右
        {
            if (transform.position.x < range_h)
                transform.position += Vector3.right * moveSpd * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //前
        {
            if (transform.position.z < range_v)
                transform.position += Vector3.forward * moveSpd * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //後ろ
        {
            if (-range_v < transform.position.z)
                transform.position += Vector3.back * moveSpd * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Missile(Clone)")
        {
            shaker.Shake();
            Explode();
            damage += damagePercentage;
            status.text = damage.ToString() + "%";
            if (100 <= damage)
            {
                Time.timeScale = 0;
                gameOverMsg.text = string.Format("Game Over!\nScore: {0}!", obsCount);
            }
        }
    }

    void Explode()
    {
        var pos = transform.position;
        explosion = Instantiate(prefExplosion, pos, Quaternion.identity) as GameObject;
        expStart = Time.deltaTime;
    }

    GameObject CreateObstacle(Vector3 position)
    {
        return Instantiate(prefObstacle, position, Quaternion.identity) as GameObject;
    }
}
