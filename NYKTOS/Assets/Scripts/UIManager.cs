using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textoCrystalA;

    [SerializeField]
    private TMP_Text _textoCrystalM;

    [SerializeField]
    private TMP_Text _textoCrystalC;

    [SerializeField]
    private Image _weaponPalo;

    [SerializeField]
    private Image _weaponCetro;

    [SerializeField]
    private Image[] _hearts;

    [SerializeField]
    private Sprite _fullHeart;

    [SerializeField]
    private Sprite _halfHeart; 

    public int health;



    // Start is called before the first frame update
    void Start()
    {
        _weaponPalo.enabled = true;
        _weaponCetro.enabled = false;
    }

    public void CrystalA(int _numCrysA)
    {
        _textoCrystalA.text = _numCrysA.ToString();
    }
    public void CrystalM(int _numCrysM)
    {
        _textoCrystalM.text = _numCrysM.ToString();
    }

    public void CrystalC(int _numCrysC)
    {
        _textoCrystalC.text = _numCrysC.ToString();
    }

    public void MejoraArma()
    {
        _weaponPalo.enabled = false;
        _weaponCetro.enabled = true;
    }

    //public void Hearts(int health)
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < health/2 || (i <health/2+1 && health % 2 != 0))
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }

            _hearts[i].sprite = _fullHeart ;
        }

        if (health % 2 != 0)
        {
            _hearts[health/ 2].sprite = _halfHeart;
        }
    }

}