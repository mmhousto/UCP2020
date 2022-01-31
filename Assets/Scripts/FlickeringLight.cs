using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MorganHouston.UCP2020
{
    public class FlickeringLight : MonoBehaviour
    {

        Material ceilingLightMat;
        Light ceilingLight;
        Color ceilingColor;

        // Start is called before the first frame update
        void Start()
        {
            ceilingLightMat = GetComponent<MeshRenderer>().material;
            ceilingLight = GetComponentInChildren<Light>();
            ceilingColor = ceilingLightMat.GetColor("_EmissionColor");
            float random1 = Random.Range(0.0f, 1.0f);
            float random2 = Random.Range(0.0f, 1.0f);
            float random3 = Random.Range(0.0f, 1.0f);
            Vector4 color1 = new Vector4(ceilingColor.r, ceilingColor.g, ceilingColor.b, random1);
            Vector4 color2 = new Vector4(ceilingColor.r, ceilingColor.g, ceilingColor.b, random2);
            Vector4 color3 = new Vector4(ceilingColor.r, ceilingColor.g, ceilingColor.b, random3);
        }
        
        // Update is called once per frame
        void Update()
        {
            Flicker();
        }

        void Flicker()
        {
            float random = Random.Range(0.0f, 1.0f);
            Vector4 color = new Vector4(ceilingColor.r, ceilingColor.g, ceilingColor.b, random);
            ceilingLight.intensity = random * 3f;
            ceilingLightMat.SetColor("_EmissionColor", color);
        }

        IEnumerable Flick()
        {
            for(int i = 0; i < 4; )
            yield return new WaitForSeconds(0.25f);
        }
    }
}
