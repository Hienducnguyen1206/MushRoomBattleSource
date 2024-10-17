
using UnityEngine;

public class FlipFlame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Vector3 baseScale;
    
    public Transform staffTransform;
    Vector3 flipScale;
    void Start()
    {
        baseScale = transform.localScale;
        flipScale = new Vector3 (baseScale.x,-baseScale.y,baseScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (staffTransform.localScale.y > 0)
        {
            gameObject.transform.localScale = baseScale;
        }
        else
        {
            gameObject.transform.localScale = flipScale;
        }
    }
}
