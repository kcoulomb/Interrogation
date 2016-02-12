using UnityEngine;
using System.Collections;
using Uniduino;

public class blink : MonoBehaviour {

    public Arduino arduino;

    public int current_pin = 5;


    // Use this for initialization
    void Start()
    {

        arduino = Arduino.global;
        arduino.Log = (s) => Debug.Log("Arduino: " + s);

        arduino.Setup(ConfigurePins);

        ConfigurePins();
    }

    void ConfigurePins()
    {
        Debug.Log("set pin mode");
        arduino.pinMode(current_pin, PinMode.OUTPUT);
    }

    public void change_effect(bool effect, int num_falses)
    {
        //if the LED should blink repeatedly, startcoroutine BlinkLoop(bool = true)
        //otherwise stop coroutine BlinkLoop
        if (effect)
        {
            StartCoroutine("EffectConstant");
        }
        else
        {
            if (num_falses == 0)
            {
                if (current_pin == 3)
                {
                    switch_pin(5);
                }
                else
                {
                    switch_pin(3);
                }
            }
            //StopCoroutine("EffectConstant");
            //turns off LED if caught in on position
            //arduino.digitalWrite(current_pin, Arduino.LOW);
        }

    }

    //blinks LED light once
    IEnumerator BlinkButtonDelay()
    {
        arduino.digitalWrite(current_pin, Arduino.HIGH);
        yield return new WaitForSeconds(1);
        arduino.digitalWrite(current_pin, Arduino.LOW);
    }

    IEnumerator EffectConstant()
    {
        arduino.digitalWrite(current_pin, Arduino.HIGH);
        yield return new WaitForSeconds(0);
    }

    //blinks LED light repeatedly
    IEnumerator BlinkLoop(bool go)
    {
        while (go)
        {  
            arduino.digitalWrite(current_pin, Arduino.HIGH); // led ON			
            yield return new WaitForSeconds(1);
            arduino.digitalWrite(current_pin, Arduino.LOW); // led OFF
            yield return new WaitForSeconds(1);
        }
    }


    //Switches current Arduino pin used so that the
    //vest effect will change. 
    void switch_pin(int pin) {
        StopCoroutine("EffectConstant");
        arduino.digitalWrite(current_pin, Arduino.LOW);
        current_pin = pin;
        ConfigurePins();
    }
}


