using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.BionicVisionVR.Coding.Structs;


namespace BionicVisionVR.Coding.Resources
{
    [RequireComponent(typeof(Camera))]
    public class CameraRaycast : MonoBehaviour
    {
        [Header("Raycast Parameters")] public float maxAngle = 0.5f;
        public int xDim = 35;
        public int yDim = 35;
        public RawImage textureOut;
        public bool invert;
        public bool usePredefinedSettings;
        public LayerMask ignoreLayers;

        [Header("Debugging")] public bool debugMode;
        public float range = 1000f;
        public GameObject hitPrefab;

        [HideInInspector] public float[,] distanceMat;

        private Transform[] debugPos;
        private int lastXDim = 0;
        private int lastYDim = 0;

        void GenerateRaycast()
        {

            float maxDistance = 0f;
            float minDistance = 1e5f;

            for (int y = 0; y < yDim; y++)
            {
                for (int x = 0; x < xDim; x++)
                {
                    float xAngle = (x * 2f / xDim - 1f) * maxAngle;
                    float yAngle = (y * 2f / yDim - 1f) * maxAngle;

                    Vector3 rightAngle = (transform.forward + transform.right * yAngle).normalized;
                    Vector3 upAngle = (transform.forward + transform.up * xAngle).normalized;
                    Vector3 outAngle = rightAngle + upAngle;

                    RaycastHit hit;

                    if (debugMode) Debug.DrawRay(transform.position, outAngle * range, Color.blue);

                    if (Physics.Raycast(transform.position, outAngle, out hit, 1000f, ignoreLayers))
                    {
                        distanceMat[y, x] = hit.distance;

                        maxDistance = Mathf.Max(maxDistance, hit.distance);
                        minDistance = Mathf.Min(minDistance, hit.distance);

                        if (debugMode)
                        {
                            if (debugPos[y * xDim + x] == null)
                                debugPos[y * xDim + x] =
                                    Instantiate(hitPrefab, hit.point, Quaternion.identity).transform;
                            else
                                debugPos[y * xDim + x].position = hit.point;
                        }
                    }
                    else distanceMat[y, x] = -1f;
                }
            }

            for (int y = 0; y < yDim; y++)
            {
                for (int x = 0; x < xDim; x++)
                {
                    distanceMat[y, x] -= minDistance;
                    distanceMat[y, x] /= maxDistance - minDistance;
                    if (invert)
                    {
                        if (distanceMat[y, x] < 0f) distanceMat[y, x] = 0f;
                        else distanceMat[y, x] = 1f - distanceMat[y, x];
                    }
                    else distanceMat[y, x] = Mathf.Max(0f, distanceMat[y, x]);
                }
            }
        }

        Texture2D GenerateTexture()
        {
            Texture2D tex = new Texture2D(yDim, xDim, TextureFormat.ARGB32, false);

            for (int y = 0; y < yDim; y++)
            {
                string s = "";
                for (int x = 0; x < xDim; x++)
                {
                    float colorVal = distanceMat[y, x];

                    s += colorVal + ", ";
                    Color color = new Color(colorVal, colorVal, colorVal);

                    tex.SetPixel(y, x, color);
                }
            }

            tex.Apply();

            return tex;
        }

        void Update()
        {
            if (lastXDim != xDim || lastYDim != yDim)
            {
                if (debugPos != null)
                    foreach (Transform sphere in debugPos)
                        if (sphere != null)
                            Destroy(sphere.gameObject);
                debugPos = new Transform[yDim * xDim];
                distanceMat = new float[yDim, xDim];
            }

            GenerateRaycast();

            textureOut.texture = GenerateTexture();

            lastYDim = yDim;
            lastXDim = xDim;
        }

        void Start()
        {
            // if (usePredefinedSettings)
            // {
            //     VariableManagerScript vm = VariableManagerScript.Instance;
            //     if (vm.usePreDefinedBlock)
            //     {
            //         BlockSettings settings = BlockSettings.GetPreDefinedBlockSettings(vm.predefinedSettings);
            //         xDim = settings.xElectrodeCount;
            //         yDim = settings.yElectrodeCount;
            //     }
            //     else
            //     {
            //         xDim = vm.numberXelectrodes;
            //         yDim = vm.numberYelectrodes;
            //     }
            // }
            //
            // Debug.Log("electrodes: " + xDim + ", " + yDim);
        }
    }
}