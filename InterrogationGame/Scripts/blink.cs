using UnityEngine;
using System.Collections;
using Uniduino;

public class blink : MonoBehaviour {

    public Arduino arduino;

    //default pin is set to vibration
    private int current_pin = 5;

    private bool go = false;

    private string current_effect = "SlowHeartBeat";

    // Use this for initialization
    void Start()
    {
        arduino = Arduino.global;
        arduino.Log = (s) => Debug.Log("Arduino: " + s);

        //arduino.Setup(ConfigurePins);

        //ConfigurePins();
    }

    void ConfigurePins()
    {
        arduino.pinMode(current_pin, PinMode.OUTPUT);
    }
    //default change effect
    public void change_effect(bool effect, int num_falses)
    {
        if(num_falses >= 3 && current_effect != "FastHeartBeat") {
            false_heart();
        }
        else if (effect && !go)
        {
            go = true;
            StartCoroutine("EffectConstant");
        }
        else if(!effect)
        {
            choose_effect(num_falses);
            go = false;
            shut_off();
            StopCoroutine("EffectConstant");
        }
    }
    //deals with special change effects
    public void change_effect(bool effect)
    {
        if (effect)
        {
            go = true;
            current_effect = "SlowHeartBeat";
            StartCoroutine(current_effect);
        }
        else
        {
            go = false;
            shut_off();
            StopCoroutine(current_effect);
        }
    }

    IEnumerator Throb()
    {
        while (go)
        {
            arduino.digitalWrite(current_pin, Arduino.HIGH);
            yield return new WaitForSeconds(0.1f);
            arduino.digitalWrite(current_pin, Arduino.LOW);
            yield return new WaitForSeconds(0.4f);
        }

    }

    IEnumerator SlowHeartBeat()
    {
        while (go)
        {
            arduino.digitalWrite(current_pin, Arduino.HIGH);
            yield return new WaitForSeconds(0.1f);
            arduino.digitalWrite(current_pin, Arduino.LOW);
            yield return new WaitForSeconds(0.1f);
            arduino.digitalWrite(current_pin, Arduino.HIGH);
            yield return new WaitForSeconds(0.12f);
            arduino.digitalWrite(current_pin, Arduino.LOW);
            yield return new WaitForSeconds(0.33f);
        }
    }

    IEnumerator FastHeartBeat()
    {
        while (go)
        {
            arduino.digitalWrite(current_pin, Arduino.HIGH);
            yield return new WaitForSeconds(0.1f);
            arduino.digitalWrite(current_pin, Arduino.LOW);
            yield return new WaitForSeconds(0.1f);
            arduino.digitalWrite(current_pin, Arduino.HIGH);
            yield return new WaitForSeconds(0.12f);
            arduino.digitalWrite(current_pin, Arduino.LOW);
            yield return new WaitForSeconds(0.06f);
        }
    }

    IEnumerator EffectConstant()
    {
        arduino.digitalWrite(current_pin, Arduino.HIGH);
        yield return new WaitForSeconds(0);
    }

    void choose_effect(int num_falses)
    {
        //figure out current pin
        float prob = Random.Range(0, 100);
        
        //if prob > 20 the current pin is vibration, else it is temperature
        //if (prob > 20) { current_pin = 5; } else { current_pin = 3; } something broken about this line
                       
        ConfigurePins();
             
        prob = Random.Range(0, 100);
        //finds out which coroutine to start
        if(current_pin == 5)
        {
            if(prob > 80) { current_effect = "FastHeartBeat"; }
            else if(prob > 30) { current_effect = "SlowHeartBeat"; }
            else { current_effect = "Throb"; }
        }
        //else temperature
        else
        {
            StopCoroutine(current_effect);
            current_pin = 3;
            current_effect = "EffectConstant";
            ConfigurePins();
            StartCoroutine(current_effect);
        }
    }

    void false_heart()
    {
        StopCoroutine(current_effect);
        current_pin = 5;
        current_effect = "FastHeartBeat";
        ConfigurePins();
        StartCoroutine(current_effect);

        return;
    }

    void OnApplicationQuit()
    {
        shut_off();
    }

    void shut_off()
    {
        arduino.digitalWrite(current_pin, Arduino.LOW);
    }
}


