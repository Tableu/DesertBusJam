using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideInstructions : MonoBehaviour
{
    public Text tutorial;
    public GameObject barrel;
    // Start is called before the first frame update
    void Start()
    {
        if (MapManager.Instance.BarrelJoustDifficulty != 0 && MapManager.Instance.TugOfWarDifficulty != 0)
        {
            tutorial.gameObject.SetActive(false);
            barrel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
