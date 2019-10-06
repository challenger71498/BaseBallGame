using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colors
{
    public enum ColorList
    {
        RED, YELLOW, GREEN, SKYBLUE, BLUE, PURPLE, PINK,
        DARK_RED, DARK_YELLOW, DARK_GREEN, DARK_SKYBLUE, DARK_BLUE, DARK_PURPLE, DARK_PINK,
        PRIMARY, PRIMARY_SEMIDARK, PRIMARY_DARK
    };
    
    public static Color red = Color.HSVToRGB(0, 0.7f, 1f);
    public static Color yellow = Color.HSVToRGB(0.15f, 0.7f, 1f);
    public static Color green = Color.HSVToRGB(0.3f, 0.7f, 0.95f);
    public static Color skyblue = Color.HSVToRGB(0.5f, 0.7f, 1f);
    public static Color blue = Color.HSVToRGB(0.6f, 0.7f, 1f);
    public static Color purple = Color.HSVToRGB(0.75f, 0.7f, 1f);
    public static Color pink = Color.HSVToRGB(0.9f, 0.7f, 1f);
    public static Color grey = Color.HSVToRGB(0f, 0f, 0.85f);

    public static Color redDark = Color.HSVToRGB(0, 0.7f, 0.8f);
    public static Color yellowDark = Color.HSVToRGB(0.15f, 0.7f, 0.8f);
    public static Color greenDark = Color.HSVToRGB(0.3f, 0.7f, 0.7f);
    public static Color skyblueDark = Color.HSVToRGB(0.5f, 0.7f, 0.7f);
    public static Color blueDark = Color.HSVToRGB(0.6f, 0.7f, 0.8f);
    public static Color purpleDark = Color.HSVToRGB(0.75f, 0.7f, 0.8f);
    public static Color pinkDark = Color.HSVToRGB(0.9f, 0.7f, 0.8f);
    public static Color greyDark = Color.HSVToRGB(0f, 0f, 0.7f);

    public static Color redDarker = Color.HSVToRGB(0, 0.7f, 0.6f);
    public static Color yellowDarker = Color.HSVToRGB(0.15f, 0.7f, 0.6f);
    public static Color greenDarker = Color.HSVToRGB(0.3f, 0.7f, 0.5f);
    public static Color skyblueDarker = Color.HSVToRGB(0.5f, 0.7f, 0.5f);
    public static Color blueDarker = Color.HSVToRGB(0.6f, 0.7f, 0.6f);
    public static Color purpleDarker = Color.HSVToRGB(0.75f, 0.7f, 0.6f);
    public static Color pinkDarker = Color.HSVToRGB(0.9f, 0.7f, 0.6f);
    public static Color greyDarker = Color.HSVToRGB(0f, 0f, 0.5f);

    public static Color redDarkest = Color.HSVToRGB(0, 0.7f, 0.4f);
    public static Color yellowDarkest = Color.HSVToRGB(0.15f, 0.7f, 0.4f);
    public static Color greenDarkest = Color.HSVToRGB(0.3f, 0.7f, 0.3f);
    public static Color skyblueDarkest = Color.HSVToRGB(0.5f, 0.7f, 0.3f);
    public static Color blueDarkest = Color.HSVToRGB(0.6f, 0.7f, 0.4f);
    public static Color purpleDarkest = Color.HSVToRGB(0.75f, 0.7f, 0.4f);
    public static Color pinkDarkest = Color.HSVToRGB(0.9f, 0.7f, 0.4f);
    public static Color greyDarkest = Color.HSVToRGB(0f, 0f, 0.3f);

    public static Color primary = Color.HSVToRGB(216 / 360f, 0.5f, 0.3f);
    public static Color primarySemiDark = Color.HSVToRGB(216/360f, 0.5f, 0.2f);
    public static Color primaryDark = Color.HSVToRGB(216 / 360f, 0.5f, 0.1f);

    //A color array.
    public static Color[] colors =
    {
        red, yellow, green, skyblue, blue, purple, pink,
        redDark, yellowDark, greenDark, skyblueDark, blueDark, purpleDark, pinkDark,
        redDarker, yellowDarker, greenDarker, skyblueDarker, blueDarker, purpleDarker, pinkDarker,
        redDarkest, yellowDarkest, greenDarkest, skyblueDarkest, blueDarkest, purpleDarkest, pinkDarkest,
        primary, primarySemiDark, primaryDark
    };
}
