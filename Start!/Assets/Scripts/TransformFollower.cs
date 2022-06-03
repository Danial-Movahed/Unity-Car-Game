using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private void Update()
    {
        // if(target.position.x > 810f)
            transform.LookAt(target);
    }

}