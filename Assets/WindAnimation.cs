using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindAnimation : MonoBehaviour
{
    public Sprite[] animatedImages;
    public Image animateImageObj;
    



    private void Update()
    {

        
         animateImageObj.sprite = animatedImages[((int)Time.time * 100) % animatedImages.Length];
        
        
            
    }

}
