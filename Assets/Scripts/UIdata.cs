﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIdata : MonoBehaviour
{
    public Text Bottom;
    public Text TopLeft;
    public Text Right;
    private Text Center;
    public Text VariableNamesRight;
    public TMPro.TextMeshProUGUI button2;
    public TMPro.TextMeshProUGUI button3;
    public TMPro.TextMeshProUGUI button4;
    public TMPro.TextMeshProUGUI button5;
    public TMPro.TextMeshProUGUI button6;
    public TMPro.TextMeshProUGUI button7;
    public TMPro.TextMeshProUGUI button8;
    public TMPro.TextMeshProUGUI button9;
    public Rigidbody Soyuz;
    public GameObject ISS;
    public string language = "English";
    public string status = "SEEKING";
    public bool UIenable = true;

    private float relativeVelocity;
    private float errorVelocity;
    private float errorDistance;
    private RandomFailures randScript;

    void OnEnable()
    {
        Contact.ExternalContact += textModifier;
        button2.text = "LANG";
        button3.text = "UI";
        button4.text = "ENGINES\nRESTART";
        button5.text = "RESET\nCOMMS";
        button6.text = "RESET\nVS";
        button7.text = "TPV";
        button8.text = "LESS\nPOWER";
        button9.text = "MORE\nPOWER";
        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            UIenable = true;
            randScript = GameObject.Find("Soyuz").GetComponent<RandomFailures>();
            Center = GameObject.Find("UICenter").GetComponent<Text>();
        }   
        else
            UIenable = false;
    }

    private void Start()
    {
        StartCoroutine(Randomizer());
        Soyuz = GameObject.Find("Soyuz").GetComponent<Rigidbody>();
        ISS = GameObject.Find("ZvezdaExternalCollider");
    }

    // Update is called once per frame
    void Update()
    {
        if (UIenable) {
            Vector3 tmp = (ISS.transform.position - Soyuz.position);
            Vector3 omegaY = new Vector3(tmp.x, 0.0f, tmp.z).normalized;
            Vector3 omegaZ = new Vector3(0.0f, tmp.y, tmp.z).normalized;
            Vector3 soyuzY = new Vector3(Soyuz.transform.forward.x, 0.0f, Soyuz.transform.forward.z).normalized;
            Vector3 soyuzZ = new Vector3(0.0f, Soyuz.transform.forward.y, Soyuz.transform.forward.z).normalized;
            if (Soyuz.velocity.z > 0.0f)
                relativeVelocity = Soyuz.velocity.magnitude;
            else
                relativeVelocity = -Soyuz.velocity.magnitude;

            
            if (language == "English")
            {
                TopLeft.text = "Phase 44 APPROACH\n" +
                           "        FLIGHT " + status + "      LSK\n" +
                           "B12\n" +
                           "DUS 123   1\n" +
                           "R 181.5\n" +
                           "S1.71001";
                Bottom.text = "Ф    ρ        " + (0.001 * (Soyuz.transform.position - ISS.transform.position).magnitude).ToString("F3") +
                    " KM        ΩY " + Vector3.Angle(soyuzY, omegaY).ToString("F3") + "    " +
                    (Vector3.Angle(soyuzY, omegaY) + errorDistance).ToString("F3") + "\n" +
                    "       ρ˙      " + relativeVelocity.ToString("F2") +
                    " М/S         ΩZ " + Vector3.Angle(soyuzZ, omegaZ).ToString("F3") + "    " +
                    (Vector3.Angle(soyuzZ, omegaZ) + errorDistance).ToString("F3") + "\n";
                Right.text = "T = " + System.DateTime.Now.ToString("hh:mm:ss") + "\n" +
                    "GSO  1234 " + "\n" +
                    Soyuz.angularVelocity.x.ToString("F3") + "\n" +
                    Soyuz.angularVelocity.y.ToString("F3") + "\n" +
                    Soyuz.angularVelocity.z.ToString("F3") + "\n" +
                    "KURS 1\n" +
                    Soyuz.rotation.z.ToString("F2") + "\n" +
                    Soyuz.rotation.x.ToString("F2") + "\n" +
                    Soyuz.rotation.y.ToString("F2") + "\n" +
                    (ISS.transform.position.x - Soyuz.transform.position.x).ToString("F2") + "\n" +
                    (ISS.transform.position.y - Soyuz.transform.position.y).ToString("F2") + "\n" +
                    (0.001 * ((Soyuz.transform.position - ISS.transform.position).magnitude + errorDistance)).ToString("F3") + "\n" +
                    (relativeVelocity + errorVelocity).ToString("F2") + "  \n";

            }
            else if (language == "Russian")
            {
                TopLeft.text = "Ф44 СБЛИЖЕНИЕ\n" +
                           "        ОБЛЕТ " + status + "      ЛСК\n" +
                           "Б12\n" +
                           "ДУС 123   1\n" +
                           "Р 181.5\n" +
                           "С1.71001";
                Bottom.text = "Ф    ρ        " + (0.001 * (Soyuz.transform.position - ISS.transform.position).magnitude).ToString("F3") +
                    " KM        ΩY " + Vector3.Angle(soyuzY, omegaY).ToString("F3") + "    " +
                    (Vector3.Angle(soyuzY, omegaY) + errorDistance).ToString("F3") + "\n" +
                    "       ρ˙      " + relativeVelocity.ToString("F2") +
                    " М/С         ΩZ " + Vector3.Angle(soyuzZ, omegaZ).ToString("F3") + "    " +
                    (Vector3.Angle(soyuzZ, omegaZ) + errorDistance).ToString("F3") + "\n";
                Right.text = "T = " + System.DateTime.Now.ToString("hh:mm:ss") + "\n" +
                    "ГСО  1234 " + "\n" +
                    Soyuz.angularVelocity.x.ToString("F3") + "\n" +
                    Soyuz.angularVelocity.y.ToString("F3") + "\n" +
                    Soyuz.angularVelocity.z.ToString("F3") + "\n" +
                    "КУРС 1\n" +
                    Soyuz.rotation.z.ToString("F2") + "\n" +
                    Soyuz.rotation.x.ToString("F2") + "\n" +
                    Soyuz.rotation.y.ToString("F2") + "\n" +
                    (ISS.transform.position.x - Soyuz.transform.position.x).ToString("F2") + "\n" +
                    (ISS.transform.position.y - Soyuz.transform.position.y).ToString("F2") + "\n" +
                    (0.001 * ((Soyuz.transform.position - ISS.transform.position).magnitude + errorDistance)).ToString("F3") + "\n" +
                    (relativeVelocity + errorVelocity).ToString("F2") + "  \n";
            }

            VariableNamesRight.text = "\n\n  ωX\n  ωY\n  ωZ\n\n   γ\n   ψ\n   θ\nψ ~\nθ ~\nρ  \nρ˙ \n";
        }
        else
        {
            Bottom.text = "";
            TopLeft.text = "";
            Right.text = "";
            VariableNamesRight.text = "";
        }

        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            switch (randScript.failureType)
            {
                case 1:
                    if(language == "English")
                        Center.text = "MAIN ENGINE FAILURE\nRESTART ENGINES";
                    else if (language == "Russian")
                        Center.text = "ОТКАЗ ГЛАВНОГО ДВИГАТЕЛЯ\nПЕРЕЗАГРУЗИТЕ ДВИГАТЕЛИ";
                    break;
                case 2:
                    if (language == "English")
                        Center.text = "CAMERA FAILURE\nDRIVER REBOOTING PENDING";
                    else if (language == "Russian")
                        Center.text = "ОТКАЗ КАМЕРЫ\nОЖИДАЕТ ПЕРЕЗАГРУЗКУ ДРАЙВЕРА";
                    break;
                case 3:
                    if (language == "English")
                        Center.text = "VITAL SUPPORT FAILURE\nOXYGEN DEPLETING IN 30 S";
                    else if (language == "Russian")
                        Center.text = "ОТКАЗ ЖИВОЙ ПОДДЕРЖКИ\nУДАЛЕНИЕ КИСЛОРОДА ЗА 30 С";
                    break;
                case 4:
                    if (language == "English")
                        Center.text = "COMMUNICATIONS FAILURE\nRESET CHANNEL IMMEDIATELY";
                    else if (language == "Russian")
                        Center.text = "СБОЙ СВЯЗИ\nСБРОСИТЕ КАНАЛ НЕМЕДЛЕННО";

                    Bottom.text = "Ф    ρ        " + "---" +
                        " KM        ΩY " + "---" + "    " +
                        "---" + "\n" +
                        "       ρ˙      " + "---" +
                        " М/S         ΩZ " + "---" + "    " +
                        "---" + "\n";
                    Right.text = "T = " + System.DateTime.Now.ToString("hh:mm:ss") + "\n" +
                        "GSO  1234 " + "\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "KURS 1\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "---" + "\n" +
                        "---" + "  \n";
                    VariableNamesRight.text = "\n\n  ωX\n  ωY\n  ωZ\n\n   γ\n   ψ\n   θ\nψ ~\nθ ~\nρ  \nρ˙ \n";
                    break;
                default:
                    Center.text = "";
                    break;
            }
        }
    }

    IEnumerator Randomizer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            errorVelocity = Random.Range(-0.02f, 0.02f);
            errorDistance = Random.Range(-2, 3);
        }
    }

    void textModifier()
    {
        if (language == "Russian")
            status = "КАСАНИЕ";
        else if (language == "English")
            status = "CONTACT";
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.5f);
        if (language == "Russian")
            status = "СЦЕПКА  ";
        else if (language == "English")
            status = "COUPLED";
        
    }
}
