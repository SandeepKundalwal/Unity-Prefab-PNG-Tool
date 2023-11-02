using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureImage : MonoBehaviour
{
    public string saveAs;
    public Camera prefabCamera;
    public RenderTexture renderTexture;

    private void Start()
    {
        StartCoroutine(CapturePrefabImage());
    }

    IEnumerator CapturePrefabImage()
    {
        // Wait for a frame to ensure the prefab is fully rendered
        yield return new WaitForEndOfFrame();

        Color originalBackgroundColor = prefabCamera.backgroundColor;
        prefabCamera.backgroundColor = new Color(0, 0, 0, 0);

        Texture2D kartTexture = new Texture2D(renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;
        kartTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        kartTexture.Apply();

        prefabCamera.backgroundColor = originalBackgroundColor;
        byte[] bytes = kartTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(saveAs + ".png", bytes);

        Debug.Log("Image saved as " + saveAs + ".png");
    }
}
