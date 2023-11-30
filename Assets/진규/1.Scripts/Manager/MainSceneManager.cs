using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Shader obj;
    [SerializeField] private GameObject popUpObj;
    float cur = 0;
    float max = 1;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TextAnimation();
        Spark();
    }

    private void Move()
    {
        trans.position = Vector3.MoveTowards(trans.position, new Vector3(0f, 1f, 0f), Time.deltaTime * 15f);
        if(trans.position.y == 1 && !isActive)
        {
            popUpObj.SetActive(true);
        }
    }

    public void ButtonClose()
    {
        isActive = true;
        popUpObj.SetActive(false);
    }

    public void ButtonStart()
    {

    }

    void TextAnimation()
    {
        title.ForceMeshUpdate();
        var textInfo = title.textInfo;
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            title.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    void Spark()
    {
        cur += Time.deltaTime * 0.5f;
        float dividcur = Mathf.PingPong(cur, max);
        title.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, dividcur);
    }
}
