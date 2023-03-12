using UnityEngine;

public class CoinScriptCD : MonoBehaviour
{
    public float RotationSpeed = 100f;
    public Vector3 rotation = new Vector3(0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * RotationSpeed * Time.deltaTime);
    }
}
