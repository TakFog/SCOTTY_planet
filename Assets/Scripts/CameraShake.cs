using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    public bool enableShake = false;
    public float power;

    public float fadeFactor = 0.99f;
    public float remainingPower;
	
	// Update is called once per frame
	void Update () {
        if (!enableShake) return;

        remainingPower *= fadeFactor;
        if (remainingPower < 0.01f)
        {
            enableShake = false;
            transform.localPosition = new Vector3(0, 0, transform.position.z);
            return;
        }
        
        Vector2 shake = Random.insideUnitCircle* remainingPower;
        transform.localPosition = new Vector3(shake.x, shake.y, transform.position.z);
	}

    public void StartShake()
    {
        remainingPower = power;
        enableShake = true;
    }
    
}
