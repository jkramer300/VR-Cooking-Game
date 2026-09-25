using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using System.Runtime.CompilerServices;
using UnityEngine.XR;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] public Transform _tip;
    [SerializeField] public int _penSize = 5; //[SerializeField]
    public int health = 150;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;
    private RaycastHit _touch;

    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(Color.white, _penSize * _penSize).ToArray(); // _renderer.material.color
        _tipHeight = _tip.localScale.y;
    }

    public void ChangePenSize(int newPenSize)
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(Color.white, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;

        _penSize = newPenSize;
    }

    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                if (x < 0 || y < 0 || x + _penSize > _whiteboard.textureSize.x || y + _penSize > _whiteboard.textureSize.y) return;

                if (_touchedLastFrame)
                {
                    Debug.Log("Test3");
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.03f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;

                    _whiteboard.texture.Apply();

                    health--; // test
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
    void Update()
    {
        if(health > 0){
            Draw();
        }
        //InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        //if (rightHand.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        //{
        //    if (triggerValue > 0.1f)
        //    {
        //        Draw();
        //    }
        //}
    }
}
