using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CheckingMarker : MonoBehaviour
{
     public GameObject Mascot,NoMascot, isTakeCamera;
     public GameObject screenshotPreview;
    //  public Camera MCamera;

     string imageFilePath;
    // Start is called before the first frame update
    void Start()
    {
        DefaultTrackableEventHandler.isMarker = false;
    }

    //  public int resWidth = 2550; 
    //  public int resHeight = 3300;
 
    //  private bool takeHiResShot = false;
 
    //  public static string ScreenShotName(int width, int height) {
    //      return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
    //                           Application.dataPath, 
    //                           width, height, 
    //                           System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    //  }

    // Update is called once per frame
    void Update()
    {
        if( DefaultTrackableEventHandler.isMarker == false){
            Mascot.SetActive(false);
            NoMascot.SetActive(true);
        }else{
            Mascot.SetActive(true);
            NoMascot.SetActive(false);
        }
    }

    public void CameraTake(){
        if( DefaultTrackableEventHandler.isMarker == true){
            
        }
       StartCoroutine(DelayCamera());
    }

     IEnumerator DelayCamera()
    {
        isTakeCamera.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture2D.Apply();
        imageFilePath = Application.persistentDataPath + "/" + DateTime.Now.ToFileTime() + ".jpg";
        Debug.Log(imageFilePath);

        File.WriteAllBytes(imageFilePath, texture2D.EncodeToPNG());
        Destroy(texture2D);
        texture2D = null;
        Resources.UnloadUnusedAssets();
        GC.Collect();
        BlankGalleryScreenshot.Instance.AddImageToGallery(imageFilePath);
        
        isTakeCamera.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        isTakeCamera.SetActive(true);
    }

    //  IEnumerator DelayCamera()
    // {
    //     string strDate = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    //     ScreenCapture.CaptureScreenshot("Screenshot"+strDate+".png");

        
    //     isTakeCamera.SetActive(false);
    //     yield return new WaitForSeconds(1f);
    //     isTakeCamera.SetActive(true);
    // }

    // IEnumerator captureScreenshot()
    // {
    //     string imageName = "screenshot.png";

    //     // Take the screenshot
    //     ScreenCapture.CaptureScreenshot(imageName);

    //     //Wait for 4 frames
    //     for (int i = 0; i < 5; i++)
    //     {
    //         yield return null;
    //     }

    //     // Read the data from the file
    //     byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/" + imageName);

    //     // Create the texture
    //     Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height);

    //     // Load the image
    //     screenshotTexture.LoadImage(data);

    //     // Create a sprite
    //     Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));

    //     // Set the sprite to the screenshotPreview
    //     screenshotPreview.GetComponent<Image>().sprite = screenshotSprite;
    //     print("ScreenShot");
    // }

    // IEnumerator captureScreenshot2()
    // {
    //     yield return new WaitForEndOfFrame();
    //     string strDate = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    //     string strName = strDate+".png";
    //     string path = Application.persistentDataPath + "Screenshots/"
    //             + "_" + strDate + "_" + Screen.width + "X" + Screen.height + "" + ".png";

    //     Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
    //     //Get Image from screen
    //     screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //     screenImage.Apply();
    //     //Convert to png
    //     byte[] imageBytes = screenImage.EncodeToPNG();

    //     //Save image to file
    //     System.IO.File.WriteAllBytes(path, imageBytes);
    // }
}
