using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeCamera : MonoBehaviour {
    public float shakeTime;
    public Text msg;

    private Vector3 initialPosition = new Vector3(0f, 37f, -10.5f);
    private float lifetime;
    private float lowRangeX;
    private float maxRangeX;
    private float lowRangeY;
    private float maxRangeY;

    void CatchShake()
    {
        lowRangeY = initialPosition.y - 1f;
        maxRangeY = initialPosition.y + 1f;
        lowRangeX = initialPosition.x - 1f;
        maxRangeX = initialPosition.x + 1f;
        lifetime = shakeTime;
    }

	// Use this for initialization
	void Start () {
        if (shakeTime <= 0f)
            shakeTime = 0.7f;
        lifetime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (lifetime < 0f)
        {
            transform.position = initialPosition;
            lifetime = 0f;
        }

        if (lifetime > 0f && Time.timeScale != 0)
        {
            lifetime -= Time.deltaTime;
            var x_val = Random.Range(lowRangeX, maxRangeX);
            var y_val = Random.Range(lowRangeY, maxRangeY);
            transform.position = new Vector3(x_val, y_val, transform.position.z);
        }
	}

    public void Shake()
    {
        CatchShake();
    }
}
