using System;
using BionicVisionVR.Coding.Resources;
using BionicVisionVR.Resources;
// using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = System.Random;

namespace Experiments.GUI_Demo
{
    public class VideoPlayerHandler : MonoBehaviour
    {
        //[SerializeField] private GameObject intro1;

        [SerializeField] private string videoToPlay;

        //private bool intro; 


        public void Start()
        {
           // intro = true; 
            //intro1.SetActive(true);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) )
            {

                VideoPlayer videoPlayer = gameObject.GetComponent<VideoPlayer>();
                //videoPlayer.url = videoToPlay;
                videoPlayer.Play();
            }
            // else if (Input.GetKeyDown(KeyCode.Return))
            // {
            //     intro1.SetActive(false);
            //     intro = false; 
            //     videoToPlay = EditorUtility.OpenFilePanel("Select Directory", "", "");
            // }
        }
    }
}