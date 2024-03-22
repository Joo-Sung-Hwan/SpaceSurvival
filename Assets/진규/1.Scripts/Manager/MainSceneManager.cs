using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Shader obj;
    [SerializeField] private GameObject popUpObj;
    [SerializeField] private GameObject space;
    public Sequence sequence;
    float cur = 0;
    float max = 1;
    bool isActive = false;
    public bool isDo = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Move();
        TextAnimation();
        Spark();
        space.transform.Rotate(new Vector3(0f, 0f, 1f * Time.deltaTime * 15f));
    }

    private void Move()
    {
        trans.position = Vector3.MoveTowards(trans.position, new Vector3(0f, 1f, 0f), Time.deltaTime * 40f);
        if(trans.position.y == 1 && !isActive)
        {
            isActive = true;
            popUpObj.SetActive(isActive);
            if(popUpObj.gameObject.activeSelf)
            {
                Do();
                isDo = false;
            }
        }
    }

    private void Do()
    {
        Sequence sequence = DOTween.Sequence(); ;
        if(isDo)
        {
            sequence.Append(popUpObj.GetComponent<RectTransform>().DOSizeDelta(new Vector2(600, 300), 0.5f, true))
                .Join(popUpObj.transform.GetChild(0).GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 150), 0.5f, true))
                .Join(popUpObj.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().DOScale(1f, 0.5f))
                .Join(popUpObj.transform.GetChild(0).transform.GetChild(1).GetComponent<RectTransform>().DOScale(1f, 0.5f)).SetLink(gameObject);
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
