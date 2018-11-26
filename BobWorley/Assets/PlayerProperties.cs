using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class PlayerProperties : ScriptableObject
{    
    private static int initialLife = 5;
    private static int vidas = initialLife;
    private static int food = 0;
    public static UnityEvent collideMushroom = new UnityEvent();
    public static UnityEvent collideEnemy = new UnityEvent();
    public static UnityEvent dead = new UnityEvent();
    public static UnityEvent loseLifeBlink = new UnityEvent();
    public static UnityEvent gainLifeBlink = new UnityEvent();
    public static UnityEvent eatFood = new UnityEvent();
    public static UnityEvent jump = new UnityEvent();
    public static UnityEvent win = new UnityEvent();

    public static void removeLife()
    {
        vidas--;
        resetFood();
        if (vidas == 0)
        {
            dead.Invoke();
            vidas = initialLife;
        }
    }

    public static void addLife()
    {
        vidas++;
    }

    public static int getTotalLife()
    {
        return vidas;
    }

    public static int getTotalFood()
    {
        return food;
    }

    public static void addFood()
    {
        food++;
        if (food == 5)
        {
            addLife();
            gainLifeBlink.Invoke();
            resetFood();
        }
    }

    public static void resetFood()
    {
        food = 0;
    }
}