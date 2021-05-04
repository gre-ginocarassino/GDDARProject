using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTexture : MonoBehaviour
{

    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
            meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Color color = tex.GetPixel(Mathf.FloorToInt(pixelUV.x * pixelUV.x), Mathf.FloorToInt(pixelUV.y * pixelUV.y));

        Debug.Log("Colour is" +" "+ color);
        Debug.Log(hit.textureCoord);
    }
}