using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIdata : MonoBehaviour
{
    public Text Bottom;
    public Text TopLeft;
    public Text Right;
    public Text VariableNamesRight;
    public Rigidbody Soyuz;
    public GameObject ISS;

    private float relativeVelocity;
    private float errorVelocity;
    private float errorDistance;
    private string status = "ЗАХВАТ  ";

    void OnEnable()
    {
        Contact.ExternalContact += textModifier;
    }

    private void Start()
    {
        StartCoroutine(Randomizer());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = (ISS.transform.position - Soyuz.position);
        Vector3 omegaY = new Vector3 (tmp.x, 0.0f, tmp.z).normalized;
        Vector3 omegaZ = new Vector3(0.0f, tmp.y, tmp.z).normalized;
        Vector3 soyuzY = new Vector3(Soyuz.transform.forward.x, 0.0f, Soyuz.transform.forward.z).normalized;
        Vector3 soyuzZ = new Vector3(0.0f, Soyuz.transform.forward.y, Soyuz.transform.forward.z).normalized;
        if (Soyuz.velocity.z > 0.0f)
            relativeVelocity = Soyuz.velocity.magnitude;
        else
            relativeVelocity = -Soyuz.velocity.magnitude;

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
        VariableNamesRight.text = "\n\n  ωX\n  ωY\n  ωZ\n\n   γ\n   ψ\n   θ\nψ ~\nθ ~\nρ  \nρ˙ \n";
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
        status = "КАСАНИЕ";
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.5f);
        status = "СЗЕПКА  ";
    }
}
