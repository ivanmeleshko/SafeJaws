using Managers;
using Model;
using System.Collections;
using System.Collections.Generic;
using Scripts.UITools;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsEvent : MonoBehaviour
{
    [SerializeField] private Button[] _colorModel;
    // Start is called before the first frame update
    void Start()
    {
       // _colorModel = GetComponent<ColorModelButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Left")
                {
                    var mesh = hit.collider.transform.parent.GetComponentsInChildren<cakeslice.Outline>();
                    foreach (var item in mesh)
                    {
                        item.enabled = false;
                    }
                    hit.collider.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    AlphaTween.OnTweenColorAction?.Invoke();
                    _colorModel[0].GetComponent<ColorModelButton>().OnClick();
                    GameManager.Instance.sectionSelected = 0;
                }
                else if (hit.collider.name == "Right")
                {
                    var mesh = hit.collider.transform.parent.GetComponentsInChildren<cakeslice.Outline>();
                    foreach (var item in mesh)
                    {
                        item.enabled = false;
                    }
                    hit.collider.gameObject.GetComponent<cakeslice.Outline>().enabled = true; 
                    AlphaTween.OnTweenColorAction?.Invoke();
                    _colorModel[1].GetComponent<ColorModelButton>().OnClick();
                    GameManager.Instance.sectionSelected = 1;
                }
                else if (hit.collider.name == "Center")
                {
                    var mesh = hit.collider.transform.parent.GetComponentsInChildren<cakeslice.Outline>();
                    foreach (var item in mesh)
                    {
                        item.enabled = false;
                    }
                    hit.collider.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    AlphaTween.OnTweenColorAction?.Invoke();
                    _colorModel[2].GetComponent<ColorModelButton>().OnClick();
                    GameManager.Instance.sectionSelected = 2;
                }

            }
        }
    }
}
