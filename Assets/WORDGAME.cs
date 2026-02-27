using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class Words : MonoBehaviour

{
string correctWord = "";

string lettersGuessed = "";

int attemptsRemaining = 3;

//Empty Strings to initialize the variables to be used

public TextAsset wordList;
ArrayList words;
List<string> guessedList = new List<string>();
[SerializeField] TextMeshProUGUI wordTextUI;
[SerializeField] TextMeshProUGUI attemptsTextUI;
[SerializeField] TextMeshProUGUI lettersGuessedTextUI;
[SerializeField] TMP_InputField guessInputField;
[SerializeField] Button submitButton;
//SerializeFields that allow us to link UI elements to the script through the inspector




    void Start()
    {

        LoadWords();
        ChooseWord();
        
    }
    //Start will load the words from the file and choose a random word to start the game

   public void LoadWords()
    {
        TextAsset wordList = Resources.Load<TextAsset>("WORDS");  // Assets/Resources/words.txt
        //Loads the text file from the resources folder
        if (wordList == null)
        {
            Debug.Log("CANNOT FIND FILE. PLAESE CHECK IF TEXTASSET wordList HAS A TEXT FILE.");
        }
        //Checks if the file is loaded correctly
        else {
            Debug.Log("WORD FILE FULLY LOADED.");
        }

        words = new ArrayList(wordList.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        Debug.Log("WORD COUNT: " + words.Count);
        //Splits the text file into individual words and stores them in an array list
        //new char[] { '\n', '\r' } is used to split the text file by new lines and carriage returns, which are common line breaks in text files
        //StringSplitOptions.RemoveEmptyEntries is used to remove any empty lines from the text file
    }


    public void ChooseWord()
    {
       
        correctWord = words[UnityEngine.Random.Range(0, words.Count)].ToString().ToUpper();
        Debug.Log("CORRECT WORD: " + correctWord);
        // Choose a random word from the list and convert it to uppercase for consistency

        wordTextUI.text = string.Join (" ", new string('_', correctWord.Length).ToCharArray());
        //wordTextUI is updated to show underscores for each letter in the correct word, with spaces in between for readability
        //ToCharArray() is used to convert the string of underscores into a character array, which is then joined back into a string with spaces in between

    }
    
    public void ResetGame()
    {
        ChooseWord();
        lettersGuessed = "";
        attemptsRemaining = 3;
        guessedList.Clear();
        UpdateWordText();
        attemptsTextUI.text = "Attempts Remaining: " + attemptsRemaining;
        lettersGuessedTextUI.text = "";
        submitButton.interactable = true;
    }
    //Resets the game by choosing a new word, clearing the guessed letters, resetting attempts, and updating the UI accordingly.
    
    public void SubmitGuess()
    {
        UpdateWordText();
        if(attemptsRemaining == 0)
            {
                attemptsTextUI.text = "Game Over! The correct word was: " + correctWord;
                submitButton.interactable = false;
                
            }
            //Checks if the player has run out of attempts and updates the UI to show the game over message and the correct word, 
            //and disables the submit button to prevent further guesses.
            else if (!wordTextUI.text.Contains("_"))
            {
                attemptsTextUI.text = "Congratulations! You won!\nDo you want to play again?";
                submitButton.interactable = false;
            }
            //Checks if the player has guessed the word correctly by checking if there are no underscores left in the wordTextUI
            
    }

   public void UpdateWordText()
    {
        string guess = guessInputField.text.ToUpper();
        //Takes the player's guess from the input field and converts it to uppercase for consistency

            guess = guessInputField.text.ToUpper();
            //guess = guessInputField.text.ToUpper(); is used to ensure that the player's guess is always in uppercase, 
            //which allows for consistent comparison with the correct word and guessed letters, regardless of how the player enters their guess (e.g., lowercase, mixed case).
            if (!guessedList.Contains(guess))
            {
                guessedList.Add(guess);
                lettersGuessedTextUI.text = string.Join("\n", guessedList);
            }
            //Checks if the player's guess has already been made by checking if it is in the guessedList. 
            //If it is not, it adds the guess to the guessedList and updates the lettersGuessedTextUI to show all guessed letters.

        if(!lettersGuessed.Contains(guess))
        {
            lettersGuessed += guess;
            //lettersGuessed += guess is used to add the player's guess to the string of lettersGuessed
            if(!correctWord.Contains(guess))
            {
                attemptsRemaining--;
                attemptsTextUI.text = "Attempts Remaining: " + attemptsRemaining;
            }
            //Checks if the guessed letter is not in the correct word. If it is not,
            //it decrements the attempts remaining and updates the attemptsTextUI to show the new number of attempts remaining.
        }
            
            char[] display = new char[correctWord.Length];
            //display is a character array that will be used to build the string that shows the correctly guessed letters and underscores for the
            //remaining letters in the correct word. It is initialized to the same length as the correct word.
            for (int i = 0; i < correctWord.Length; i++)
            //This loop iterates through each letter in the correct word and checks if it has been guessed by the player.
            //If the letter has been guessed, it is added to the display array, if not, an underscore is added to represent the unguessed letter.
            {
                if(lettersGuessed.Contains(correctWord[i].ToString()))
                {
                    display[i] = correctWord[i];
                }
                //Checks if the current letter in the correct word has been guessed by checking if it is in the lettersGuessed string.
                else
                {
                    display[i] = '_';
                }
                //If the letter has not been guessed, an underscore is added to the display array at the current index to represent the unguessed letter.
            }
                wordTextUI.text = string.Join(" ", display);
                //After the loop, the display array is joined into a string with spaces in between and assigned to the wordTextUI to
                //update the display of the word with correctly guessed letters and underscores for remaining letters.
                guessInputField.text = "";
                //Clears the input field after processing the guess.
            
            
    }
}
            

   
    
    

