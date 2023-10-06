using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave2 : MonoBehaviour
{

    public static wave2 Instance;
    public MeshRenderer meshRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
