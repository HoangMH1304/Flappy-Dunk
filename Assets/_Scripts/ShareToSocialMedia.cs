using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ShareToSocialMedia : MonoBehaviour
{
    [SerializeField] private GameObject screenshotBlink;
    public void OnClick()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        screenshotBlink.SetActive(true);
        yield return new WaitForSeconds(0.01f); //0.0001
        screenshotBlink.SetActive(false);

        yield return new WaitForEndOfFrame();
        // yield return new WaitForSeconds(0.25f);
        
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        
        string filePath = Path.Combine(Application.temporaryCachePath, "shared_img.png");
        
        
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Flappy Hoang").SetText("Toi da rat tram cam voi con game nay T.T").SetUrl("https://www.facebook.com/HoangMH1304")
            .SetCallback((result, shareTarget) => Logger.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
