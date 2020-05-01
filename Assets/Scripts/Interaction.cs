using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace MyGame
{
    public class Interaction : MonoBehaviour
    {
        public UpdateProgressBar cooldownBar;
        public bool hasTarget = false;
        public GameObject target = null;

        void Awake()
        {
            cooldownBar = GameObject.FindWithTag("Cooldown").GetComponent<UpdateProgressBar>();
        }

        void Update()
        {
            DetectNearby();
        }

        void DetectNearby()
        {
            var center = transform.position;
            var radius = 0.5f;

            Collider[] hitColliders = Physics.OverlapSphere(center, radius)
                .Where(o => o.gameObject.tag == "ActionTarget")
                .OrderBy((o) => Vector3.SqrMagnitude(o.gameObject.transform.position - center))
                .ToArray();

            var first = hitColliders.FirstOrDefault();
            if (first != null)
            {
                hasTarget = true;
                target = first.gameObject;
                cooldownBar.offsetX = Camera.main.WorldToScreenPoint(first.transform.position).x;
                cooldownBar.offsetY = Camera.main.WorldToScreenPoint(first.transform.position).y;
                cooldownBar.gameObject.SetActive(true);
            }
            else
            {
                hasTarget = false;
                target = null;
                cooldownBar.gameObject.SetActive(false);
            }
        }
    }
}