using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DamageController : MonoBehaviour
{
    public Canvas canvas;
    public GameObject dmgTxt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDmgTxt(Vector3 hitPoint, float damage)
    {
        GameObject damagerText = Instantiate(dmgTxt, hitPoint, Quaternion.identity, canvas.transform);
        damagerText.GetComponent<TMP_Text>().text = damage.ToString();
    }

    public void DestroyEvent() => Destroy(gameObject);
}
