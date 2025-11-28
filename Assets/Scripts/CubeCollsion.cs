using UnityEngine;

public class CubeCollsion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject PlaneColor;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag==PlaneColor.tag)
        {
           
        }
    }

    public void WinSate()
    {
      
    }
}
