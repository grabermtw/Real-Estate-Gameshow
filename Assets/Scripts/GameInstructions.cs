using System.Collections.Generic;
using UnityEngine;

public class GameInstructions
{
    string[] priceGuessInstructions = new string[] {
        "The Price Guessing game is very simple! Guess the price of the house to the best of your ability!",
        "You can win up to $100,000! The closer you are to the actual value, the more money'll get!"
    };

    AnimCategory[] priceGuessAnims = new AnimCategory[] { AnimCategory.CorrectAnswer, AnimCategory.CorrectAnswer };

    string[] oneOffInstructions = new string[] {
        "This is one of my favorites, called \"One Off!\"",
        "Every digit in the price will be one integer away from its actual value, either up or down. It's up to you to figure out what direction!",
        "Press the buttons above and below each digit to create your guess!",
        "Be careful though, you have to guess the number PERFECTLY to win any money!"
    };

    AnimCategory[] oneOffAnims = new AnimCategory[] {
        AnimCategory.CorrectAnswer,
        AnimCategory.CorrectAnswer,
        AnimCategory.CorrectAnswer,
        AnimCategory.WrongAnswer
    };

    string[] bedBathInstructions = new string[] {
        "This is a fun game I like to call \"Bed Bath and Beyond!\"",
        "To play, simply click the buttons to select how many bedrooms and bathrooms are in the house!",
        "But beware, while you can increment the numbers of bedrooms and bathrooms, you cannot decrement them, so click carefully!",
        "Oh, and watch out for falling furniture. You signed a waiver, you can't sue us!"
    };

    AnimCategory[] bedBathAnims = new AnimCategory[] {
        AnimCategory.CorrectAnswer,
        AnimCategory.CorrectAnswer,
        AnimCategory.WrongAnswer,
        AnimCategory.Idle
    };
}