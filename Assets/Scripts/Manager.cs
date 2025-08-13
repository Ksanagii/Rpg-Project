using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance { get; private set; }
    public GameObject player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }



    

}
