using UnityEngine;

public class EmotionRenderer : MonoBehaviour
{
    public Transform mouth; 
    public Vector3 mouthScale = Vector3.one;
    public float frequency = 0.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Emotion renderer started");
        //mouth = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        mouth.localScale = new Vector3(Mathf.Sin(Time.time * frequency) * mouthScale.x, mouthScale.y, mouthScale.z);
    }
}
