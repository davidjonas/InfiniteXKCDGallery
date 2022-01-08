using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class TextureLoader : MonoBehaviour
{
    private Material mat;
    private Vector3 defaultScale;

    private class XKCDInfo
    {
        public int num;
        public string safe_title;
        public string title;
        public string alt;
        public string img;
        
        public static XKCDInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<XKCDInfo>(jsonString);
        }
    }
    
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        defaultScale = Vector3.zero + transform.localScale;
        UpdateImage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        StartCoroutine(GetComic($"https://xkcd.com/{Random.Range(1, 2000)}/info.0.json"));
    }

    IEnumerator GetComic(string url)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            XKCDInfo info = XKCDInfo.CreateFromJSON(request.downloadHandler.text);
            StartCoroutine(DownloadImage(info.img));
        }
    } 
    
    IEnumerator DownloadImage(string imageUrl)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            transform.localScale = Vector3.zero + defaultScale;
            mat.mainTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            float height = mat.mainTexture.height;
            float width = mat.mainTexture.width;
            float ratio = Mathf.Min(width, height) / Mathf.Max(width, height);

            if (width > height)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*ratio, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x*ratio, transform.localScale.y, transform.localScale.z);
            }
            
        }
    } 
}
